using API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.DTOs.Product;
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
        public Task<IActionResult> GetAllProductPaging([FromQuery] int pageIndex = 1)
        {
            try
            {
                var rs = _productService.GetAllProductPaging(pageIndex);
                return Task.FromResult<IActionResult>(Ok(rs));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllProductPaging:");
                return Task.FromResult<IActionResult>(BadRequest());
            }
        }

        [HttpGet("GetProductByAnyPoint")]
        [Authorize]
        public async Task<IActionResult> GetProductByAnyPoint(ProductDto pro, int pageIndex = 1)
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
        public async Task<IActionResult> InsertProduct([FromBody] ProductDto pro)
        {
            try
            {
                var rs = await _productService.Add(pro);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add: ");
                return BadRequest();
            }
        }

        [HttpPut("UpdateProduct/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto pro)
        {
            try
            {
                var rs = await _productService.Update(pro);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateProduct: ");
                return BadRequest();
            }
        }

        [HttpPost("DeleteProduct")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            try
            {
                var rs = await _productService.Delete(id);
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
