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
using Model.ViewModel.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IProductRepository _ProductRepository;
        private readonly IProductColorRepository _ProductColorRepository;
        private readonly IImageRepository _ImageRepository;

        public ProductController(IProductRepository ProductRepository, IImageRepository ImageRepository, IProductColorRepository ProductColorRepository, UnitOfWork unitOfWork)
        {
            _ProductRepository = ProductRepository;
            _ProductColorRepository = ProductColorRepository;
            _ImageRepository = ImageRepository;
            _unitOfWork = unitOfWork;
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
        [HttpGet("GetProductByAnyPoint")]
        [Authorize]
        public async Task<IActionResult> GetProductByAnyPoint(Product_Brand_Color_Img pro, int pageIndex = 0)
        {
            var Respon = await _ProductRepository.GetProductByAnyPoint(pro, pageIndex);
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
            //return NotFound()
        }
        [HttpPost("InsertProduct")]
        [Authorize]
        public async Task<IActionResult> InsertProduct([FromBody] ProductInsert pro)
        {
            // vấn đề transaction với nhiều lệnh insert như thế này.
            // trường hợp này sẽ không lấy được Id của Insert trước => phải Count để lấy
            //=> tạm thời dùng store

            // vấn đề lấy ID cho thằng sau Insert, khi mà SaveChange chưa được thực thi????
            var Product = new Product
            {
                //Id = pro.Id,
                Name = pro.NamePro,
                BrandID = pro.IdBrand == null ? null : pro.IdBrand,
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
            await _unitOfWork.ProductResponsitory.CreateAsync(Product);
            // ngoài cách này còn cách nào nữa không?
            var IdProduct = _unitOfWork.ProductResponsitory.GetLastID<Product>(x => x.Id) + 1;
            if(pro.ColorID != null)
            {
                for (int i = 0; i < pro.ColorID.Count; i++)
                {
                    await _unitOfWork.ProductColorResponsitory.CreateAsync(new ProductColor
                    {
                        ProductID = IdProduct,
                        ColorID = pro.ColorID[i],
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = 1,
                        UpdatedOn = DateTime.Now,
                    });
                }
            }
            if (pro.UrlImage != null)
            {
                for (int i = 0; i < pro.UrlImage.Count; i++)
                {
                    await _unitOfWork.ImageResponsitory.CreateAsync(new Image
                    {
                        UrlImage = pro.UrlImage[i],
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = 1,
                        UpdatedOn = DateTime.Now,
                    });
                    var IdImage = _unitOfWork.ProductResponsitory.GetLastID<Image>(x => x.Id) + 1;
                    await _unitOfWork.ProductImageResponsitory.CreateAsync(new ProductImage
                    {
                        ProductID = IdProduct,
                        ImageID = IdImage,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = 1,
                        UpdatedOn = DateTime.Now,
                    });
                }
            }
            
            await _unitOfWork.CommitAsync();
            var rs = new RestOutput<Product_Brand_Color_Img>();
            rs.Success = true;
            rs.Message = " Success";
            return Ok(rs);
Insert
        }
    }
}
