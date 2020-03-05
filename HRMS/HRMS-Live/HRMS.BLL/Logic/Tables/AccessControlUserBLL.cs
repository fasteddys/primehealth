using HRMS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;
using HRMS.DTOs.Business;

namespace HRMS.BLL.Logic.Tables
{
  public  class AccessControlUserBLL : Repository<AccessControlUser>
    {
        DateTime creationDate;
         
        public AccessControlUserBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<AccessControlUserDTO> GetALLAccessControlUser()
        {
            List<AccessControlUserDTO> listAccessControlUserDTO = new List<AccessControlUserDTO>();              
            List<AccessControlUser> ListAccessControlUser= GetAll().ToList();
            foreach(var item in ListAccessControlUser)
            {
                listAccessControlUserDTO.Add(new AccessControlUserDTO
                {
                    AccessControlUserID = item.AccessControlUserID,
                    AccessControlUserName = item.AccessControlUserName,
                    CreationDate = item.CreationDate,
                    DeletionDate = item.DeletionDate
                   ,
                    GroupName = item.GroupName,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted,
                    ModificationDate = item.ModificationDate


                });
            }

            return listAccessControlUserDTO;

        }


    }
}
