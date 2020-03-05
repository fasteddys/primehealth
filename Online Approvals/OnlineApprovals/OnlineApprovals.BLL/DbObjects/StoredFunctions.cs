using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlineApprovals.DAL.Factory;
using OnlineApprovals.Entities;
using OnlineApprovals.DTOs;
using static OnlineApprovals.BLL.StaticData.StaticEnums;
using System.Runtime.Caching;

namespace OnlineApprovals.BLL.DbObjects
{
    public sealed class StoredFunctions : ContainerContextFactory, IDisposable
    {
        public int LanguageId { get; set; }
        private const string ListedDrugsCache = "ListedDrugsCacheKey";

        #region Constructors

        public StoredFunctions()
        {

        }
        public List< SP_Select_OnlineApprovalsRequests_Result> Select_OnlineApprovalsRequests_Result(int ID)

        {
            return context.SP_Select_OnlineApprovalsRequests(ID).ToList();
           
        }
        public List< SP_Select_OnlineApprovalsRequestDetails_Result> Select_OnlineApprovalsRequestDetailss(int ID)

        {
           return context.SP_Select_OnlineApprovalsRequestDetails(ID).ToList();

        }
        public List<SP_Select_OnlineApprovalsAttachment_Result> Select_OnlineApprovalsAttachment(int ID)

        {
            return context.SP_Select_OnlineApprovalsAttachment(ID).ToList();

        }

        public List<SP_Select_OnlineApprovalsDrugList_Result> Select_OnlineApprovalsDrugList()

        {
            ObjectCache cache = MemoryCache.Default;
            if (cache.Contains(ListedDrugsCache))
            {
                return (List<SP_Select_OnlineApprovalsDrugList_Result>)cache.Get(ListedDrugsCache);
            }
            else
            {
                var ListedDrugs = context.SP_Select_OnlineApprovalsDrugList().ToList();
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(30);
                cache.Add(ListedDrugsCache, ListedDrugs, cacheItemPolicy);
                return ListedDrugs ;
            }


        }

    


        public SP_Select_OnlineApprovalByAccountID_Result Select_OnlineApprovalsProviderByAccountID(string AccountID)

        {
            return context.SP_Select_OnlineApprovalByAccountID(AccountID).FirstOrDefault();

        }


        public SP_Select_OnlineApprovalsProvidersByID_Result Select_OnlineApprovalsProviderByID(int? ID)

        {
            return context.SP_Select_OnlineApprovalsProvidersByID(ID).FirstOrDefault();

        }

        public SP_OnlineApprovals_InovicesByInvoiceID_Result OnlineApprovals_InovicesByInvoiceID(int InvoieID)

        {
            return context.SP_OnlineApprovals_InovicesByInvoiceID(InvoieID).FirstOrDefault();

        }
        public SP_OnlineApprovals_InovicesByRequestID_Result OnlineApprovals_InovicesByRequestID(int RequestID)

        {

          var x=  context.SP_OnlineApprovals_InovicesByRequestID(RequestID).FirstOrDefault();
            return context.SP_OnlineApprovals_InovicesByRequestID(RequestID).FirstOrDefault();

        }

        public List<SP_OnlineApprovals_InovicesByMedicalID_Result> OnlineApprovals_InovicesByMedicalID(int MedicalID)

        {
            return context.SP_OnlineApprovals_InovicesByMedicalID(MedicalID).ToList();

        }
        public List<SP_Select_OnlineApprovalsDemandDrugs_Result> Select_OnlineApprovalsDemandDrugs_Result(int RequestID)
        {
            return context.SP_Select_OnlineApprovalsDemandDrugs(RequestID).ToList();


        }

        public List<SP_OnlineApprovalsSearch_Result> Search_OnlineApprovals
            (string MemberName,string ClientName,string ClaimNumber,string Diagnose,DateTime CreationDate, DateTime CloseDate,string IvrNumber,
            int RequestTypeID, int RequestStatusID,string DrugName)
        {
            return context.SP_OnlineApprovalsSearch( MemberName,  ClientName,  ClaimNumber,  Diagnose,
                CreationDate.ToString(),  CloseDate.ToString(),  IvrNumber,  RequestTypeID.ToString(),  RequestStatusID.ToString(), DrugName).ToList();


        }
        public int? Select_CountOfALLRequestByProvideID(int? ProviderID)

        {
            return context.SP_OnlineApprovals_CountTotalRequestsByProviderID(ProviderID).FirstOrDefault();

        }

        public int? Select_CountOfALLRequestByProvideIDAndStatusID(int Provider, int Status)

        {
            return context.SP_OnlineApprovals_CountRequestsByStatusAndProvider(Provider, Status).FirstOrDefault();

        }




        public List< SP_OnlineApprovals_RequestsByStatusAndProvider_Result> Select_OnlineApprovalsProvideIDAndStatusID(int ProviderID,int StatusID)

        {
            return context.SP_OnlineApprovals_RequestsByStatusAndProvider(ProviderID, StatusID).ToList();

        }

        public int? Select_CountClaimsPerCurrentMonth(int? ProviderID)

        {
            return context.SP_OnlineApprovals_ClaimsPerCurrentMonth(ProviderID).FirstOrDefault();

        }
        public int? Select_CountTotal_Members(int? ProviderID)

        {
            return context.SP_OnlineApprovals_Total_Members(ProviderID).FirstOrDefault();

        }
        public List< SP_OnlineApprovals_RequestsByprovider_Result >OnlineApprovals_RequestsByprovider(int? ProviderID)

        {
            return context.SP_OnlineApprovals_RequestsByprovider(ProviderID).ToList();

        }
        public List<SP_Provider_Login_Result> SP_Provider_Login(string AccountID ,string Password)

        {
            return context.SP_Provider_Login(AccountID, Password).ToList();

        }
        #endregion

        public StoredFunctions(int lang)
        {
            LanguageId = lang;
        }
    }
}
