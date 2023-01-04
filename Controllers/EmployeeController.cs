using EmpManagementSystem.Models;
using EmpManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private ICRUD cRUD;
        private IFileUpload fileUpload;
        public EmployeeController(ICRUD cRUD, IFileUpload fileUpload)  // where dependencies are injected  // not by new
        {
            this.cRUD = cRUD;
            this.fileUpload = fileUpload;
        }

        //get request
        public IActionResult Create()
        {
            var employee = new Employee();
            employee.Id = cRUD.GetMaxId();
            return View(employee);
        }

        public IActionResult About()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(Employee obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (await fileUpload.UploadFile(file))
                {
                    obj.ImageName = fileUpload.FileName;
                    cRUD.AddEmployee(obj);
                    return RedirectToRoute(new { Action = "Index", Controller = "Employee" });
                }
                else
                {
                    ViewBag.ErrorMessage = "File Upload failed";
                    return View(obj);
                }
            }
            ViewBag.Message = "Error adding employee..";
            return View(obj);
        }


        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.Employees = cRUD.GetEmployees();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            var emp = cRUD.GetEmployee(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }


        //get method
        public IActionResult Edit(int id)
        {
            var emp = cRUD.GetEmployee(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee obj)
        {
            if(ModelState.IsValid)
            {
                cRUD.UpdateEmployee(obj);
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Error editing employee";
            return View(obj);
        }

        public IActionResult Delete(int id) 
        {
            cRUD.DeleteEmployee(id);
            
            return RedirectToAction("Index");  
        }
    }
}
