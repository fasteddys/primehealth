using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSystemReports.BLL.StaticData
{
   public static class StaticEnums
    {
        public enum RequestStatus
        {
            NewAutoGenerated = 1,
            New = 2,
            PendingDoctorsAssign = 3,
            AssignedByDoctor = 4,
            PendingTechnicalApproveByDoctor = 5,
            ApprovedByDoctor = 6,
            RejectedByDoctor = 7,
            PendingAuditAssign = 8,
            AssignedByAudit = 9,
            PendingTechnicalApproveByAudit = 10,
            ApprovedByAudit = 11,
            RejectedByAudit = 12,
            Closed = 13,
            EndTechnicalApproveByDoctor = 14,
            EndTechnicalApproveByAudit = 15,
            AssignedByOpeartor = 16,
            Ignored = 17,
            RepliedOnInquiry = 18,
            ReOpenedByDoctor = 19,
            ReOpenedByAudit = 20
        }

        public enum TicketTypes
        {
            General = 1,
            Special = 2,
            Inquiries = 3,
            None = 4
        }

        public enum TicketPriority
        {
            Normal = 1,
            High = 2,
            Emergency = 3,
        }
        public enum TicketCategory
        {
            Inpatient = 1,
            Outpatient = 2,
            Medication = 3,
        }
        public enum EmailApprovalLogTypes
        {
            ReopenTicket = 1,
            Transfer = 2,
            ChangeMedicalID = 3,
            LogIn = 4,
            LogOut = 5,
            UpdateApproveOrRejectNotes = 6,
            UpdateActionTime = 7
        }
        public enum AverageTicketTimeSatsus
        {
            PendiningOperatorAssign=1,
            PendiningOperatorAction = 2,
            PendiningDoctorAssign = 3,
            PendiningDoctorAction = 4,
            PendiningAuditAssign = 5,
            PendiningAuditAction = 6,
            TotalAverageTime = 7,
        }
    }
}
