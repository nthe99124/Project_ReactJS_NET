using API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.ViewModel.Product;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger,
                                IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("GetAllProductPaging")]
        [Authorize]
        public async Task<IActionResult> GetAllProductPaging([FromQuery] int pageIndex = 1)
        {
            try
            {
                var rs = _productService.GetAllProductPaging(pageIndex);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllProductPaging:");
                return BadRequest();
            }
        }

        [HttpGet("GetProductByAnyPoint")]
        [Authorize]
        public async Task<IActionResult> GetProductByAnyPoint(ProductViewModel pro, int pageIndex = 1)
        {
            try
            {
                var rs = await _productService.GetProductByAnyPoint(pro, pageIndex);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetProductByAnyPoint: ");
                return BadRequest();
            }
        }

        [HttpPost("InsertProduct")]
        [Authorize]
        public async Task<IActionResult> InsertProduct([FromBody] ProductViewModel pro)
        {
            try
            {
                var rs = await _productService.InsertProduct(pro);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InsertProduct: ");
                return BadRequest();
            }
        }

        [HttpPost("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, [FromBody] ProductViewModel pro)
        {
            try
            {
                var rs = await _productService.UpdateProduct(id, pro);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateProduct: ");
                return BadRequest();
            }
        }
    }
}
