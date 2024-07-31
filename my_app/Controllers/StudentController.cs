using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_app.Data;
using my_app.Models;
using my_app.Models.Entities;

namespace my_app.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subcribed = viewModel.Subcribed
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Student");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            
            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subcribed = viewModel.Subcribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");

        }

        [HttpPost]
        public async Task<IActionResult> Delete (Student viewModel)
        {
            //tim sinh vien bang id de ktra xem sinh vien co ton tai trong csdl hay khong
            var student = await dbContext.Students
                //xử lý tranking
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            //neu co ton tai trong csdl thi xoa
            if (student is not null)
            { 
                dbContext.Students.Remove(viewModel);
                //luu lai thay doi sau khi xoa
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
    }
}
