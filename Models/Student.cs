using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StudentManagementSystem.Models
{
    [Table("tblStudents")]
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Class")]
        public string ClassRoom { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
