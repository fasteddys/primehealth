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
    public class InsuranceSystemBLL : Repository<InsuranceSystemDIM>
    {
        DateTime creationDate;

        public InsuranceSystemBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        // get system data such as mca and ims;
        public List<InsuranceSystemDTO>getAllSystemsData()
        {
            var allSystemsData = this.GetAll();
            List<InsuranceSystemDTO> insuranceSystemList = new List<InsuranceSystemDTO>();
            InsuranceSystemDTO tempInsuranceSystem;
            foreach (var systemData in allSystemsData)
            {
                tempInsuranceSystem = new InsuranceSystemDTO();
                tempInsuranceSystem.InsuranceSystemID = systemData.SystemID;
                tempInsuranceSystem.InsuranceSystemName = systemData.SystemName;
                insuranceSystemList.Add(tempInsuranceSystem);
            }
            return insuranceSystemList;
        }
        //------------------------------------------------------------
        //13) mca or ibnsina
        public string getInsuranceSystemName(int SystemFK)
        {
            return Find(isy => isy.SystemID == SystemFK).FirstOrDefault().SystemName;
        }
        //------------------------------------------------------------

    }
}
