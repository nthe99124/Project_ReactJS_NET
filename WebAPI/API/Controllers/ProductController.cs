using API.Common;
using API.Common.Interface;
using API.Reponsitories;
using API.Reponsitories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;
using Model.Common;
using Model.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IProductColorRepository _ProductColorRepository;
        private readonly IProductImageRepository _ProductImageRepository;
        private readonly IImageRepository _ImageRepository;

        public ProductController(IProductRepository ProductRepository, IImageRepository ImageRepository, IProductColorRepository ProductColorRepository, IProductImageRepository ProductImageRepository)
        {
            _ProductRepository = ProductRepository;
            _ProductColorRepository = ProductColorRepository;
            _ProductImageRepository = ProductImageRepository;
            _ImageRepository = ImageRepository;
        }
        [HttpGet("GetAllProduct")]
        [Authorize]
        // get loại đơn giảm từ URL : [FromUri], get loại phức tạp Body : [FromBody]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageIndex = 1)
        {
            // có cách nào chỉ lấy vài trường trong Product return về Data không? Hay bắt buộc phải tạo 1 ViewModel khác để thực hiện
            var Respon = await _ProductRepository.GetAllProductRepository(pageIndex);
            if (Respon != null)
            {
                Respon.Success = true;
                Respon.Message = "Success";
            }
            else
            {
                Respon.Success = false;
                Respon.Message = "Null";
            }
            return Ok(Respon);
        }
        //public async Task<IActionResult>
        [HttpPost("InsertProduct")]
        [Authorize]
        public async Task<IActionResult> InsertProduct([FromBody] Product_Brand_Color_Img pro)
        {
            // vấn đề transaction với nhiều lệnh insert như thế này.
            var Product = new Product
            {
                //Id = pro.Id,
                Name = pro.NamePro,
                BrandID = pro.IdBrand == null ? 0 : pro.IdBrand,
                Description = pro.Description,
                Price = pro.Price,
                PromotionPrice = pro.PromotionPrice,
                Option = pro.Option,
                Type = pro.Type,
                Warranty = pro.Warranty,
                Weight = pro.Weight,
                Size = pro.Size,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                UpdatedBy = 1,
                UpdatedOn = DateTime.Now,
            };
            Product = await _ProductRepository.Create(Product);
            var ColorPro = new ProductColor
            {
                ProductID = Product.Id,
                ColorID = pro.IdColor
            };
            ColorPro = await _ProductColorRepository.Create(ColorPro);
            var Img = new Image
            {
                Id = pro.IdImage,
                UrlImage = pro.UrlImage
            };
            Img = await _ImageRepository.Create(Img);
            var ImgPro = new ProductImage
            {
                ProductID = Product.Id,
                ImageID = Img.Id,
            };
            ImgPro = await _ProductImageRepository.Create(ImgPro);
            var rs = new RestOutput<Product_Brand_Color_Img>();

            if (ImgPro != null && Img != null && ColorPro != null && Product != null)
            {
                rs.Success = true;
                rs.Message = "Insert Success";
            }
            else
            {
                rs.Success = false;
                rs.Message = "Insert Fails";
            }
            return Ok(rs);
        }
    }
}
