using BNC.DAL.Repositories;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.BLL.Logic.Tables
{
  public  class SPRequestUserBLL : Repository<SPRequestUser>
    {
        DateTime creationDate;

        public SPRequestUserBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public SPRequestUser AddSpUserReq(SPRequestUser sPUserRequest)
        {
            this.Add(sPUserRequest);
            return sPUserRequest;
        }
        public SPRequestUser GetSpUserRequest(int spReqFK)
        {
            return this.Find(u => u.SPReqFK == spReqFK).FirstOrDefault();
        }

    }
}
