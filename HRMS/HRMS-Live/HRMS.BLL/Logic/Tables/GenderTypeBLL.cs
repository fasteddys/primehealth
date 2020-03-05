 using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
 public   class GenderTypeBLL : Repository<GenderTypeDIM>
    {
        DateTime creationDate;

        public GenderTypeBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }

        public List<GenderTypeDTO> GetAllGenderTypes()
        {


            List<GenderTypeDTO> ListGenderType = new List<GenderTypeDTO>();

            foreach (var item in GetAll().Where(x => x.IsActive == true))
            {
                ListGenderType.Add(new GenderTypeDTO
                {
                     GenderID = item.GenderID,
                     GenderName = item.GenderName

                });

            }
            return ListGenderType;

        }



    }
}
