
namespace NetBanking.Core.Application.Dtos.User
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Cedula { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
        public List<string>? Roles { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
