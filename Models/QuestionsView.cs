using System;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Models
{
    public class QuestionsView
    {
        [Display(Name = "Question no")]
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public DateTime InsertDate { get; set; }
        public string ActiveOrInactive { get; set; }
    }
}
