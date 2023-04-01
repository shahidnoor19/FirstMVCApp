using FirstMVCapp.Data;
using FirstMVCapp.Models;
using FirstMVCapp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCapp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DatabaseContext _context;
        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await _context.Employees.ToListAsync();
            return View(employee);
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Department = model.Department,
                DOB = model.DOB,
                Email = model.Email,
                Salary = model.Salary,
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var empoyee = await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (empoyee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = empoyee.Id,
                    Name = empoyee.Name,
                    Department = empoyee.Department,
                    DOB = empoyee.DOB,
                    Email = empoyee.Email,
                    Salary = empoyee.Salary,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _context.Employees.Where(e => e.Id == model.Id).FirstOrDefaultAsync();
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Department = model.Department;
                employee.DOB = model.DOB;
                employee.Email = model.Email;
                employee.Salary = model.Salary;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await _context.Employees.Where(e => e.Id == model.Id).FirstOrDefaultAsync();
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
