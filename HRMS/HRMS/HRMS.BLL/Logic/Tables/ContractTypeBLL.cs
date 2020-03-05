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
    public class ContractTypeBLL : Repository<ContractTypeDIM>
    {
        DateTime creationDate;

        public ContractTypeBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        { 
            creationDate = CreationDate;

        }
        public List<ContractTypeDTO> GetAllContractTypes()
        {


            List<ContractTypeDTO> ListContractType = new List<ContractTypeDTO>();

            foreach (var item in GetAll().Where(x => x.IsActive == true))
            {
                ListContractType.Add(new ContractTypeDTO
                {
                    ContractTypeID = item.ContractTypeID,
                    ContractTypeName = item.ContractName

                });

            }
            return ListContractType;

        }       
    }
}
