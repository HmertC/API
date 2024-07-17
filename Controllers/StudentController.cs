using KitapService.Helpers;
using KitapService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KitapService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int pagenum, int pagesi,string? sortOrder,string? sortColumn)
        {
          
            FormattableString fs = $"EXECUTE Student_Get {pagenum}, {pagesi}, {sortOrder}, {sortColumn}";
            var users = _context.Students.FromSql(fs).ToList();
            BaseResponse<List<Student>> response = new BaseResponse<List<Student>>()
            {
                success = true,
                data = users
            };
            return Ok(response);
        }

        //[HttpGet("{search}")]
        //public IActionResult Search(string searchStr)
        //{
        //    FormattableString fs = $@"{"Student_Search"} {searchStr}";

        //    var student = _context.Students.FromSqlInterpolated(fs).ToList().FirstOrDefault();
        //    if (student == null)
        //    {
        //        return BadRequest();
        //    }
        //    BaseResponse<Student> response = new BaseResponse<Student>()
        //    {
        //        success = true,
        //        data = student,
        //    };
        //    return Ok(response);
        //}

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FormattableString fs = $@"{"Student_Detail"} {id}";

            var student = _context.Students.FromSqlInterpolated(fs).ToList().FirstOrDefault();
            if (student == null)
            {
                return BadRequest();
            }
            BaseResponse<Student> response = new BaseResponse<Student>()
            {
                success = true,
                data = student,
            };
            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create(Student student)
        {
            //FormattableString fs = $@"{"Post_Api"} '{student.Name}', '{student.Surname}', '{student.studentNo}',student.birtday,1 ";


            var x = _context.Students.FromSqlRaw($"Student_Post @p0,@p1,@p2,@p3", new[] { student.Name, student.SurName, student.StudentNo.ToString(),student.Birtday }).ToList();
            BaseResponse<Student> response = new BaseResponse<Student>()
            {
                success = true,
                data = null,
            };
            _context.SaveChanges();
            return Ok(response);
        }
        [HttpPost("update")]
        public IActionResult Update(Student student)
        {
            

            var x = _context.Students.FromSqlRaw($"Student_Update @p0,@p1,@p2,@p3,@p4,@p5", new[] { student.Id.ToString(), student.Name, student.SurName, student.StudentNo.ToString(),student.Birtday.ToString(), student.IsActive.ToString() }).ToList();
            BaseResponse<Student> response = new BaseResponse<Student>()
            {
                success = true,
                data = null,
            };
            _context.SaveChanges();
            return Ok(response);
        }
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
           

            var x = _context.Database.ExecuteSqlRaw($"Student_Delete @p0", new SqlParameter("@p0", id));
            BaseResponse<Student> response = new BaseResponse<Student>()
            {
                success = true,
                data = null,
            };
            return Ok(response);
        }
    }
}
