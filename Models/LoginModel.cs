using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
