using BNC.DAL.Repositories;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.BLL.Logic.Tables
{
    public class GovernmentBLL : Repository<GovernmentDIM> 
    {
        DateTime creationDate;
        public GovernmentBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<GovernmentDTO> GetAllGovernment()
        {
            List<GovernmentDTO> governments = new List<GovernmentDTO>();
            foreach(var items in GetAll())
            {
                governments.Add(new GovernmentDTO
                {
                    GovernmentID = items.GovernmentID,
                    GovernmentName = items.GovernmentName
                });
            }
            return governments;
        }

        public DataTable GetAllGovernmentToDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Government ID");
            dataTable.Columns.Add("Government Name");

            foreach(var items in GetAll())
            {
                dataTable.Rows.Add(items.GovernmentID, items.GovernmentName);
            }

            return dataTable;
        }
    }
}
