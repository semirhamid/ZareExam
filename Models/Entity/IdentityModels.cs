using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ZareExam.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime BirthDate { get; set; }
        

    }


    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string Token { get; set; }
        public string JwtId { get; set; } 
        public bool IsUsed { get; set; } 
        public bool IsRevoked { get; set; } 
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; } 
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }

}