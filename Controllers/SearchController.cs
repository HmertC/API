using KitapService.Helpers;
using KitapService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KitapService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly DataContext _context;

        public SearchController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search(string searchstr)
        {
            FormattableString fs = $@"{"Student_Search"}";
            var search = _context.Students.FromSql(fs).ToList();
            BaseResponse<List<Student>> response = new BaseResponse<List<Student>>()
            {
                success = true,
                data = search
            };
            if (string.IsNullOrEmpty(searchstr))
            {
                return BadRequest("Arama terimi boş olamaz.");
            }
            return Ok(search);
        }
    }
}
