using System.ComponentModel.DataAnnotations;

namespace ZareExam.Models.DTO
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string RefreshToken { get; set; }

        public string[] Roles {get; set;}
        public string[] Permissions { get; set; }
    }

    public class RegistrationResponse : AuthResult
    {

    }

    public class UserRegistrationRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required] 
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }


}