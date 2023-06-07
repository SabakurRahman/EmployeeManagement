using EmployeeManagement.Data;
using EmployeeManagement.Service;


namespace EmployeeManagement
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the DbContext
           
           
            // Register the EmployeeService
            services.AddScoped<EmployeeService>();

            // ...

            // Add other necessary configurations
        }

        // ...
    }
}
