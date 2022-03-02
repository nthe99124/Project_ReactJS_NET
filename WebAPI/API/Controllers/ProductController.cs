using API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;
using Model.Common;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetPro")]
        [Authorize]
        public IActionResult GetAllProduct()
        {
            var getPro = _context.Products.Select(p => new { id = p.Id, name = p.Name });
            return Ok(new RestOutput
            {
                Success = false,
                Message = "Authenticate success",
                Data = getPro
            });
        }
    }
}
