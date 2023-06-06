using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    [Table("tblEmployee")]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public decimal EmployeeSalary { get; set; }
    }
}
