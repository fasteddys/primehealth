using BNC.DAL.Repositories;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.BLL.Logic.Tables
{
    public class ProviderTypeBLL : Repository<ProviderTypeDIM>
    {
        DateTime creationDate;
        public ProviderTypeBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public DataTable GetAllProviderTypes()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Provider Type ID");
            dataTable.Columns.Add("Provider Type Name");

            foreach (var items in GetAll())
            {
                dataTable.Rows.Add(items.ProviderTypeID, items.ProviderTypeName);
            }

            return dataTable;
        }
    }
}
