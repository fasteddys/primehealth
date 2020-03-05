using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;
using BNC.BLL.Logic.Shared_Logic;
using static BNC.BLL.StaticData.StaticEnums;
using BNC.BLL.StaticData;

namespace BNC.BLL.Logic.Tables
{

  
    public class SPRequestBLL : Repository<SPRequest>
    {
        DateTime creationDate;
        MapperBLL mapperBLL;
        SpReqReasonBLL spReqReasonBLL;
        BatchingRequestBLL batchingRequestBLL;
        MedicalAuditRequestBLL medicalAuditRequestBLL;
        FinancialAuditRequestBLL financialAuditRequestBLL;
        ReceivingRequestBLL receivingRequestBLL;
        FilterationRequestBLL filterationRequestBLL;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        SPRequestUserBLL sPRequestUserBLL;
        SPAuditRequestBLL sPAuditRequestBLL;
        SpUserBLL spUserBLL;
        SPStatusBLL spStatusBLL;
        SystemEntitiesBLL systemEntitiesBLL;

        public SPRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

            mapperBLL = new MapperBLL(Context, creationDate);
            batchingRequestBLL = new BatchingRequestBLL(Context, creationDate);
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, creationDate);
            medicalAuditRequestBLL = new MedicalAuditRequestBLL(Context, creationDate);
            financialAuditRequestBLL = new FinancialAuditRequestBLL(Context, creationDate);
            receivingRequestBLL = new ReceivingRequestBLL(Context, creationDate);
            filterationRequestBLL = new FilterationRequestBLL(Context, creationDate);
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, creationDate);
            spReqReasonBLL = new SpReqReasonBLL(Context, creationDate);
            sPRequestUserBLL = new SPRequestUserBLL(Context, creationDate);
            sPAuditRequestBLL = new SPAuditRequestBLL(Context, creationDate);
            spUserBLL = new SpUserBLL(Context, creationDate);
            spStatusBLL=new SPStatusBLL(Context, creationDate);
            systemEntitiesBLL = new SystemEntitiesBLL(Context, creationDate);

        }

        //-------------------------------------------------------------
        public List<SPReqestDTO> searchSpecialApprovalReq(SpecialApprovalDTO specialApprovalDTO)
        {
           
            var allReqsData = Find(r => r.SPStatusFK == specialApprovalDTO.SPStatusID);//1
            if (specialApprovalDTO.From!=null)//2
            {
                TimeSpan CreationFromTime = new TimeSpan(0, 1, 0);
                DateTime StartDate = DateTime.ParseExact(specialApprovalDTO.From, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + CreationFromTime;

                allReqsData = allReqsData.Where(r => r.CreationDate >= StartDate);
            }
            if(specialApprovalDTO.To != null)//3
            {
                TimeSpan CreationToTime = new TimeSpan(23, 59, 59);
                DateTime EndDate = DateTime.ParseExact(specialApprovalDTO.To, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + CreationToTime;
                allReqsData = allReqsData.Where(r => r.CreationDate <= EndDate);

            }
            if (specialApprovalDTO.UserID != 0)//4
            {
                allReqsData = allReqsData.Where(r => r.ReqByUserFk == specialApprovalDTO.UserID);

            }
            if (specialApprovalDTO.SPReasonFK != 0)//5
            {
               var allReqAtSpReqReason=spReqReasonBLL.Find(spRR => spRR.SPReasonFK == specialApprovalDTO.SPReasonFK).Select(r=>r.SPReqFK).ToList();
                allReqsData = from req in allReqsData
                              where allReqAtSpReqReason.Contains(req.SPReqID)
                              select req;

            }
            if (specialApprovalDTO.BatchNumber != null)//6
            {
                AuditCategDTO allAuditCategReqListFk = GetListOfAuditCategReqFk(specialApprovalDTO.BatchNumber);
           
                    allReqsData = from req in allReqsData
                                  where (req.EntrityFK == (int)Entity.MedicalAuditRequest && allAuditCategReqListFk.medicalList.Contains(req.ReqFK.Value)) || (req.EntrityFK == (int)Entity.FinancialAuditRequest && allAuditCategReqListFk.FinincialList.Contains(req.ReqFK.Value))
                                  select req;

            }
            if (specialApprovalDTO.ProviderFK != 0)//7
            {
                AuditCategDTO allAuditCategReqListFk =GetAuditCategReqsFk(specialApprovalDTO.ProviderFK);
                allReqsData = from req in allReqsData
                              where  (req.EntrityFK == (int)Entity.MedicalAuditRequest && allAuditCategReqListFk.medicalList.Contains(req.ReqFK.Value)) || (req.EntrityFK == (int)Entity.FinancialAuditRequest && allAuditCategReqListFk.FinincialList.Contains(req.ReqFK.Value))
                              select req;
            }
            List<SPReqestDTO> reqList = new List<SPReqestDTO>();
            foreach (var req in allReqsData)
            {
                var tempReq = mapperBLL.ConvertSpReqToDTO(req);
                tempReq.EntrityName = systemEntitiesBLL.GetEntityName((int)req.EntrityFK);
                tempReq.ReqByUserName = spUserBLL.GetUser((int)req.ReqByUserFk).UserName;
                tempReq.SPStatusName =spStatusBLL.GetSpStatusByID((int)req.SPStatusFK).SpStatutsName;
                reqList.Add(tempReq);

            }
            return reqList;
        }//1
        //-------------------------------------------------------------
        //take batchNumber and return list of medical and finicial Ids
        public int GetBatchID(string batchNumber)
        {
          var batchReq=batchingRequestBLL.Find(b => b.BatchNumber == batchNumber).FirstOrDefault();
            if(batchReq!=null)
            {
               return batchReq.BatchingRequestID;
            }
            else
            {
                return -1;
            }
        }
        public int GetBatchAuditReqByID(int batchId)
        {
            var batchAudiReq = batchAuditingRequestBLL.Find(bA => bA.BatchingRequestFK== batchId).FirstOrDefault();
            if (batchAudiReq != null)
            {
                return batchAudiReq.BatchAuditingRequestID;
            }
            else
            {
                return -1;
            }
        }
        public int GetBatchAuditReqByBatchNumber(string batchNumber)
        {
            int batchId = GetBatchID(batchNumber);
            if (batchId != -1)
            {
              int batchAuditId =GetBatchAuditReqByID(batchId);
                if(batchAuditId!=-1)
                {
                    return batchAuditId;
                }
            }
            return -1;
        }
        public int GetMedicalReqFK(int auditReqFk)
        {
            var medicalsReq = medicalAuditRequestBLL.Find(mr => mr.BatchAuditRequestFK == auditReqFk).FirstOrDefault();
            if(medicalsReq!=null)
            {
               return medicalsReq.MedicalAuditRequestID;
            }
            else
            {
                return -1;
            }
        }
        public int GetFininciallReqFK(int auditReqFk)
        {
            var finincialReq = financialAuditRequestBLL.Find(mr => mr.BatchAuditRequestFK == auditReqFk).FirstOrDefault();
            if (finincialReq != null)
            {
                return finincialReq.FinancialAuditRequestID;
            }
            else
            {
                return -1;
            }
        }
        public AuditCategDTO GetListOfAuditCategReqFk(string batchNumber)
        {
           int auditReqFk= GetBatchAuditReqByBatchNumber(batchNumber);
            List<int> ListMedReqFk = new List<int>();
            List<int> ListFinReqFk = new List<int>();
            AuditCategDTO auditCateg = new AuditCategDTO();
            if (auditReqFk!=-1)
            {
                int medicalFk = GetMedicalReqFK(auditReqFk);
                int finicialFk = GetFininciallReqFK(auditReqFk);

                if (medicalFk != -1)
                {
                    ListMedReqFk.Add(medicalFk);

                }
                if (finicialFk != -1)
                {
                    ListFinReqFk.Add(finicialFk);

                }
            }
            auditCateg.medicalList = ListMedReqFk;
            auditCateg.FinincialList = ListFinReqFk;
            return auditCateg;

        }//audit medical and finicial
        //-------------------------------------------------------------
        //take providFk and return list of medical and finicial Ids
        public List<int> GetRecvReqFk(int provFk)
        {
            var recvReq = receivingRequestBLL.Find(r=>r.ProviderFK== provFk).Select(r=>r.ReceivingRequestID).ToList();
            return recvReq;
        }
        public int GetFiltReqFk(int recFk)
        {
            var filiterReq = filterationRequestBLL.Find(fr => fr.ReceivingRequestFK == recFk).FirstOrDefault();
            if(filiterReq!=null)
            {
                return filiterReq.FilterationRequestID;
            }
            else
            {
                return -1;
            }
        }
        public List<int>GetBatchFilterationReqFkList(int filtReqFK)
        {
            List<int> batchFiltReqsFK = batchingFilterationDetailsBLL.Find(bfr => bfr.FilterationRequestFK == filtReqFK).Select(bf=>bf.BatchingFilterationDetailID).ToList();
            return batchFiltReqsFK;
        }
        public List<int>GetBatchFkList(List<int> batchFilitReqListFk)
        {
            List<int> tempBatchReqListFk;
            List<int> allBatchReqListFk = new List<int>();

            foreach (var batchFilitReqFk in batchFilitReqListFk)
            {
                tempBatchReqListFk = new List<int>();
                tempBatchReqListFk = batchingRequestBLL.Find(br =>br.BatchingFilterationDetailsFK== batchFilitReqFk).Select(br=>br.BatchingRequestID).ToList();
                if(tempBatchReqListFk!=null)
                {
                    allBatchReqListFk.AddRange(tempBatchReqListFk);

                }
            }

            return allBatchReqListFk;
   
        }
        public List<int>GetAuditFkList(List<int> BatchListFK)
        {
            List<int> allAuditReqListFk = new List<int>();
            foreach (var BatchFk in BatchListFK)
            {
              var auditReqFkList=batchAuditingRequestBLL.Find(ba => ba.BatchingRequestFK == BatchFk).Select(ba => ba.BatchAuditingRequestID).ToList();
                allAuditReqListFk.AddRange(auditReqFkList);
            }
           return allAuditReqListFk;
        }
        public List<int> GetListOfMedicalCategReqFk(List<int> auditListFK)
        {
            List<int> allAuditMedicalReqListFk = new List<int>();
            foreach (var auditFk in auditListFK)
            {
                int medicalFk = GetMedicalReqFK(auditFk);
                if (medicalFk != -1)
                {
                    allAuditMedicalReqListFk.Add(medicalFk);

                }
               
            }
            return allAuditMedicalReqListFk;
        }
        public List<int> GetListOfFinicialCategReqFk(List<int> auditListFK)
        {
            List<int> allAuditFinicialReqListFk = new List<int>();
            foreach (var auditFk in auditListFK)
            {
                int finicialFk = GetFininciallReqFK(auditFk);
                if (finicialFk != -1)
                {
                    allAuditFinicialReqListFk.Add(finicialFk);

                }
            }
            return allAuditFinicialReqListFk;
        }
        public AuditCategDTO GetAuditCategReqsFk(int provFk)
        {
            List<int> allAuditCategReqListFk = new List<int>();
            AuditCategDTO auditCategDTO = new AuditCategDTO();
            List<int> RecvReqFkList =GetRecvReqFk(provFk);
            if(RecvReqFkList.Count>0)
            {
                foreach (var RecvReqFK in RecvReqFkList)
                {
                    int FiltReqFk = GetFiltReqFk(RecvReqFK);
                    if (FiltReqFk != -1)
                    {
                        List<int> allBatchFilitReqs = GetBatchFilterationReqFkList(FiltReqFk);
                        if (allBatchFilitReqs.Count() > 0)
                        {
                            List<int> allBatchReqListFk = GetBatchFkList(allBatchFilitReqs);
                            if (allBatchReqListFk.Count() > 0)
                            {
                                List<int> allAuditReqListFk = GetAuditFkList(allBatchReqListFk);
                                if (allAuditReqListFk.Count() > 0)
                                {
                                    auditCategDTO.medicalList.AddRange(GetListOfMedicalCategReqFk(allAuditReqListFk));
                                    auditCategDTO.FinincialList.AddRange(GetListOfFinicialCategReqFk(allAuditReqListFk));

                                }
                            }
                        }
                    }
                }
               
            }
            return auditCategDTO;
        }
        //-------------------------------------------------------------
        public SPRequestUser ChangeSpRequestLifeCycle(SpAuditUserRequest spAuditUserRequest) 
        {
            //add sp user
            SPRequestUser sPRequestUser = new SPRequestUser
            {
                SPUserFK = spAuditUserRequest.SpUserFk,
                SPReqFK = spAuditUserRequest.SpReqFk,
                SpActionFK = spAuditUserRequest.SpActionFK,
                SpActionTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                CreationDate = DateTime.Now,
            };
            sPRequestUser = sPRequestUserBLL.AddSpUserReq(sPRequestUser);

            //edit sp audit request
            //------------------------------------------------------------
            sPAuditRequestBLL.EditAuditReq(spAuditUserRequest);
            //------------------------------------------------------------
            //edit sp request
            SPRequest sPRequest= GetSpReqByID(spAuditUserRequest.SpReqFk);
            if(sPRequest!=null)
            {
                //edit audit categ finincial and claims claim data and Batch audit request and batch request
                editAuditCategData(sPRequest.EntrityFK.Value, sPRequest.ReqFK.Value, spAuditUserRequest.ApprovedClaimCount, spAuditUserRequest.RejectClaimCount);

                if (spAuditUserRequest.ApprovedClaimCount>0&& spAuditUserRequest.RejectClaimCount==0)
                {
                    sPRequest.SPStatusFK = (int)StaticEnums.SPSTATUS.Approved;
                    sPRequestUser.SpActionFK = (int)StaticEnums.SPAction.Approve;
                }
                else if (spAuditUserRequest.ApprovedClaimCount == 0 && spAuditUserRequest.RejectClaimCount > 0)
                {
                    sPRequest.SPStatusFK = (int)StaticEnums.SPSTATUS.Rejected;
                    sPRequestUser.SpActionFK = (int)StaticEnums.SPAction.Rejecte;

                }
                else
                {
                    sPRequest.SPStatusFK = (int)StaticEnums.SPSTATUS.DataManipulated;
                    sPRequestUser.SpActionFK = (int)StaticEnums.SPAction.DataManipulated;

                }
                this.Edit(sPRequest);
            }
            return sPRequestUser;
        }//2
        //-------------------------------------------------------------
        public SPRequest GetSpReqByID(int spReqid)
        {
            return this.Find(r => r.SPReqID == spReqid).FirstOrDefault();
        }
        public void editAuditCategData(int EntityFk,int AuditCategReqFK, int numberOfApprovedClaims, int numberOfRejectedClaims)
        {
            if(EntityFk==(int)Entity.MedicalAuditRequest)
            {
                var medicalRequest = medicalAuditRequestBLL.Find(r => r.MedicalAuditRequestID == AuditCategReqFK).FirstOrDefault();
                if(medicalRequest != null)
                {
                    medicalRequest.NumberOfApprovedClaimsBySP = numberOfApprovedClaims;
                    medicalRequest.NumberOfRejectedClaimsBySP = numberOfRejectedClaims;
                    medicalRequest.TotalNumberOfApprovedClaims = numberOfApprovedClaims + medicalRequest.NumberOfApprovedClaims;
                    medicalRequest.TotalNumberOfRejectedClaims= numberOfRejectedClaims + medicalRequest.NumberOfRejectedClaims;
                    medicalAuditRequestBLL.Edit(medicalRequest);
                    editBatchAuditData(medicalRequest.BatchAuditRequestFK,(int) medicalRequest.TotalNumberOfApprovedClaims,(int)medicalRequest.TotalNumberOfRejectedClaims);
                }
            }
            else if (EntityFk == (int)Entity.FinancialAuditRequest)
            {
                var financialRequest = financialAuditRequestBLL.Find(r => r.FinancialAuditRequestID == AuditCategReqFK).FirstOrDefault();
                if (financialRequest != null)
                {
                    financialRequest.NumberOfApprovedClaimsBySP = numberOfApprovedClaims;
                    financialRequest.NumberOfRejectedClaimsBySP = numberOfRejectedClaims;
                    financialRequest.TotalNumberOfApprovedClaims = numberOfApprovedClaims + financialRequest.NumberOfApprovedClaims;
                    financialRequest.TotalNumberOfRejectedClaims = numberOfRejectedClaims + financialRequest.NumberOfRejectedClaims;
                    financialAuditRequestBLL.Edit(financialRequest);
                    editBatchAuditData(financialRequest.BatchAuditRequestFK, (int)financialRequest.TotalNumberOfApprovedClaims, (int)financialRequest.TotalNumberOfRejectedClaims);
                }
            }
        }
        public void editBatchAuditData(int batchAuditReqFK,int totalNumberOfApprovedClaims,int totalNumberOfRejectedClaims)
        {
          var batchAuditingRequest=  batchAuditingRequestBLL.Find(r => r.BatchAuditingRequestID == batchAuditReqFK).FirstOrDefault();
            if(batchAuditingRequest!=null)
            {
                batchAuditingRequest.TotalNumberOfApprovedClaims= totalNumberOfApprovedClaims;
                batchAuditingRequest.TotalNumberOfRejectedClaims= totalNumberOfRejectedClaims;
                batchAuditingRequest.IsLocked = false;
                batchAuditingRequestBLL.Edit(batchAuditingRequest);
                editBatchRequest(batchAuditingRequest.BatchingRequestFK);
            }

        }
        public void editBatchRequest(int BatchReqFK)
        {
          var batchingRequest= batchingRequestBLL.Find(r => r.BatchingRequestID == BatchReqFK).FirstOrDefault();
            if(batchingRequest!=null)
            {
                batchingRequest.IsLocked = false;
                batchingRequestBLL.Edit(batchingRequest);

            }

        }
        //-------------------------------------------------------------




    }
}
