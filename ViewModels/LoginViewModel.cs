using System.ComponentModel.DataAnnotations;


namespace EmpManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="User Name cannot be blank")]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Password cannot be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }  
    }
}
