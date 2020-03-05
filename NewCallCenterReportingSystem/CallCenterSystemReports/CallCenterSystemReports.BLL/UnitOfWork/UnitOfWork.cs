using System;
using CallCenterSystemReports.BLL.DbObjects;
using CallCenterSystemReports.Entities;
using CallCenterSystemReports.BLL.Logic;

namespace CallCenterSystemReports.BLL.UnitOfWork
{
    public class UnitOfWork
    {
        //Sample Data
        private static int LanguageId;
        private int UserId;
        private DateTime CreateDate;
        private bool IsActive;
        private PhNetworkEntities Context;  

        public UnitOfWork(int userId, DateTime createDate, bool isActive, int languageId, PhNetworkEntities context)
        {
            LanguageId = languageId;
            UserId = userId;
            CreateDate = createDate;
            IsActive = isActive;
            Context = context;
        }
        #region Functions
        public bool Submit()
        {
            try
            {
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        private StoredFunctions storedFunctions;
        public StoredFunctions StoredFunctions
        {
            get
            {

                if (this.storedFunctions == null)
                {
                    this.storedFunctions = new StoredFunctions(LanguageId);
                }
                return storedFunctions;
            }
        }

        private EmailApprovalRequestsBLL emailApprovalRequestsBLL;
        public EmailApprovalRequestsBLL EmailApprovalRequestsBLL
        {
            get
            {

                if (this.emailApprovalRequestsBLL == null)
                {
                    this.emailApprovalRequestsBLL = new EmailApprovalRequestsBLL(Context);
                }
                return emailApprovalRequestsBLL;
            }
        }


    }
}