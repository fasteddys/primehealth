﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterProviderApprovals.Enum
    {
        static public class TechnicalDestinationEnums
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
                ReOpenedByAudit = 20,
                SavedAsFax = 21,
                PendingTechnicalApproveFromDepartments = 22,
                EndTechnicalApproveFromDepartments = 23
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
                TransfareRequestToBool = 2,
                ChangeMedicalID = 3,
                LogIn = 4,
                LogOut = 5,
                UpdateApproveOrRejectNotes = 6,
                UpdateActionTime = 7,
                AssignRequest = 8,
                EditTicketType = 9,
                ManagerOverRideAuthentcation = 10,
                FalseBrowserLogin = 11,
                ChangeEmailRequestApprovalCategory = 12,
                ChangeStatus = 13,
                ProviderAssign = 14,
                AccountManagerAssign = 15,
                ProviderReply = 16,
                AccountManagerReply = 17
        }

            public enum LogType
            {
                LogIn = 1,
                LogOut = 2
            }


            public enum OnlineApprovalStatus
            {
                New = 1,
                AssignedByCallCenter = 2,
                Onhold = 3,
                Reopend = 4,
                Closed = 5,
                Approved = 6,
                Rejected = 7,
                PendingProviderAction = 8,
                Terminated = 9,
                PendingCallCenterAction = 10
            }
            public enum OnlineApprovalStatusColor
            {
                secondary = 1,
                danger = 2,
                info = 3,
                primary = 4,
                warning = 5,
                success = 6,
                light = 7,
                dark = 8
            }




            public enum OnlineApprovalSDetailsType
            {
                Assign = 1,
                Approve = 2,
                ReplyFromCallCenter = 3,
                Reject = 4,
                Reopen = 5,
                OpenNewRequest = 6,
                ReplyFromProvider = 7,
                Terminate = 8,
                Onhold = 9,
                EndOnhold = 10

            }
            public enum UserType
            {
            ProviderUser = 1,
            AccountManager = 2
            }


        public enum RequestType
            {
                Medical = 1,
                Financial = 2,
                MedicalAndFinancial = 3

            }
            public enum OnineApprovalLog
            {
                TerminatLog = 1,
                Onholdog = 2,


            }
            public enum TechnicalDestination
            {
                Providers = 1,
                AccountManager = 2
            }
        public enum TechnicalAprrovalRequestStatus
        {
            PendingAssign = 1,
            Assigned = 2,
            Done = 3
        }
    }

}
    
   