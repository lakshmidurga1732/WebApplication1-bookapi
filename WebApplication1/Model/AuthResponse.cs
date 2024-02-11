namespace WebApplication1.Model
{
    public class AuthResponse
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string Role  { get; set; }
        public string Token { get; set; }
    }
}
