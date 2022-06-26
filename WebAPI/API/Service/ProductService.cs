using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;
using Model.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public RestOutput<ProductViewModel> GetAllProductPaging(int pageIndex)
        {
            try
            {
                var rs = _unitOfWork.ProductRepository.GetAllProductPaging(pageIndex);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RestOutput<ProductViewModel>> GetProductByAnyPoint(ProductViewModel pro, int pageIndex)
        {
            try
            {
                var rs = await _unitOfWork.ProductRepository.GetProductByAnyPoint(pro, pageIndex);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RestOutputCommand<ProductViewModel>> InsertProduct(ProductViewModel pro)
        {
            try
            {
                var productColor = new List<ProductColor>();
                if (pro.ColorId != null)
                {
                    foreach (var t in pro.ColorId)
                    {
                        productColor.Add(new ProductColor
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

                var product = new Product
                {
                    Name = pro.NamePro,
                    BrandId = pro.IdBrand,
                    Description = pro.Description,
                    Price = pro.Price ?? 0,
                    PromotionPrice = pro.PromotionPrice ?? 0,
                    Option = pro.Option,
                    Type = pro.Type,
                    Warranty = pro.Warranty ?? 0,
                    Weight = pro.Weight ?? 0,
                    Size = pro.Size,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now,
                    UpdatedBy = 1,
                    UpdatedOn = DateTime.Now,
                    ProductColor = productColor,
                    ProductImage = productImages
                };

                await _unitOfWork.ProductRepository.CreateAsync(product);
                await _unitOfWork.CommitAsync();

                var rs = new RestOutputCommand<ProductViewModel>(pro);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<RestOutputCommand<ProductViewModel>> UpdateProduct(int id, ProductViewModel pro)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == id);
                if (product != null)
                {
                    product.Name = pro.NamePro;
                    product.BrandId = pro.IdBrand;
                    product.Description = pro.Description;
                    product.Price = pro.Price ?? 0;
                    product.Weight = pro.Weight ?? 0;
                    product.Size = pro.Size;
                    product.CreatedBy = pro.CreatedBy;
                    product.CreatedOn = pro.CreatedOn;
                    product.UpdatedBy = pro.UpdatedBy;
                    product.UpdatedOn = pro.UpdatedOn;
                    product.PromotionPrice = pro.PromotionPrice ?? 0;
                    product.Type = pro.Type;
                    product.Warranty = pro.Warranty ?? 0;
                    product.Weight = pro.Weight ?? 0;
                    product.Size = pro.Size;

                    // check Update Color
                    if (pro.ColorId != null)
                    {
                        var lstColorNew = _unitOfWork.ProductColorRepository.GetProductIdColorId(c => c.ProductId.Equals(id) && pro.ColorId.Contains(c.ColorId));
                        var lstColorOld = _unitOfWork.ProductColorRepository.GetProductIdColorId(c => c.ProductId.Equals(id));
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
                        var lstImgNew = _unitOfWork.ProductImageRepository.GetProductIdImageId(pro.UrlImage);
                        var lstImgOld = _unitOfWork.ProductImageRepository.GetProductIdImageIdByProductId(id);
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

                var rs = new RestOutputCommand<ProductViewModel>(pro);
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}