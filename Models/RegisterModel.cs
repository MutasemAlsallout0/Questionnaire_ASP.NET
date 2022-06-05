﻿using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Password didn't match")]
        public string ComfirmPassword { get; set; }

    }
}
