using EmployeeManagement.Data;
using EmployeeManagement.Model;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
       

        private readonly AppDbContext _dbContext;

        public EmployeeController(AppDbContext context)
        {
            _dbContext = context;
        }


        /*
                [HttpGet("employees")]
                public IActionResult GetAllEmployees()
                {
                    var employees = _dbContext.tblEmployee.ToList();

                    return Ok(employees);
                }
        */

        // API01: Update an employee’s Employee Code [Don't allow duplicate employee code]
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployeeCode(int employeeId, [FromBody] string employeeCode)
        {
            var employee = _dbContext.tblEmployee.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
                return NotFound();

            var duplicateEmployee = _dbContext.tblEmployee.FirstOrDefault(e => e.EmployeeCode == employeeCode);
            if (duplicateEmployee != null)
                return BadRequest("Duplicate employee code");

            employee.EmployeeCode = employeeCode;
            _dbContext.SaveChanges();

            return Ok();
        }



        // API02: Get all employee based on maximum to minimum salary
        [HttpGet("salaries")]
        public IActionResult GetEmployeesBySalary()
        {
            var employees = _dbContext.tblEmployee.OrderByDescending(e => e.EmployeeSalary).ToList();
            return Ok(employees);
        }


        // API03: Find all employee who is absent at least one day
        [HttpGet("absent")]
        public IActionResult GetAbsentEmployees()
        {
            var employees = _dbContext.tblEmployeeAttendance
                .Where(a => a.IsAbsent)
                .Join(_dbContext.tblEmployee,
                    attendance => attendance.EmployeeId,
                    employee => employee.EmployeeId,
                    (attendance, employee) => employee)
                .ToList();

            return Ok(employees);
        }
        //API04 Get monthly attendance report of all employees
        [HttpGet("attendance/report")]
        public IActionResult GetMonthlyAttendanceReport()
        {
            var report = _dbContext.tblEmployee
                .Join(
                    _dbContext.tblEmployeeAttendance,
                    e => e.EmployeeId,
                    ea => ea.EmployeeId,
                    (e, ea) => new { Employee = e, Attendance = ea }
                )
                .GroupBy(
                    x => new { x.Employee.EmployeeId, x.Attendance.AttendanceDate.Month },
                    (key, group) => new
                    {
                        EmployeeName = group.FirstOrDefault().Employee.EmployeeName,
                        MonthName = key.Month.ToString("MMMM"),
                        TotalPresent = group.Count(a => a.Attendance.IsPresent),
                        TotalAbsent = group.Count(a => a.Attendance.IsAbsent),
                        TotalOffday = group.Count(a => a.Attendance.IsOffday)
                    }
                )
                .ToList();

            return Ok(report);
        }
    }
}
