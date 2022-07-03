using System;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Repositories.Interface;
using API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.BaseEntity;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(ILogger<BrandController> logger, IBrandService brandService)
        {
            _logger = logger;
            _brandService = brandService;
        }

        [HttpGet("GetBrandPaging")]
        [Authorize]
        public IActionResult GetBrandPaging(int pageIndex = 1)
        {
            try
            {
                var brand = _brandService.GetAllPaging(new Paging(pageIndex));
                return Ok(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBrandPaging: ");
                return BadRequest();
            }
        }

        [HttpPost("InsertBrand")]
        [Authorize]
        public async Task<IActionResult> InsertBrand(Brand brand)
        {
            try
            {
                var brandAdded = await _brandService.Add(brand);
                if (brandAdded != null)
                {
                    return Ok(brandAdded);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InsertBrand: ");
                return BadRequest();
            }
        }

        [HttpPost("UpdateBrand")]
        [Authorize]
        public async Task<IActionResult> UpdateBrand(Brand brand)
        {
            try
            {
                var brandUpdated = await _brandService.Update(brand);
                if (brandUpdated != null)
                {
                    return Ok(brandUpdated);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateBrand: ");
                return BadRequest();
            }
        }

        [HttpPost("DeleteBrand")]
        [Authorize]
        public async Task<IActionResult> DeleteBrand(long id)
        {
            try
            {
                var brandUpdated = await _brandService.Delete(id);
                if (brandUpdated != null)
                {
                    return Ok(brandUpdated);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateBrand: ");
                return BadRequest();
            }
        }
    }
}
