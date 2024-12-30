namespace CoreApiFirst.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Consider using hashed passwords
        public string Role { get; set; } // Optional, for role-based authorization
    }
}
