using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;

namespace BNC.BLL.Logic.Tables
{
    public class SPReasonBLL : Repository<SPReason>
    {
        DateTime creationDate;
        public SPReasonBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        //get id and name of provider to full dropDown list 
        public List<SpReasonsDTO> GetSpReasonsData()
        {
            var allSpReasons = this.GetAll().ToList();
            List<SpReasonsDTO> spReasonsList = new List<SpReasonsDTO>();
            SpReasonsDTO tempSpReasonsData;
            foreach (var SpReason in allSpReasons)
            {
                tempSpReasonsData = new SpReasonsDTO();
                tempSpReasonsData.SpReasonsID = SpReason.SPReasonID;
                tempSpReasonsData.SpReasonsName = SpReason.SPReasonName;
                spReasonsList.Add(tempSpReasonsData);
            }
            return spReasonsList;
        }
        //-------------------------------------------------------------

    }
}
