using NeoSOFT.Infrastructure.Context;
using NeoSOFT.Domain.Models;
using NeoSOFT.Infrastructure.Repositories;
using NeoSOFT.Infrastrcture.Contracts;

namespace NeoSOFT.Infrastrcture.Repositories
{    
	public partial class departMasterRepository : RepositoryBase<DepartMaster>, IdepartMasterRepository
    {
		public departMasterRepository(EmployeeManagementContext repositoryContext) : base(repositoryContext)
        {

        }
    }        
	   
}