using EmpManagementSystem.Models;

namespace EmpManagementSystem.Services
{
    public class CRUDRepository : ICRUD
    {
        private List<Employee> employees;
        // Constructor
        public CRUDRepository()
        {
            employees = new List<Employee>();
            employees.Add(new Employee() {Id=1, Age=34, Address="LI,NY", Name="Elena" ,ImageName="elena.jpg",Department=Dept.Marketing,Salary=8000 });
            employees.Add(new Employee() { Id = 2, Age = 44, Address = "Princeton,NJ", Name = "Michael", ImageName = "michael.jpg", Department = Dept.Marketing, Salary = 10000 });
            employees.Add(new Employee() { Id = 3, Age = 24, Address = "Princeton,NJ", Name = "Logan", ImageName = "logan.jpg", Department = Dept.HR, Salary = 9000 });
            employees.Add(new Employee() { Id = 4, Age = 44, Address = "Princeton,NJ", Name = "Nathan", ImageName = "nathan.jpg", Department = Dept.IT, Salary = 8000 });
        }
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void DeleteEmployee(int? id)
        {
            var emptodelete=employees.Find(x => x.Id==id);
            if(emptodelete!=null)
            {
                employees.Remove(emptodelete);
            }
        }

        public Employee GetEmployee(int? id)
        {
            if(id == null)
            {
                return null;
            }
            else
            {
                return employees.Find(x => x.Id == id);
            }
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public int GetMaxId()
        {
            int maxid=employees.Max(x => x.Id);
            return maxid + 1;
        }

        public void UpdateEmployee(Employee employee)
        {
            var emptoupdate=employees.Find(x => x.Id==employee.Id);
            if(emptoupdate!=null)
            {
              emptoupdate.Id = employee.Id;
              emptoupdate.Name = employee.Name;
              emptoupdate.Salary = employee.Salary;
              emptoupdate.Address=employee.Address;
              emptoupdate.Department=employee.Department;
              emptoupdate.Age=employee.Age;
              emptoupdate.ImageName = employee.ImageName;
            }
        }
    }
}
