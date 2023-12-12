using AutoMapper;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Common.Helpers;
using NeoSOFT.Domain.Models;
using NeoSOFT.Infrastrcture.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Application.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<DepartMaster>>> GetAll()
        {
            var resultList = await _repository.departMasterRepository.GetAllAsync();

            return ApiResponseHelper.CreateApiResponse(_mapper.Map<IEnumerable<DepartMaster>>(resultList), HttpStatusCode.OK);
        }

        public async Task<ApiResponse<DepartMaster>> GetById(int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.CreateApiResponse<DepartMaster>(HttpStatusCode.BadRequest);
            }

            var result = await _repository.departMasterRepository.FindFirstAsync(x => x.Id == id);

            return ApiResponseHelper.CreateApiResponse(_mapper.Map<DepartMaster>(result), HttpStatusCode.OK);
        }
        public async Task<ApiResponse<bool>> Create(DepartMaster departMaster)
        {
            if (departMaster == null)
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            _repository.departMasterRepository.Create(departMaster);
            await _repository.departMasterRepository.SaveAsync();
            return ApiResponseHelper.CreateApiResponse(true, HttpStatusCode.BadRequest);
            //  return await GetById(facilityVm.Id);
        }

        public async Task<ApiResponse<bool>> Update(DepartMaster departMaster)
        {
            if (departMaster == null)
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            var result = await _repository.departMasterRepository.FindFirstAsync(x => x.Id == departMaster.Id);

            if (result == null)
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            result.DepartName=departMaster.DepartName;
            result.IsActive=departMaster.IsActive;

            _repository.departMasterRepository.Update(result);
            await _repository.departMasterRepository.SaveAsync();

            return ApiResponseHelper.CreateApiResponse(true, HttpStatusCode.BadRequest);
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            var result = await _repository.departMasterRepository.FindFirstAsync(x => x.Id == id);

            if (result == null)
            {
                return ApiResponseHelper.CreateApiResponse(false, HttpStatusCode.BadRequest);
            }

            result.IsActive = false;

            _repository.departMasterRepository.Update(result);
            await _repository.desigMasterRepository.SaveAsync();

            return ApiResponseHelper.CreateApiResponse(true, HttpStatusCode.OK);
        }
    }

}
