using StSudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    [Table("tblGrades")]
    public class Grade
    {
        [Key]
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }

        public decimal Score { get; set; }

        [Column("Grade")] // ← Maps "GradeLetter" → "Grade" in DB
        public string GradeLetter { get; set; }

        // Navigation properties (connect tables)
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }

    }
}
