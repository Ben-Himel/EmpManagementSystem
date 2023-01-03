using System.ComponentModel.DataAnnotations;
//using MessagePack;

namespace EmpManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string? DeptName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } //1 department many employees // a collection of Employees

    }
}
