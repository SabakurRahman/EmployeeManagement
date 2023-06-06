using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    [Table("tblEmployeeAttendance")]
    public class EmployeeAttendance
    {
        public int EmployeeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsOffday { get; set; }
    }
}
