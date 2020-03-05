using BNC.BLL.Logic.Tables;
using BNC.DTOs;
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
    public class MapperBLL
    {
        DateTime creationDate;
        public MapperBLL(BNCEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
        }
        //-------------------------------------------------------------------------------------------------------
        public BatchingFilterationDetailDTO convertBatchingFilterationRequestToDTO(BatchingFilterationDetail BatchingFilterationDetail)
        {
            BatchingFilterationDetailDTO tempBatchingFilterationRequestDetials = new BatchingFilterationDetailDTO();
            tempBatchingFilterationRequestDetials.BatchingFilterationDetailID = BatchingFilterationDetail.BatchingFilterationDetailID;
            tempBatchingFilterationRequestDetials.FilterationRequestID = BatchingFilterationDetail.FilterationRequestFK;
            tempBatchingFilterationRequestDetials.FilterationRequestID = BatchingFilterationDetail.FilterationRequestFK;
            tempBatchingFilterationRequestDetials.FilterationCategoryID = BatchingFilterationDetail.FilterationCategoryFK;
            tempBatchingFilterationRequestDetials.TotalClaimsCount = BatchingFilterationDetail.TotalClaimsCount;
            tempBatchingFilterationRequestDetials.RemainingClaimsCount = BatchingFilterationDetail.RemainingClaimsCount;
            tempBatchingFilterationRequestDetials.CreationDate = BatchingFilterationDetail.CreationDate;

            return tempBatchingFilterationRequestDetials;
        }
        public BatchingRequestDTO convertBatchingRequestToDTO(BatchingRequest BatchingRequest)
        {
            return new BatchingRequestDTO()
            {
                BatchingRequestID = BatchingRequest.BatchingRequestID,
                NumberOfBatchClaims = BatchingRequest.NumberOfBatchClaims,
                BatchSystemFK = BatchingRequest.BatchSystemFK,
                BatchNumber = BatchingRequest.BatchNumber,
                BatchingOfficerFK = BatchingRequest.BatchingOfficerFK,
                BatchingOfficerComment = BatchingRequest.BatchingOfficerComment,
                FilterationCategoryFK = BatchingRequest.FilterationCategoryFK,
                TransferredToAuditDate = BatchingRequest.TransferredToAuditDate,//null
                CreationDate = BatchingRequest.CreationDate,
            };


        }
    
        public SPReqestDTO ConvertSpReqToDTO(SPRequest spRequest)
        {
            return new SPReqestDTO
            {
                EntrityFK = spRequest.EntrityFK,
                ReqByUserFk = spRequest.ReqByUserFk,
                ReqByUserNote = spRequest.ReqByUserNote,
                ReqFK = spRequest.ReqFK,
                SPStatusFK = spRequest.SPStatusFK,
                CreationDate= spRequest.CreationDate,
                IsLoadedByUser= spRequest.IsLoadedByUser,
                SPReqID = spRequest.SPReqID,


            };
        }

    }

}
