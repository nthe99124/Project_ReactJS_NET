using API.Common;
using API.Common.Interface;
using API.Reponsitories.Interface;
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
        private readonly IProductRepository _ProductRepository;

        public ProductController(IProductRepository ProductRepository) 
        {
            _ProductRepository = ProductRepository;
        }
        [HttpGet("GetPro")]
        [Authorize]
        public IActionResult GetAllProduct()
        {
            //var getProSQL = "SELECT * FROM Product";
            //var getPro = this.SqlQuery(getProSQL);
            _ProductRepository.GetAllProductRepository();
            return Ok(new RestOutput
            {
                Success = false,
                Message = "Authenticate success",
                Data = _ProductRepository.GetAllProductRepository()
            });
        }
    }
}
