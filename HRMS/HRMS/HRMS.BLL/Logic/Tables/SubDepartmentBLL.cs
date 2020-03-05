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
    public class SubDepartmentBLL : Repository<SubDepartment>
    {
        //  private UserBLL UserBLL;
        DateTime creationDate;
        public SubDepartmentBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

            // UserBLL = new UserBLL(Context);
        }

        public SubDepartmentDTO GetSubDepartmentByID(int SubDepartmentID)
        {
            SubDepartment SubDepartment = Find(x => x.SubDepartmentID == SubDepartmentID).FirstOrDefault();

            return new SubDepartmentDTO
            {
                SubDepartmentID = SubDepartment.SubDepartmentID,
                SubDepartmentName = SubDepartment.SubDepartmentName,
            };

        }


        public List<SubDepartmentDTO> GetSubDepartmentByDepartmrntID(int DepartmentID)
        {
            List<SubDepartmentDTO> subdepartmentList = new List<SubDepartmentDTO>();
            foreach (var item in Find(x => x.DepartmentFK == DepartmentID).ToList())
            {
                subdepartmentList.Add(new SubDepartmentDTO
                {
                    SubDepartmentID = item.SubDepartmentID,
                    SubDepartmentName = item.SubDepartmentName,

                }
                );
            }
            return subdepartmentList;
        }

        public List<SubDepartmentDTO> GetSubDepartmentByDepartmrntIDs(List< int> ListDepartmentIDs)
        {
            List<SubDepartmentDTO> subdepartmentList = new List<SubDepartmentDTO>();
            foreach(var itemID in ListDepartmentIDs)
            foreach (var item in Find(x => x.DepartmentFK == itemID).ToList())
            {
                subdepartmentList.Add(new SubDepartmentDTO
                {
                    SubDepartmentID = item.SubDepartmentID,
                    SubDepartmentName = item.SubDepartmentName,

                }
                );
            }
            return subdepartmentList;
        }


    }
}
