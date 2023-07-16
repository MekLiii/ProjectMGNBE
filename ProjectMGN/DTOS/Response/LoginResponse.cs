namespace ProjectMGN.DTOS.Response
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Id { get; set; }
    }
}
