using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class RequestHandlerBLL : Repository<RequestHandler>
    {
        DateTime creationDate;

        public RequestHandlerBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }
        public void Add(RequestHandler RequestHandler)
        {
            base.Add(RequestHandler);
        }

    }
}
