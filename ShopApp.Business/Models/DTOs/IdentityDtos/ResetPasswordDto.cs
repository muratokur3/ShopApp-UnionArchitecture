﻿
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Business.Models.DTOs.IdentityDtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
