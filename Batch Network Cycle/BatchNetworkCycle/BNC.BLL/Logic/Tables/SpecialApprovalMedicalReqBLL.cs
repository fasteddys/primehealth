using BNC.DAL.Repositories;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
  public  class SpecialApprovalMedicalReqBLL : Repository<SpecialApprovalMedicalReq>
    {
        DateTime creationDate;
        SPReasonBLL SPReasonBLL;
        SpReqReasonBLL SpReqReasonBLL;
        SPRequestBLL SPRequestBLL;
        SPRequestUserBLL SPRequestUserBLL;
        SpUserBLL SpUserBLL;
        SPAuditRequestBLL SPAuditRequestBLL;

        public SpecialApprovalMedicalReqBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            SPReasonBLL = new SPReasonBLL(context, creationDate);
            SpReqReasonBLL = new SpReqReasonBLL(context, creationDate);
            SPRequestBLL = new SPRequestBLL(context, creationDate);
            SPRequestUserBLL = new SPRequestUserBLL(context, creationDate);
            SpUserBLL = new SpUserBLL(context, creationDate);
            SPAuditRequestBLL = new SPAuditRequestBLL(context, creationDate);
        }
        public SpecialApprovalMedicalReq GetSpecialApprovalInfoByID(int spReqId)
        {
            SpecialApprovalMedicalReq specialApprovalMedicalReq = this.Find(sp => sp.SPReqID == spReqId).FirstOrDefault();

            var reasons = SpReqReasonBLL.Find(spRR => spRR.SPReqFK == spReqId).Select(spRR => spRR.SPReasonFK).ToList();

            var reasonsName = SPReasonBLL.Find(spR => reasons.Contains(spR.SPReasonID)).Select(spR => spR.SPReasonName);
            specialApprovalMedicalReq.Reasons = new List<string>();
            foreach (var reasonName in reasonsName)
            {
                specialApprovalMedicalReq.Reasons.Add(reasonName);
            }
            //get name action on and comment
            var spRequest = SPRequestBLL.Find(r => r.SPReqID == spReqId).FirstOrDefault();
            if (spRequest != null)
            {
                if (spRequest.SPStatusFK == (int)SPSTATUS.Approved || spRequest.SPStatusFK == (int)SPSTATUS.Rejected || spRequest.SPStatusFK == (int)SPSTATUS.DataManipulated)
                {
                    SPRequestUser spRequestUser = SPRequestUserBLL.GetSpUserRequest(spReqId);
                    SPAuditRequest spAuditRequest = SPAuditRequestBLL.GetSpAuditReq(spReqId);
                    if (spAuditRequest != null)
                    {
                        if (spAuditRequest.Notes != null)
                        {
                            specialApprovalMedicalReq.spUserActionComment = spAuditRequest.Notes;
                        }
                    }
                    if (spRequestUser != null)
                    {
                        specialApprovalMedicalReq.ActionOn = (DateTime)spRequestUser.SpActionTime;
                        User user = SpUserBLL.GetSpUserData(spRequestUser.SPUserFK.Value);
                        if (user != null)
                        {
                            specialApprovalMedicalReq.SpUserName = user.UserName;

                        }
                    }
                }
            }
            return specialApprovalMedicalReq;
        }

    }
}
