using KitapService.Helpers;
using KitapService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace KitapService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KitapController : ControllerBase
    {
        private readonly DataContext _context;

        public KitapController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            FormattableString fs = $@"{"Get_Api"}";
            var users = _context.Books.FromSql(fs).ToList();
            BaseResponse<List<Book>> response = new BaseResponse<List<Book>>()
            {
                success = true,
                data = users
            };
            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FormattableString fs = $@"{"Get_Detail_Api"} {id}";

            var book = _context.Books.FromSqlInterpolated(fs).ToList().FirstOrDefault();
            if(book == null)
            {
                return BadRequest();
            }
            BaseResponse<Book> response = new BaseResponse<Book>()
            {
                success = true,
                data = book,
            };
            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create(Book book)
        {
            //FormattableString fs = $@"{"Post_Api"} '{book.Name}', '{book.Description}', '{book.Category}',1 ";
           
           var x = _context.Books.FromSqlRaw($"Post_Api @p0,@p1,@p2", new[] { book.Name, book.Description, book.Category }).ToList();
            BaseResponse<Book> response = new BaseResponse<Book>()
            {
                success = true,
                data = null,
            };
            _context.SaveChanges();
            return Ok(response);
        }
        [HttpPost("update")]
        public IActionResult Update(Book book)
        {
            //FormattableString fs = $@"{"Post_Api"} '{book.Name}', '{book.Description}', '{book.Category}',1 ";

            var x = _context.Books.FromSqlRaw($"Put_Api @p0,@p1,@p2,@p3,@p4", new[] {book.Id.ToString(), book.Name, book.Description, book.Category,book.IsActive.ToString() }).ToList();
            BaseResponse<Book> response = new BaseResponse<Book>()
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
            //FormattableString fs = $@"{"Post_Api"} '{book.Name}', '{book.Description}', '{book.Category}',1 ";

            var x = _context.Database.ExecuteSqlRaw($"Delete_Api @p0", new SqlParameter("@p0", id));
            BaseResponse<Book> response = new BaseResponse<Book>()
            {
                success = true,
                data = null,
            };
            return Ok(response);
        }
    }
   
}

