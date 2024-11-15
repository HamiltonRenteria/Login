﻿using Application.Commons.Bases;
using Application.DTOs.User.Request;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities.Static;
using BC = BCrypt.Net.BCrypt;

namespace Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            BaseResponse<bool> response = new();

            try
            {
                User account = _mapper.Map<User>(requestDto);
                account.Password = BC.HashPassword(account.Password);

                response.Data = await _unitOfWork.User.RegisterAsync(account);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_REGISTER;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto)
        {
            BaseResponse<string> response = new();
            User? account = await _unitOfWork.User.AccountByUserName(requestDto.UserName!);

            if (account is not null)
            {
                if (BC.Verify(requestDto.Password, account.Password))
                {
                    response.IsSuccess = true;
                    response.Data = GenerateToken(account);
                    response.Message = ReplyMessage.MESSAGE_TOKEN;
                    return response;
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
            }

            return response;
        }

        private string GenerateToken(User user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.NameId, user.Email!),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString(), ClaimValueTypes.Integer64)
            ];

            JwtSecurityToken token = new(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"]!)),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
