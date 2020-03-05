using HRMS.DAL.Repositories;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
 public   class DeviceBLL: Repository< Device>
    {
        DateTime creationDate;

        public DeviceBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
             
        }
        public Device GetDeviceType(int? DeviceID)
        {
            Device device = new Device();
            try 
            {
                device = Find(x=>x.DeviceID==DeviceID).FirstOrDefault();
            }
            catch(Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");

            }
            return device;
        }
        }
}
