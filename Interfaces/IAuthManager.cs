
using System.Security.Claims;
using ZareExam.Models.DTO;
using ZareExam.Models.Entity;

namespace ZareExam.Interface
{
    public interface IAuthManager
    {
        
        Task<List<Claim>> GetValidClaims(AppUser user);
        Task<AuthResult> VerifyToken(TokenRequest tokenRequest);
        Task<AuthResult> GenerateJwtToken(AppUser user);
    }
}
