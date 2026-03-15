using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    [Table("tblAttendance")]
    public class Attendance
    {
        [Key]
        public int ID { get; set; }
        public int StudentID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // Present / Absent / Late
        public string Status { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }

    }
}
