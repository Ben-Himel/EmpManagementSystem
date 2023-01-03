using Microsoft.EntityFrameworkCore;
namespace EmpManagementSystem.Models
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set;} // emp tables
        public DbSet<Department> Departments { get; set;}  //departments table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //Creating rows in Department for demo
            //Data seeding
            modelBuilder.Entity<Department>().HasData(
                new Department { DeptId = 1, DeptName = "HR" },
                new Department { DeptId = 2, DeptName = "Finance" },
                new Department { DeptId = 3, DeptName = "IT" },
                new Department { DeptId = 4, DeptName = "Marketing" }
                );
            //dummy records 
            modelBuilder.Entity<Employee>().HasData(
                new Employee { DeptId = 1, Id = 1, Name = "Bob", Age = 32, Department = Dept.HR, Address = "Frank St", Salary = 90200 },
                new Employee { DeptId = 3, Id = 2, Name = "Pat", Age = 21, Department = Dept.IT, Address = "Back Alley", Salary = 20.00 },
                new Employee { DeptId = 2, Id = 3, Name = "Angie", Age = 52, Department = Dept.Finance, Address = "Z Ave West", Salary = 70094 }

                );

        }

    }
}
