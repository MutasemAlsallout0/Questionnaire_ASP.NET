using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Models
{
    public class Users
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
    }
}
