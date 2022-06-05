using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Models
{
    public class CreateModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Question { get; set; }
        [Required]
        [MaxLength(100)]

        public string Option1 { get; set; }
        [Required]
        [MaxLength(100)]

        public string Option2 { get; set; }
        [Required]
        [MaxLength(100)]

        public string Option3 { get; set; }
    }
}
