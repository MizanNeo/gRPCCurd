using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using NeoSOFT.Common.Helpers;
using NeoSOFT.Domain.Models;
using NeoSOFT.GRPCServices;
using NeoSOFT.GRPCServices.Protos;
using NeoSOFT.Infrastrcture.Contracts;
using System.Net;

namespace NeoSOFT.GRPCServices.Services
{
    public class departMasterService : departMaster.departMasterBase
    {
        protected readonly IRepository _repository;
        protected readonly IMapper _mapper;
        public departMasterService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            if (request==null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply the model data"));
            }
            else
            {
                DepartMaster departMasters = new DepartMaster();
                departMasters.DepartName=request.DepartName;
                departMasters.IsActive=request.IsActive;
                // var departMarster = _mapper.Map<DepartMaster>(request);
                _repository.departMasterRepository.Create(departMasters);
                await _repository.departMasterRepository.SaveAsync();

                return await Task.FromResult(new CreateResponse
                {
                    Id=departMasters.Id
                });
            }
        }
        public override async Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
        {
            if (request.Id<=0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply the model data"));

            var result = await _repository.departMasterRepository.GetByIdAsync(request.Id);

            if (result!=null)
            {
                return await Task.FromResult(new GetByIdResponse
                {
                    Id=result.Id,
                    DepartName=result.DepartName,
                    IsActive=result.IsActive
                });
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Record not found"));
            }

        }
        public override async Task<GellAllResponse> GellAll(GetAllRequest request, ServerCallContext context)
        {
            var response = new GellAllResponse();
            var result = await _repository.departMasterRepository.GetAllAsync();

            foreach (var items in result)
            {
                response.ToDo.Add(new GetByIdResponse
                {
                    Id=items.Id,
                    DepartName=items.DepartName,
                    IsActive=items.IsActive

                });
            }
            return await Task.FromResult(response);

        }

        public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
        {
            if (request==null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply the model data"));

            var result = await _repository.departMasterRepository.FindFirstAsync(t => t.Id==request.Id);

            if (result==null)
                throw new RpcException(new Status(StatusCode.NotFound, "Record not found"));

            result.Id=request.Id;
            result.DepartName=request.DepartName;
            result.IsActive=request.IsActive;

            _repository.departMasterRepository.Update(result);
            await _repository.departMasterRepository.SaveAsync();

            return await Task.FromResult(new UpdateResponse
            {
                Id=result.Id,
                DepartName=result.DepartName,
                IsActive=result.IsActive

            });
        }
        public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            if (request.Id<=0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply the model data"));

            var result = await _repository.departMasterRepository.FindFirstAsync(t => t.Id==request.Id);

            if (result==null)
                throw new RpcException(new Status(StatusCode.NotFound, "Record not found"));

            result.Id=request.Id;
            result.IsActive=false;

            _repository.departMasterRepository.Update(result);
            await _repository.departMasterRepository.SaveAsync();

            return await Task.FromResult(new DeleteResponse { Result=true }) ;
      
        }
    }
}

