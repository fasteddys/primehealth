using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.BLL.StaticData
{
    public static class StaticEnums
    {


        public enum Status
        {
            NewReceived = 1,
            Printed = 2,
            PendingFiltration = 3,
            FinishedFiltration = 4,
            PendingBatching = 5,
            UnderBatchingProcess = 6,
            FinishedBatching = 7,
            PendingAudit = 8,
            PendingDataEntryFinish = 9,
            PendingMedical = 10,
            PendingFindincial = 11,
            PendingMi = 12,
            PendingReconilition = 13,
            PendingProvider = 14,
            FinishedAudit = 15,
            NewAdministrationRequest = 16,
            DataEntryAdminAssign = 17,
            PendingDataEntryAssign = 18,
            PendingTransferToClosingTeam = 19,
            PendingDataEntryConfirm = 20,
            ConfirmDataEntryAssign = 21,
            DataEntryAssignFinished = 22,
            NewClosingRequest = 23,
            AssignToClosing = 24,
            ClosingConfirmReceiving = 25,
            ClosingFinished = 26,
            AssignMedical = 30,
            AssignFindincial = 31,
            AssignMi = 32,
            AssignReconilition = 33,
            AssignProvider = 34,
            ReAdministrationAssign = 35,
            ReAdministrationNewRequest = 36,
            ReAdministrationConfirmReceving = 37,
            ReAdministrationFinished = 38,
            FiltrationTransferredToBatching=39,
            UnderFiltrationProcess = 40,
            PendingTransferFromMidecalAudit= 41,
            PendingTransferFromMIAudit = 42,
            PendingTransferFromFindincialAudit = 43,
            PendingTransferFromReconilitionAudit = 44,
            TransferedFromRecieving = 45,
            DataEntryAdminFinished = 46,
            ClosingPendingTransfer = 47,
          	NewBatch= 48
        }

        public enum Entity
        {
            ReceivingRequest = 1,
            FilterationRequest = 2,
            BatchRequest = 3,
            AuditRequest = 6,
           // DataEntryRequest = 7,
            DataEntryAdminstrationRequest = 8,
            BatchingFilterationDetails = 9,
            DataEntryClosingRequest = 10,
            ClosedBatchReAdministrationRequest = 11,
            DataEntryBatchAssigningRequest = 12,
            BatchClosingRequest=13,
            ReAdministrationRequest = 14,
            MedicalAuditRequest=15,
            FinancialAuditRequest=16,
            MIAuditRequest=17,
            ReconciliationAuditRequest=18,
            //BatchingFilterationDetails = 19
            //BatchAuditingRequest=21
        }
        public enum AuditCategories
        {
            Medical=1,
            Financial=2,
            Misr=3,
            Reconciliation=4,
        }
        public enum Actions
        {
            Locked = 1,
            UnLocked = 2
        };
       public enum Team
        {
        DataEntry= 1,
        DataEntryAdmin= 2,
        Provider= 4,
        Recieving= 5,
        Filteration= 6,
        Audit= 7,
        Admin= 8,
        Closing= 9,
        MedicalAudit= 11,
        FinincialAudit= 12,
        ReconciliationAudit= 15,
        MisrInsuranceAudit= 21,
        Batching= 22  ,
        Readministration=23
        };

        public enum SPSTATUS
        {
            Pending=1,
            Approved=2,
            Rejected=4,
            Voided=5,
            DataManipulated=7,
            AutoApproved=12
        }
        public enum SPAction
        {
            Approve = 2,
            Rejecte = 3,
            DataManipulated = 5,
        }

        public enum SPREASONS
        {
            Pending = 1,

        }
        public enum Company
        {
	PrimeHealth=1,
	MedGulf=2,
	MisrInsurance=3
        }
     



    }
}
