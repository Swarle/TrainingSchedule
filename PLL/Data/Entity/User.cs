namespace PLL.Data.Entity
{
    public class User : IEntity
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
