using System;
using System.ComponentModel.DataAnnotations;

namespace zadanie1_19.Models
{
    public class StudyTaskViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DeadLine { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}
