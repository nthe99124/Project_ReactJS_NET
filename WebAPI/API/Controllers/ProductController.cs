using API.Common;
using API.Common.Interface;
using API.Reponsitories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;
using Model.Common;
using Model.ViewModel;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("GetAllProduct")]
        [Authorize]
        // get loại đơn giảm từ URL : [FromUri], get loại phức tạp Body : [FromBody]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageIndex = 1)
        {
            // có cách nào chỉ lấy vài trường trong Product return về Data không? Hay bắt buộc phải tạo 1 ViewModel khác để thực hiện
            return Ok(new RestOutput<Product_Brand_Color_Img>
            {
                Success = true,
                Message = "Success",
                Data = await _ProductRepository.GetAllProductRepository(pageIndex)
            });
        }
    }
}
