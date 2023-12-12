using NeoSOFT.Common.Helpers;
using NeoSOFT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Application.Contracts
{
    public interface IDepartmentService
    {
        Task<ApiResponse<IEnumerable<DepartMaster>>> GetAll();
        Task<ApiResponse<DepartMaster>> GetById(int id);
        Task<ApiResponse<bool>> Create(DepartMaster departMaster);
        Task<ApiResponse<bool>> Update(DepartMaster departMaster);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
