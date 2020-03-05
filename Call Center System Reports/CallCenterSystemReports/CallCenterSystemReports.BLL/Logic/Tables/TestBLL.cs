using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenterSystemReports.DAL.Repositories;
using CallCenterSystemReports.Entities;

namespace CallCenterSystemReports.BLL.Logic.Tables
{
    public class TestBLL : Repository<CallCenterAppUser>
    {
        public TestBLL(PhNetworkEntities Context) : base(Context)
        {

        }
    }
}
