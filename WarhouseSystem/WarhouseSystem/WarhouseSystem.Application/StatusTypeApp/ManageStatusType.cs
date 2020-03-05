using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;
namespace WarhouseSystem.Application.StatusTypeApp
{
    public class ManageStatusType : IManageStatusType
    {
        private UnitOfWork unit = null;

        MappingStatusType map = new MappingStatusType();

        public ManageStatusType()
        {
            unit = new UnitOfWork();
        }
        public IEnumerable<LockupViewModel> GetAllStatusTypes()
        {
            IEnumerable<StatusType> AllStatusTypes = unit.statusTypeRepository.GetAllData();
            IEnumerable<LockupViewModel> items = AllStatusTypes.Select(map.MapEntityToModel);
            return items;
        }
    }
}
