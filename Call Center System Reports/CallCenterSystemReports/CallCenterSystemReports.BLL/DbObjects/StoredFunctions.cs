using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CallCenterSystemReports.DAL.Factory;
using CallCenterSystemReports.Entities;
using CallCenterSystemReports.DTOs;
using static CallCenterSystemReports.BLL.StaticData.StaticEnums;

namespace CallCenterSystemReports.BLL.DbObjects
{
    public sealed class StoredFunctions : ContainerContextFactory, IDisposable
    {
        public int LanguageId { get; set; }

        #region Constructors

        public StoredFunctions()
        {

        }

        public StoredFunctions(int lang)
        {
            LanguageId = lang;
        }

        public List<TikcetsAvaregeTimeDTO> GetTicketsAverageTimeByType()
        {

            var Result = context.SP_GetTicketsAverageTimeByType();

            List<TikcetsAvaregeTimeDTO> TicketsList = new List<TikcetsAvaregeTimeDTO>();

            foreach (var item in Result)
            {
                TikcetsAvaregeTimeDTO temp = new TikcetsAvaregeTimeDTO();
                temp.TicketType = ((TicketTypes)Enum.Parse(typeof(TicketTypes),item.TicketTypeID.ToString())).ToString();
                temp.AvrageTimeInMinutes = item.AverageTicketTime.Value;
                TicketsList.Add(temp);
            }

            return TicketsList;

        }

        public List<TicketsAverageTimeBySattusDTO> GetTicketsAverageTimeByStatus ()
        {
            List<TicketsAverageTimeBySattusDTO> FinalList = new List<TicketsAverageTimeBySattusDTO>();
            var OperatorAssign = context.SP_GetOpeartorAssignAverageTime();
            var OperatorAction = context.SP_GetOpeartorActionAverageTime();
            var DoctorAssign = context.SP_GetDoctorAssignAverageTime();
            var DoctorAction = context.SP_GetDoctorActionAverageTime();
            var AuditAssign = context.SP_GetAuditAssignAverageTime();
            var AuditAction = context.SP_GetAuditActionAverageTime();
            var TotalTickets = context.SP_GetTicketsAverageTimeByType();

            foreach (var item in OperatorAssign)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.PendiningOperatorAssign.ToString();

                FinalList.Add(temp);
            }
            foreach (var item in OperatorAction)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.PendiningOperatorAction.ToString();

                FinalList.Add(temp);
            }
            foreach (var item in DoctorAssign)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.PendiningDoctorAssign.ToString();

                FinalList.Add(temp);
            }
            foreach (var item in DoctorAction)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.PendiningDoctorAction.ToString();

                FinalList.Add(temp);
            }
            foreach (var item in AuditAssign)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.PendiningAuditAssign.ToString();

                FinalList.Add(temp);
            }
            foreach (var item in AuditAction)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.PendiningAuditAction.ToString();

                FinalList.Add(temp);
            }
            foreach (var item in TotalTickets)
            {
                TicketsAverageTimeBySattusDTO temp = new TicketsAverageTimeBySattusDTO();
                temp.TicketTypeID = item.TicketTypeID.Value;
                temp.AverageTime = item.AverageTicketTime.Value;
                temp.AverageTicketTimeSatsus = AverageTicketTimeSatsus.TotalAverageTime.ToString();

                FinalList.Add(temp);
            }

            return FinalList;
        }
        #endregion

       


    }
}
