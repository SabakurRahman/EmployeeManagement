using EmployeeManagement.Model;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;user=root;password= ;database=employees";
            var serverVersion = new MySqlServerVersion(new Version(10, 4, 27));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeAttendance>()
                .HasKey(ea => new { ea.EmployeeId, ea.AttendanceDate });

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
