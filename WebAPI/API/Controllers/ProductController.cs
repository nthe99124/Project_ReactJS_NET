using API.Common;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.BaseEntity;
using Model.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IProductColorRepository _productColorRepository;
        public readonly IProductImageRepository _productImageRepository;
        private readonly IImageRepository _imageRepository;

        public ProductController(IProductRepository productRepository, IImageRepository imageRepository, IProductColorRepository productColorRepository, IProductImageRepository productImageRepository, UnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productColorRepository = productColorRepository;
            _productImageRepository = productImageRepository;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllProduct")]
        [Authorize]
        // get loại đơn giảm từ URL : [FromUri], get loại phức tạp Body : [FromBody]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageIndex = 1)
        {
            try
            {
                var rs = await _productRepository.GetAllProductRepository(pageIndex);
                if (rs != null)
                {
                    rs.Success = true;
                    rs.Message = "Success";
                }
                return Ok(rs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetProductByAnyPoint")]
        [Authorize]
        public async Task<IActionResult> GetProductByAnyPoint(ProductBrandColorImg pro, int pageIndex = 0)
        {
            try
            {
                var Respon = await _productRepository.GetProductByAnyPoint(pro, pageIndex);
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
                if (pro.ColorId != null)
                {
                    foreach (var t in pro.ColorId)
                    {
                        ProductColor.Add(new ProductColor
                        {
                            ColorId = t,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now,
                        });
                    }
                }

                var productImages = new List<ProductImage>();
                if (pro.UrlImage != null)
                {
                    foreach (var t in pro.UrlImage)
                    {
                        productImages.Add(new ProductImage
                        {
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Now,
                            Image = new Image
                            {
                                UrlImage = t,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                UpdatedBy = 1,
                                UpdatedOn = DateTime.Now,
                            }
                        });
                    }
                }
                var product = new Model.BaseEntity.Product
                {
                    Name = pro.NamePro,
                    BrandId = pro.IdBrand,
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
                    ProductImage = productImages
                };
                await _unitOfWork.ProductRepository.CreateAsync(product);
                await _unitOfWork.CommitAsync();

                var rs = new RestOutputCommand<ProductBrandColorImg>
                {
                    Success = true,
                    Message = "Success"
                };
                return Ok(rs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, [FromBody] ProductInsert pro)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    product.Name = pro.NamePro;
                    product.BrandId = pro.IdBrand;
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
                    if (pro.ColorId != null)
                    {
                        var lstColorNew = _productColorRepository.GetProductIdColorId(c => c.ProductId.Equals(id) && pro.ColorId.Contains(c.ColorId));
                        var lstColorOld = _productColorRepository.GetProductIdColorId(c => c.ProductId.Equals(id));
                        var lstColorNewClone = new Dictionary<int, long>(lstColorNew);
                        var lstColorOldClone = new Dictionary<int, long>(lstColorOld);
                        var lstColorMax = lstColorOld.Count;
                        var lstColorMin = lstColorNew.Count;

                        if (lstColorNew.Count > lstColorOld.Count)
                        {
                            lstColorMax = lstColorNew.Count;
                            lstColorMin = lstColorOld.Count;
                        }

                        for (var i = 0; i < lstColorMax; i++)
                        {
                            for (var j = 0; j < lstColorMin; j++)
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
                            _unitOfWork.ProductColorRepository.DeleteRange(c => c.ProductId.Equals(id) && lstColorOldClone.Values.Contains(c.ColorId));
                        }
                        var productColors = new List<ProductColor>();
                        if (lstColorNewClone.Count != 0)
                        {
                            productColors.AddRange(lstColorNewClone.Select((t, i) => new ProductColor
                            {
                                ColorId = lstColorNewClone.ElementAt(i).Key,
                                ProductId = lstColorNewClone.ElementAt(i).Value,
                                UpdatedBy = 1,
                                UpdatedOn = DateTime.Now,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                            }));
                            await _unitOfWork.ProductColorRepository.CreateRangeAsync(productColors);
                        }
                    }
                    else
                    {
                        _unitOfWork.ProductColorRepository.DeleteRange(c => c.ProductId.Equals(id));
                    }
                    if (pro.UrlImage != null)
                    {
                        var lstImgNew = _productImageRepository.GetProductIdImageId(pro.UrlImage);
                        var lstImgOld = _productImageRepository.GetProductIdImageIdByProductId(pro.Id);
                        var lstImgNewClone = new Dictionary<long, long>(lstImgNew);
                        var lstImgOldClone = new Dictionary<long, long>(lstImgOld);
                        var lstImgMax = lstImgOld.Count;
                        var lstImgMin = lstImgNew.Count;
                        if (lstImgNew.Count > lstImgOld.Count)
                        {
                            lstImgMax = lstImgNew.Count;
                            lstImgMin = lstImgOld.Count;
                        }
                        for (var i = 0; i < lstImgMax; i++)
                        {
                            for (var j = 0; j < lstImgMin; j++)
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
                            _unitOfWork.ProductImageRepository.DeleteRange(c => c.ProductId.Equals(id) && lstImgOldClone.Values.Contains(c.ImageId));
                        }
                        var productImages = new List<ProductImage>();
                        if (lstImgNewClone.Count != 0)
                        {
                            productImages.AddRange(lstImgNewClone.Select((t, i) => new ProductImage
                            {
                                ImageId = lstImgNewClone.ElementAt(i).Key,
                                ProductId = lstImgNewClone.ElementAt(i).Value,
                                UpdatedBy = 1,
                                UpdatedOn = DateTime.Now,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                            }));
                            await _unitOfWork.ProductImageRepository.CreateRangeAsync(productImages);
                        }
                    }
                    else
                    {
                        _unitOfWork.ProductImageRepository.DeleteRange(c => c.ProductId.Equals(id));
                    }
                    _unitOfWork.ProductRepository.Update(product);
                    await _unitOfWork.CommitAsync();
                }
                var rs = new RestOutputCommand<ProductBrandColorImg>
                {
                    Success = true,
                    Message = "Success"
                };
                return Ok(rs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
