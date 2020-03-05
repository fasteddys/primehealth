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
    public class SPStatusBLL : Repository<SPStatusDIM>
    {
        DateTime creationDate;
        public SPStatusBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<SpStatutsDTO> GetSpStatutsData()
        {
            var allSpStatuts = this.GetAll().ToList();
            List<SpStatutsDTO> SpStatutsList = new List<SpStatutsDTO>();
            SpStatutsDTO tempSpStatutsData;
            foreach (var SpStatut in allSpStatuts)
            {
                tempSpStatutsData = new SpStatutsDTO();
                tempSpStatutsData.SpStatutsID = SpStatut.SPStatusID;
                tempSpStatutsData.SpStatutsName = SpStatut.SPStatusName;
                SpStatutsList.Add(tempSpStatutsData);
            }
            return SpStatutsList;
        }
        public SpStatutsDTO GetSpStatusByID(int SpStatusFK)
        {
            SPStatusDIM SpStatut =this.Find(s => s.SPStatusID == SpStatusFK).FirstOrDefault();
            if (SpStatut!=null)
            {
                SpStatutsDTO tempSpStatutsData = new SpStatutsDTO();
                tempSpStatutsData.SpStatutsID = SpStatut.SPStatusID;
                tempSpStatutsData.SpStatutsName = SpStatut.SPStatusName;
                return tempSpStatutsData;

            }
            return null;



        }
        //--------------------------------------------------------

    }
}
