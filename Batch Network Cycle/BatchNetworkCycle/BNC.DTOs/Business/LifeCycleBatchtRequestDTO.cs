using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.Entities;
namespace BNC.DTOs.Business
{
  public  class LifeCycleBatchtRequestDTO
    {
        public BatchingRequestDTO BatchingRequest { get; set; }//1)
        public BatchAuditingRequestDTO BatchAuditingRequest { get; set; }//2)
        public CategoryAuditDTO MedicalAuditRequest { get; set; }//3)
        public CategoryAuditDTO FinancialAuditRequest { get; set; }//4)
        public CategoryAuditDTO MIAuditRequest { get; set; }//5)
        public CategoryAuditDTO ReconciliationAuditRequest { get; set; }//6)
        public DataEntryAdminstrationRequestDTO DataEntryAdminstrationRequest { get; set; }//7)
        public List<DataEntryBatchAssigningRequestDTO> DataEntryBatchAssigningRequestList { get; set; }//8)
        public BatchClosingRequestDTO BatchClosingRequest { get; set; }//9)
        public BatchClosingReAdministrationRequestDTO BatchClosingReAdministrationRequest { get; set;}//10)
        public List<CategoryAuditDTO> CategoryAuditList { get; set; }//11
    }
}
