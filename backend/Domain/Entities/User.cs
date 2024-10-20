namespace Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
    }
}
