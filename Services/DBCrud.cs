using EmpManagementSystem.Models;

namespace EmpManagementSystem.Services
{
    public class DBCrud : ICRUD
    {
        private EmployeeContext _employeeContext;
        public DBCrud(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public void AddEmployee(Employee employee)
        {
            if(employee.Department.ToString()=="HR")
                employee.DeptId = 1;
            else if (employee.Department.ToString() == "Finance")
                employee.DeptId = 2;
            else if (employee.Department.ToString() == "IT")
                employee.DeptId = 3;
            else if (employee.Department.ToString() == "Marketing")
                employee.DeptId = 4;

            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
        }

        public void DeleteEmployee(int? id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if(employee != null)
            {
                _employeeContext.Employees.Remove(employee);
                _employeeContext.SaveChanges();
            }
        }

        public Employee GetEmployee(int? id)
        {
            return _employeeContext.Employees.Find(id);
        }

        public List<Employee> GetEmployees()
        {
            return new List<Employee>(_employeeContext.Employees); //employees are convereted into a list and returned
        }

        public int GetMaxId()
        {
            return _employeeContext.Employees.Max(x => x.Id)+1; 
        }

        public void UpdateEmployee(Employee employee)
        {
            var emp = _employeeContext.Employees.Find(employee.Id);
            if(emp != null)
            {
                emp.Id = employee.Id;
                emp.Name= employee.Name;
                emp.Age = employee.Age; 
                emp.Salary= employee.Salary;
                emp.Address= employee.Address;
                emp.Department= employee.Department;
                emp.ImageName= employee.ImageName;
                if (employee.Department.ToString() == "HR")
                    employee.DeptId = 1;
                else if (employee.Department.ToString() == "Finance")
                    employee.DeptId = 2;
                else if (employee.Department.ToString() == "IT")
                    employee.DeptId = 3;
                else if (employee.Department.ToString() == "Marketing")
                    employee.DeptId = 4;

                _employeeContext.SaveChanges();
            }
        }
    }
}
