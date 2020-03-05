using BNC.DAL.Repositories;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.BLL.Logic.Tables
{
   public class SPAuditRequestBLL : Repository<SPAuditRequest>
    {
        DateTime creationDate;

        public SPAuditRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public SPAuditRequest AddSPAuditRequest(SPAuditRequest sPAuditRequest)
        {
            this.Add(sPAuditRequest);
            return sPAuditRequest;
        }

        public SPAuditRequest AddSPAuditRequest(int  sPReqFk)
        {
            SPAuditRequest sPAuditRequest = new SPAuditRequest
            {
                ApprovedClaimCount = 0,
                RejectClaimCount = 0,
                PendingClaimCount = 0,
                SPReqFK = sPReqFk,
                IsActive = true,
                IsDeleted = false,
                IsDelayed=false,
                CreationDate = DateTime.Now,
            };
            this.Add(sPAuditRequest);

            return sPAuditRequest;
        }

        public void EditAuditReq(SpAuditUserRequest spAuditUserRequest)
        {
            SPAuditRequest sPAuditRequest = this.Find(r => r.SPReqFK == spAuditUserRequest.SpReqFk).FirstOrDefault();
            if (sPAuditRequest != null)
            {
                sPAuditRequest.ApprovedClaimCount = spAuditUserRequest.ApprovedClaimCount;
                sPAuditRequest.RejectClaimCount = spAuditUserRequest.RejectClaimCount;
                sPAuditRequest.PendingClaimCount = spAuditUserRequest.PendingClaimCount;
            }
            if (spAuditUserRequest.spUserComment != "" && spAuditUserRequest.spUserComment!=null)
            {
                sPAuditRequest.Notes = spAuditUserRequest.spUserComment;

            }

            this.Edit(sPAuditRequest);
        }
        public SPAuditRequest GetSpAuditReq(int spReqFK)
        {
            return Find(spAR => spAR.SPReqFK == spReqFK).FirstOrDefault();
        }

    }
}
