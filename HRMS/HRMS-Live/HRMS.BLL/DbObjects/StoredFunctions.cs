using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HRMS.DAL.Factory;
using HRMS.Entities;

namespace HRMS.BLL.DbObjects
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

        public List<Sp_PrintLeaveTypeRequest_Result> GetVacationRequestReport (int RequestID)
        {
            return context.Sp_PrintLeaveTypeRequest(RequestID).ToList();
        }

        #endregion
    }
}
