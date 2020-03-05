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
using BNC.BLL.StaticData;
using BNC.BLL.Logic.Shared_Logic;

namespace BNC.BLL.Logic.Tables
{
    public class DataEntryBatchAssigningRequestBLL : Repository<DataEntryBatchAssigningRequest>
    {
        DateTime creationDate;

        public DataEntryBatchAssigningRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public DataEntryBatchAssigningRequestDTO GetRequestData(int ID)
        {
            DataEntryBatchAssigningRequest dataEntryBatchAssigningRequest = Find(x => x.DataEntryBatchAssigningRequestID == ID).FirstOrDefault();

            DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO = new DataEntryBatchAssigningRequestDTO
            {
                ConfirmRecievingByOfficerTime = dataEntryBatchAssigningRequest.ConfirmRecievingByOfficerTime,
                DataEntryOfficerFinishedOnSystemTime = dataEntryBatchAssigningRequest.DataEntryOfficerFinishedOnSystemTime,
                DataEntryBatchAssigningRequestID= dataEntryBatchAssigningRequest.DataEntryBatchAssigningRequestID,
                 DataEntryOfficerReceivingComment= dataEntryBatchAssigningRequest.DataEntryOfficerReceivingComment
            };

            return dataEntryBatchAssigningRequestDTO;
        }
     }
}
