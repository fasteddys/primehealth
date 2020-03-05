using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class FilterationRequestBLL : Repository<FilterationRequest>
    {
        DateTime creationDate;
        FilterationRequestDetailsBLL filterationRequestDetailsBLL;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        FilterationCategoriesBLL filterationCategoriesBLL;
        LockLoggesBLL lockLoggesBLL;
        public FilterationRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            filterationRequestDetailsBLL = new FilterationRequestDetailsBLL(Context, CreationDate);
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
            filterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);
            lockLoggesBLL = new LockLoggesBLL(Context, CreationDate);
        }
        
        public void CreateFilterationRequestDetial(FilterationRequestDetialsDTO FilterationRequestDetial)
        {

            FilterationRequestDetail FilterationRequestDetail = new FilterationRequestDetail
            {
                CreationDate = creationDate,
                IsActive = true,
                IsDeleted = false,
                FilterationRequestFK = FilterationRequestDetial.FilterationRequestID,
                FilterationCategoryFK = FilterationRequestDetial.CategoryID,
                FilterationOfficerClaimsCount = FilterationRequestDetial.FilterationOfficerClaimsCount,
                FilterationOfficerComment = FilterationRequestDetial.comment,
                FilterationOfficerFK =FilterationRequestDetial.OfficerID, 
                
            };
            filterationRequestDetailsBLL.Add(FilterationRequestDetail);
           var filterationRequest= Find(x => x.FilterationRequestID == FilterationRequestDetial.FilterationRequestID).FirstOrDefault();
            filterationRequest.FilterationRequestStatusFK = (int)Status.UnderFiltrationProcess;
            Edit(filterationRequest);
        }
        
    
        //add new filteration request after add recieving request
   

        //-----------------------------------------------------------------------------------------------------
        public int getNumberOfClaimsAtFilterationRequestDetail(int BatchingFilterationRequestDetailID)
        {
            var BatchingFilterationRequest = batchingFilterationDetailsBLL.Find(id => id.BatchingFilterationDetailID == BatchingFilterationRequestDetailID).FirstOrDefault();
            if (BatchingFilterationRequest != null)
            {
                return BatchingFilterationRequest.RemainingClaimsCount;
            }
            else
            {
                return -1;
            }
        }
        public void changeRemainingClaimsCount(int BatchingFilterationRequestDetailID, int ClaimsCount)
        {
            var BatchingFilterationRequest = batchingFilterationDetailsBLL.Find(id => id.BatchingFilterationDetailID == BatchingFilterationRequestDetailID).FirstOrDefault();
            if (BatchingFilterationRequest != null)
            {
                if (BatchingFilterationRequest.RemainingClaimsCount == BatchingFilterationRequest.TotalClaimsCount)
                {
                    BatchingFilterationRequest.BatchingFilterationDetailStatusFK = (int)Status.UnderBatchingProcess;
                }
                if (BatchingFilterationRequest.RemainingClaimsCount - ClaimsCount >= 0)
                {
                    if (BatchingFilterationRequest.RemainingClaimsCount- ClaimsCount == 0)
                    {
                        BatchingFilterationRequest.BatchingFilterationDetailStatusFK = (int)Status.FinishedBatching;
                        BatchingFilterationRequest.RemainingClaimsCount = BatchingFilterationRequest.RemainingClaimsCount - ClaimsCount;


                    }
                    else
                    {
                        BatchingFilterationRequest.RemainingClaimsCount = BatchingFilterationRequest.RemainingClaimsCount - ClaimsCount;
                    }
                }
                batchingFilterationDetailsBLL.Edit(BatchingFilterationRequest);
            }
        }

        public int GetCountOfClaimsOfFilterationRequest(int FilterationRequestID)
        {
            int CountOfAllClaims = 0;
            foreach (var item in filterationRequestDetailsBLL.Find(x => x.FilterationRequestFK == FilterationRequestID))
            {
                CountOfAllClaims = CountOfAllClaims + item.FilterationOfficerClaimsCount;

            }
            return CountOfAllClaims;
        }
        public string GetFilterationRequestStatus(int FilterationRequestID)
        {
            var FilterationStatus = Find(x => x.FilterationRequestID == FilterationRequestID).FirstOrDefault().FilterationRequestStatusFK;
            return Enum.GetName(typeof(Status), FilterationStatus).ToString();
           
        }
        public  List<FilterationCategoryDTO> GetFilterationRequestDetails(int FilterationRequestID)
        {
            List<FilterationCategoryDTO> FilterationDetails = new List<FilterationCategoryDTO>();
            foreach (var item in batchingFilterationDetailsBLL.Find(x=>x.FilterationRequestFK== FilterationRequestID))
            {
              var FilterationCategoryName=  filterationCategoriesBLL.Find(x => x.FilterationCategoriesID == item.FilterationCategoryFK).FirstOrDefault().FilterationCategoryName;
                FilterationDetails.Add(new FilterationCategoryDTO
                {
                     ClaimsCount= item.TotalClaimsCount,
                     FilterationCategoryName= FilterationCategoryName
                });

            }
            return FilterationDetails;
        }
        public List<FilterationCategoryDTO> GetFilterationRequestBeforeBatching(int FilterationRequestID)
        {
            List<FilterationCategoryDTO> FilterationDetails = new List<FilterationCategoryDTO>();
            foreach (var ItemCategory in filterationCategoriesBLL.Find(x=>x.IsActive==true&&x.IsDeleted==false))
            {
                int TotalClaims = 0;
                foreach (var item in filterationRequestDetailsBLL.Find(x => x.FilterationRequestFK == FilterationRequestID&&x.FilterationCategoryFK== ItemCategory.FilterationCategoriesID))
                {
                    TotalClaims = item.FilterationOfficerClaimsCount;



                }
                FilterationDetails.Add(new FilterationCategoryDTO
                {
                    ClaimsCount = TotalClaims,
                    FilterationCategoryName = ItemCategory.FilterationCategoryName
                });
            }
            return FilterationDetails;

        }
        //-----------------------------------------------------------------------------
        public int getReceivingRequestID(int FilterationRequestFK)
        {
            return Find(fr => fr.FilterationRequestID == FilterationRequestFK).FirstOrDefault().ReceivingRequestFK;
        }
        //-----------------------------------------------------------------------------
    }
}
