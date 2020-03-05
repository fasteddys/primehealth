using BNC.BLL.Logic.Tables;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Shared_Logic
{
    public class SharedReceivingFilterationBLL
    {
        DateTime creationDate;

        ReceivingRequestBLL receivingRequestBLL;
        FilterationRequestBLL filterationRequestBLL;
        FilterationRequestDetailsBLL filterationRequestDetailsBLL;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        FilterationCategoriesBLL filterationCategoriesBLL;
        public SharedReceivingFilterationBLL(BNCEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            receivingRequestBLL = new ReceivingRequestBLL(Context, CreationDate);
            filterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
            filterationRequestDetailsBLL = new FilterationRequestDetailsBLL(Context, CreationDate);
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
            filterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);


        }
        public int ReceivingRequestsCount(int FilterationRequestID)
        {
            var RequestID =  filterationRequestBLL.Find(x => x.FilterationRequestID == FilterationRequestID).FirstOrDefault().ReceivingRequestFK;
            return receivingRequestBLL.Find(x => x.ReceivingRequestID == RequestID).FirstOrDefault().ReceivingOfficerCalimsCount;
        }
        public void SendFiltrationRequestToBatching(int FilterationRequestID)
        {
            var filterationRequestDetails = filterationRequestDetailsBLL.Find(x =>
                     x.FilterationRequestFK == FilterationRequestID && x.IsDeleted == false && x.IsActive == true);
            foreach (var CategoryItem in filterationCategoriesBLL.Find(x=>x.IsActive==true&&x.IsDeleted==false).Select(x=>x.FilterationCategoriesID))
            {
                {
                    int TotalClaimsCount = 0;
                    foreach (var item in filterationRequestDetails.Where(x=>x.FilterationCategoryFK== CategoryItem&& x.FilterationRequestFK == FilterationRequestID))
                    {
                        TotalClaimsCount = TotalClaimsCount+ item.FilterationOfficerClaimsCount;
                    }
                    batchingFilterationDetailsBLL.Add(new BatchingFilterationDetail
                    {
                        CreationDate = creationDate,
                        FilterationRequestFK = FilterationRequestID,
                        FilterationCategoryFK = CategoryItem,
                        IsActive = true,
                        IsDeleted = false,
                        RemainingClaimsCount = TotalClaimsCount,
                        TotalClaimsCount = TotalClaimsCount,
                        BatchingFilterationDetailStatusFK=(int)Status.PendingBatching
                        
                    });
                }
            }
          var filterationRequest= filterationRequestBLL.Find(x => x.FilterationRequestID == FilterationRequestID).FirstOrDefault();
            filterationRequest.FilterationRequestStatusFK = (int)Status.FiltrationTransferredToBatching;
            filterationRequestBLL.Edit(filterationRequest);
        }
        public void CloseFiltrationRequest(int FiltrationRequestID)
        {
          var filterationRequest=  filterationRequestBLL.Find(x => x.FilterationRequestID == FiltrationRequestID).FirstOrDefault();
            filterationRequest.FilterationRequestStatusFK = (int)Status.FinishedFiltration;
            filterationRequest.FilterationRequestLatestClosingDate = creationDate;
            filterationRequestBLL.Edit(filterationRequest);
        }
    }
}
