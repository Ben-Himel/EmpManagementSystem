using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpManagementSystem.Models
{
    public enum Dept
    {
        HR=1,
        Finance,
        IT,
        Marketing
    }

    public class Employee
    {
        [Display(Name="Employee Id")]
        [Required(ErrorMessage ="Field canot be empty")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DeptId { get; set; } // foreign key

        [Display(Name ="Employee Name")]
        [Required(ErrorMessage ="Please fill in the name")]
        [MaxLength(75)]
        public string? Name { get; set; }
        [Range(18,80,ErrorMessage =" please fill in age between 18 and 80")]
        public int Age { get; set; }
        [Display(Name="Primay Address")]
        [DataType(DataType.MultilineText)]
        [MaxLength(250,ErrorMessage ="Address to long")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
        public string? ImageName { get; set; }

        public Dept Department { get; set; }

    }
}
