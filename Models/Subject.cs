using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StSudentManagementSystem.Models
{
    [Table("tblSubjects")]
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        public int Credits { get; set; } = 3;
    }
}
