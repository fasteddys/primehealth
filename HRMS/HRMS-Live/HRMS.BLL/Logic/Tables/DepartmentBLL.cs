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
   public class DepartmentBLL : Repository<Department>
    {
        DateTime creationDate;
        public DepartmentBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        { 
            creationDate = CreationDate;

        }

        //public DepartmentDTO GetDepartmentByID(int DepartmentID)
        //{
        //   Department Department = Find(x => x.DepartmentID == DepartmentID).FirstOrDefault();

        //    return new DepartmentDTO
        //    {
        //        DepartmentID = Department.DepartmentID,
        //        DepartmentName = Department.DepartmentName,
        //    };

        //}
    }
}
