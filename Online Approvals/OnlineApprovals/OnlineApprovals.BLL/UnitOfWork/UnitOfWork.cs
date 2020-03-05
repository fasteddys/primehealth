using System;
using OnlineApprovals.BLL.DbObjects;
using OnlineApprovals.Entities;
using OnlineApprovals.BLL.Logic;
using OnlineApprovals.BLL.Logic.Tables;

namespace OnlineApprovals.BLL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        //Unit OF Work Settings
        private bool disposed = false;
        //Sample Data
        private int ProviderTypeID;
        private int ProviderID;
        private string LoginKey;
        private PhNetworkEntities Context;  

        public UnitOfWork(PhNetworkEntities context,int providerTypeID,int providerID,string loginKey)
        {
            Context = context;
            ProviderTypeID = providerTypeID;
            ProviderID = providerID;
            LoginKey = loginKey;
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
                    this.storedFunctions = new StoredFunctions();
                }
                return storedFunctions;
            }
        }


        private OnlineApprovalRequestBLL onlineApprovalRequestBLL;
        public OnlineApprovalRequestBLL OnlineApprovalRequestBLL
        {
            get
            {

                if (this.onlineApprovalRequestBLL == null)
                {
                    this.onlineApprovalRequestBLL = new OnlineApprovalRequestBLL(Context,ProviderTypeID,ProviderID,LoginKey);
                }
                return onlineApprovalRequestBLL;
            }
        }

        private OnlineApprovalsRequestAttachmentBLL onlineApprovalRequestAttachmentBLL;
        public OnlineApprovalsRequestAttachmentBLL OnlineApprovalRequestAttachmentBLL
        {
            get
            {

                if (this.onlineApprovalRequestAttachmentBLL == null)
                {
                    this.onlineApprovalRequestAttachmentBLL = new OnlineApprovalsRequestAttachmentBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalRequestAttachmentBLL;
            }
        }

        private OnlineApprovalsRequestDetailsBLL onlineApprovalRequestDetailsBLL;
        public OnlineApprovalsRequestDetailsBLL OnlineApprovalRequestDetailsBLL
        {
            get
            {

                if (this.onlineApprovalRequestDetailsBLL == null)
                {
                    this.onlineApprovalRequestDetailsBLL = new OnlineApprovalsRequestDetailsBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalRequestDetailsBLL;
            }
        }


        private OnlineApprovalsConfigrationBLL onlineApprovalsConfigrationBLL;
        public OnlineApprovalsConfigrationBLL OnlineApprovalsConfigrationBLL
        {
            get
            {

                if (this.onlineApprovalsConfigrationBLL == null)
                {
                    this.onlineApprovalsConfigrationBLL = new OnlineApprovalsConfigrationBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalsConfigrationBLL;
            }
        }



        private OnlineApprovalsDrugsListDIMBLL onlineApprovalsDrugsListDIMBLL;
        public OnlineApprovalsDrugsListDIMBLL OnlineApprovalsDrugsListDIMBLL
        {
            get
            {

                if (this.onlineApprovalsDrugsListDIMBLL == null)
                {
                    this.onlineApprovalsDrugsListDIMBLL = new OnlineApprovalsDrugsListDIMBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalsDrugsListDIMBLL;
            }
        }
        private OnlineApprovalsInvoicesBLL onlineApprovalsInvoicesBLL;
        public OnlineApprovalsInvoicesBLL OnlineApprovalsInvoicesBLL
        {
            get
            {

                if (this.onlineApprovalsInvoicesBLL == null)
                {
                    this.onlineApprovalsInvoicesBLL = new OnlineApprovalsInvoicesBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalsInvoicesBLL;
            }
        }
        private OnlineApprovalsDrugsDetailBLL onlineApprovalsDrugsDetailBLL;


        public OnlineApprovalsDrugsDetailBLL OnlineApprovalsDrugsDetailBLL
        {
            get
            {

                if (this.onlineApprovalsDrugsDetailBLL == null)
                {
                    this.onlineApprovalsDrugsDetailBLL = new OnlineApprovalsDrugsDetailBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalsDrugsDetailBLL;
            }
        }

        private OnlineApprovalsProviderBLL onlineApprovalsProviderBLL;


        public OnlineApprovalsProviderBLL OnlineApprovalsProviderBLL
        {
            get
            {

                if (this.onlineApprovalsProviderBLL == null)
                {
                    this.onlineApprovalsProviderBLL = new OnlineApprovalsProviderBLL(Context, ProviderTypeID, ProviderID, LoginKey);
                }
                return onlineApprovalsProviderBLL;
            }
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}