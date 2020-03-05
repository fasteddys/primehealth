﻿using BNC.DAL.Repositories;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
   public class SpecialApprovalFinincalReqBLL : Repository<SpecialApprovalFinincalReq>
    {
        DateTime creationDate;
        SPReasonBLL SPReasonBLL;
        SpReqReasonBLL SpReqReasonBLL;
        SPRequestBLL SPRequestBLL;
        SPRequestUserBLL SPRequestUserBLL;
        SpUserBLL SpUserBLL;
        SPAuditRequestBLL SPAuditRequestBLL;

        public SpecialApprovalFinincalReqBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            SPReasonBLL = new SPReasonBLL(context, creationDate);
            SpReqReasonBLL = new SpReqReasonBLL(context, creationDate);
            SPRequestBLL = new SPRequestBLL(context, creationDate);
            SPRequestUserBLL = new SPRequestUserBLL(context, creationDate);
            SpUserBLL = new SpUserBLL(context, creationDate);
            SPAuditRequestBLL = new SPAuditRequestBLL(context, creationDate);




        }
        public SpecialApprovalFinincalReq GetSpecialApprovalInfoByID(int spReqId)
        {

            SpecialApprovalFinincalReq specialApprovalFinincalReq = Find(sp => sp.SPReqID == spReqId).FirstOrDefault();
            var reasons = SpReqReasonBLL.Find(spRR => spRR.SPReqFK == spReqId).Select(spRR => spRR.SPReasonFK).ToList();

            var reasonsName = SPReasonBLL.Find(spR => reasons.Contains(spR.SPReasonID)).Select(spR => spR.SPReasonName);
            specialApprovalFinincalReq.Reasons = new List<string>();
            foreach (var reasonName in reasonsName)
            {
                specialApprovalFinincalReq.Reasons.Add(reasonName);
            }
            //get name action on and comment
           var spRequest=SPRequestBLL.Find(r => r.SPReqID == spReqId).FirstOrDefault();
            if (spRequest!=null)
            {
                if(spRequest.SPStatusFK==(int)SPSTATUS.Approved|| spRequest.SPStatusFK == (int)SPSTATUS.Rejected|| spRequest.SPStatusFK == (int)SPSTATUS.DataManipulated)
                {
                    SPRequestUser spRequestUser = SPRequestUserBLL.GetSpUserRequest(spReqId);
                    SPAuditRequest spAuditRequest = SPAuditRequestBLL.GetSpAuditReq(spReqId);
                    if(spAuditRequest!=null)
                    {
                        if(spAuditRequest.Notes!=null)
                        {
                            specialApprovalFinincalReq.spUserActionComment = spAuditRequest.Notes;
                        }
                    }
                    if (spRequestUser!=null)
                    {
                        specialApprovalFinincalReq.ActionOn =(DateTime)spRequestUser.SpActionTime;
                        User user= SpUserBLL.GetSpUserData(spRequestUser.SPUserFK.Value);
                        if(user!=null)
                        {
                            specialApprovalFinincalReq.SpUserName = user.UserName;

                        }
                    }
                }
            }
            return specialApprovalFinincalReq;
        }




    }
}
