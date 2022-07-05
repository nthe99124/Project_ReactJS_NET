using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Common.Interface;
using API.Service.Interface;
using Model.BaseEntity;
using Model.Common;

namespace API.Service
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public RestOutput<Brand> GetAllPaging(Paging paging)
        {
            var lstBrand = _unitOfWork.BrandRepository.GetAllPaging(paging);
            var rs = new RestOutput<Brand>(lstBrand.data.ToList(), lstBrand.totalRecord);
            return rs;
        }

        public async Task<RestOutputCommand<Brand>> Add(Brand entity)
        {
            await _unitOfWork.BrandRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            var rs = new RestOutputCommand<Brand>(entity);
            return rs;
        }

        public async Task<RestOutputCommand<Brand>> Update(Brand entity)
        {
            try
            {
                var brand = _unitOfWork.BrandRepository.FindBy(b => b.Id.Equals(entity.Id));
                if (brand != null)
                {
                    _unitOfWork.BrandRepository.Update(entity);
                    await _unitOfWork.CommitAsync();
                    var rs = new RestOutputCommand<Brand>(entity);
                    return rs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<RestOutputCommand<Brand>> Delete(dynamic id)
        {
            try
            {
                long idBrand = Convert.ToInt64(id);
                var brand = _unitOfWork.BrandRepository.FindBy(b => b.Id.Equals(idBrand));
                if (brand != null)
                {
                    _unitOfWork.BrandRepository.DeleteById(id);
                    await _unitOfWork.CommitAsync();
                    var rs = new RestOutputCommand<Brand>();
                    return rs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
