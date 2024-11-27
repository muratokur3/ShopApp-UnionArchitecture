using System.ComponentModel.DataAnnotations;

namespace ShopApp.Business.Models.DTOs.IdentityDtos
{
    public class LoginDto
    {
        //public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
