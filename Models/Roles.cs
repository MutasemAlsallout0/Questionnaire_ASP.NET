using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Questionnaire.Models
{
    public class Roles
    {
        public string Id { get; set; }
        [NotNull]
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [NotNull]
        [Required]
        [MinLength(1)]
        [NotMapped]
        public List<string> Claims { get; set; }
    }
}
