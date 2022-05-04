using API.Common;
using API.Common.Interface;
using API.Reponsitories;
using API.Reponsitories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.BaseEntity;
using Model.ViewModel;
using Model.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Model.Common.DataType;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IProductRepository _ProductRepository;
        private readonly IProductColorRepository _ProductColorRepository;
        private readonly IProductImageRepository _ProductImageRepository;
        private readonly IImageRepository _ImageRepository;

        public ProductController(IProductRepository ProductRepository, IImageRepository ImageRepository, IProductColorRepository ProductColorRepository, IProductImageRepository ProductImageRepository, UnitOfWork unitOfWork)
        {
            _ProductRepository = ProductRepository;
            _ProductColorRepository = ProductColorRepository;
            _ProductImageRepository = ProductImageRepository;
            _ImageRepository = ImageRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllProduct")]
        [Authorize]
        // get loại đơn giảm từ URL : [FromUri], get loại phức tạp Body : [FromBody]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageIndex = 1)
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetProductByAnyPoint")]
        [Authorize]
        public async Task<IActionResult> GetProductByAnyPoint(Product_Brand_Color_Img pro, int pageIndex = 0)
        {
            try
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
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("InsertProduct")]
        [Authorize]
        public async Task<IActionResult> InsertProduct([FromBody] ProductInsert pro)
        {
            try
            {
                var ProductColor = new List<ProductColor>();
                if (pro.ColorID != null)
                {

                    for (int i = 0; i < pro.ColorID.Count; i++)
                    {
                        ProductColor.Add(new ProductColor
                        {
                            ColorID = pro.ColorID[i],
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now,
                        });
                    }
                }

                var ProductImage = new List<ProductImage>();
                if (pro.UrlImage != null)
                {

                    for (int i = 0; i < pro.UrlImage.Count; i++)
                    {
                        ProductImage.Add(new ProductImage
                        {
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now,
                            Image = new Image
                            {
                                UrlImage = pro.UrlImage[i],
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                UpdatedBy = 1,
                                UpdatedOn = DateTime.Now,
                            }
                        });
                    }
                }
                var Product = new Model.BaseEntity.Product
                {
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
                    ProductColor = ProductColor,
                    ProductImage = ProductImage
                };
                await _unitOfWork.ProductResponsitory.CreateAsync(Product);
                await _unitOfWork.CommitAsync();

                var rs = new RestOutputCommand<Product_Brand_Color_Img>();
                rs.Success = true;
                rs.Message = "Success";
                return Ok(rs);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, [FromBody] ProductInsert pro)
        {
            try
            {
                var product = await _unitOfWork.ProductResponsitory.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    product.Name = pro.NamePro;
                    product.BrandID = pro.IdBrand;
                    product.Description = pro.Description;
                    product.Price = pro.Price;
                    product.Weight = pro.Weight;
                    product.Size = pro.Size;
                    product.CreatedBy = pro.CreatedBy;
                    product.CreatedOn = pro.CreatedOn;
                    product.UpdatedBy = pro.UpdatedBy;
                    product.UpdatedOn = pro.UpdatedOn;
                    product.PromotionPrice = pro.PromotionPrice;
                    product.Type = pro.Type;
                    product.Warranty = pro.Warranty;
                    product.Weight = pro.Weight;
                    product.Size = pro?.Size;
                    // check Update Color
                    if (pro.ColorID != null)
                    {
                        var lstColorNew = _ProductColorRepository.GetProductIdColorID(c => c.ProductID.Equals(id) && pro.ColorID.Contains(c.ColorID));
                        var lstColorOld = _ProductColorRepository.GetProductIdColorID(c => c.ProductID.Equals(id));
                        var lstColorNewClone = new Dictionary<int, long>(lstColorNew);
                        var lstColorOldClone = new Dictionary<int, long>(lstColorOld);
                        var lstColorMax = lstColorOld.Count;
                        var lstColorMin = lstColorNew.Count;

                        if (lstColorNew.Count > lstColorOld.Count)
                        {
                            lstColorMax = lstColorNew.Count;
                            lstColorMin = lstColorOld.Count;
                        }

                        for (int i = 0; i < lstColorMax; i++)
                        {
                            for (int j = 0; j < lstColorMin; j++)
                            {
                                if (lstColorOld.ElementAt(i).Key == lstColorNew.ElementAt(j).Key)
                                {
                                    lstColorOldClone.Remove(lstColorOld.ElementAt(i).Key);
                                    lstColorNewClone.Remove(lstColorNew.ElementAt(i).Key);
                                }
                            }
                        }

                        if (lstColorOldClone.Count != 0)
                        {
                            _unitOfWork.ProductColorResponsitory.DeleteRange(c => c.ProductID.Equals(id) && lstColorOldClone.Values.Contains(c.ColorID));
                        }
                        var ProductColor = new List<ProductColor>();
                        if (lstColorNewClone.Count != 0)
                        {
                            for (int i = 0; i < lstColorNewClone.Count; i++)
                            {
                                ProductColor.Add(new ProductColor
                                {
                                    ColorID = lstColorNewClone.ElementAt(i).Key,
                                    ProductID = lstColorNewClone.ElementAt(i).Value,
                                    UpdatedBy = 1,
                                    UpdatedOn = DateTime.Now,
                                    CreatedBy = 1,
                                    CreatedOn = DateTime.Now,
                                });
                            }
                            await _unitOfWork.ProductColorResponsitory.CreateRangeAsync(ProductColor);
                        }
                    }
                    else
                    {
                        _unitOfWork.ProductColorResponsitory.DeleteRange(c => c.ProductID.Equals(id));
                    }
                    if (pro.UrlImage != null)
                    {
                        var lstImgNew = _ProductImageRepository.GetProductIdImageID(pro.UrlImage);
                        var lstImgOld = _ProductImageRepository.GetProductIdImageIDByProductID(pro.Id);
                        var lstImgNewClone = new Dictionary<long, long>(lstImgNew);
                        var lstImgOldClone = new Dictionary<long, long>(lstImgOld);
                        var lstImgMax = lstImgOld.Count;
                        var lstImgMin = lstImgNew.Count;
                        if (lstImgNew.Count > lstImgOld.Count)
                        {
                            lstImgMax = lstImgNew.Count;
                            lstImgMin = lstImgOld.Count;
                        }
                        for (int i = 0; i < lstImgMax; i++)
                        {
                            for (int j = 0; j < lstImgMin; j++)
                            {
                                if (lstImgOld.ElementAt(i).Key == lstImgNew.ElementAt(j).Key)
                                {
                                    lstImgOldClone.Remove(lstImgOld.ElementAt(i).Key);
                                    lstImgNewClone.Remove(lstImgNew.ElementAt(i).Key);
                                }
                            }
                        }
                        if (lstImgOldClone.Count != 0)
                        {
                            _unitOfWork.ProductImageResponsitory.DeleteRange(c => c.ProductID.Equals(id) && lstImgOldClone.Values.Contains(c.ImageID));
                        }
                        var ProductImg = new List<ProductImage>();
                        if (lstImgNewClone.Count != 0)
                        {
                            for (int i = 0; i < lstImgNewClone.Count; i++)
                            {
                                ProductImg.Add(new ProductImage
                                {
                                    ImageID = lstImgNewClone.ElementAt(i).Key,
                                    ProductID = lstImgNewClone.ElementAt(i).Value,
                                    UpdatedBy = 1,
                                    UpdatedOn = DateTime.Now,
                                    CreatedBy = 1,
                                    CreatedOn = DateTime.Now,
                                });
                            }
                            await _unitOfWork.ProductImageResponsitory.CreateRangeAsync(ProductImg);
                        }
                    }
                    else
                    {
                        _unitOfWork.ProductImageResponsitory.DeleteRange(c => c.ProductID.Equals(id));
                    }
                    _unitOfWork.ProductResponsitory.Update(product);
                    await _unitOfWork.CommitAsync();
                }
                var rs = new RestOutputCommand<Product_Brand_Color_Img>();
                rs.Success = true;
                rs.Message = "Success";
                return Ok(rs);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
