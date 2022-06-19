using System;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.BaseEntity;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public BrandController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetBrandPaging")]
        [Authorize]
        public async Task<IActionResult> GetBrandPaging(int pageIndex = 1)
        {
            try
            {
                var brand = _unitOfWork.BrandRepository.GetAllPaging(new Paging(pageIndex));
                return Ok(new RestOutput<Brand>(brand.data.ToList(), brand.count));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetBrandPaging: " + ex);
                return BadRequest();
            }
        }

        [HttpPost("InsertBrand")]
        [Authorize]
        public async Task<IActionResult> InsertBrand(Brand brand)
        {
            try
            {
                await _unitOfWork.BrandRepository.CreateAsync(brand);
                await _unitOfWork.CommitAsync();
                return Ok(new RestOutputCommand<Brand>());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "InsertBrand: " + ex);
                return BadRequest();
            }
        }

        [HttpPost("UpdateBrand")]
        [Authorize]
        public async Task<IActionResult> UpdateBrand(Brand brand)
        {
            try
            {
                _unitOfWork.BrandRepository.Update(brand);
                await _unitOfWork.CommitAsync();
                return Ok(new RestOutputCommand<Brand>());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "UpdateBrand: " + ex);
                return BadRequest();
            }
        }
    }
}
