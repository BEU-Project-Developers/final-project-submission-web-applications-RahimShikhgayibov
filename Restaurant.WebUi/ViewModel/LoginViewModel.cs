﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebUi.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
