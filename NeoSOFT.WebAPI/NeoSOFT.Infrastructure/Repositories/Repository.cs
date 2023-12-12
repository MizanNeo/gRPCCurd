using NeoSOFT.Infrastrcture.Contracts;
using NeoSOFT.Infrastrcture.Repositories;
using NeoSOFT.Infrastructure.Context;

namespace NeoSOFT.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private EmployeeManagementContext _repoContext;

        public Repository(EmployeeManagementContext repoContext)
        {
            _repoContext = repoContext;
        }

        
        private IdepartMasterRepository _departMaster;
        public IdepartMasterRepository departMasterRepository 
        {
            get 
            {
                if (_departMaster == null)
                {
                    _departMaster = new departMasterRepository(_repoContext);
                }
                return _departMaster;
            }
        }
    }
}      
