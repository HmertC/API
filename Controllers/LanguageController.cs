using KitapService.Helpers;
using KitapService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KitapService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly DataContext _context;

        public LanguageController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll( string language)
        {
            FormattableString lg = $@"{"Language_Get"} {language}";
            var langu = _context.Languages.FromSql(lg).ToList();

            BaseResponse<List<Language>> response = new BaseResponse<List<Language>>()
            {
                success = true,
                data = langu,
            };
            return Ok(response);
        }
    }
}
