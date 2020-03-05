using System;
using BNC.BLL.DbObjects;
using BNC.Entities;
using System.Reflection;
using BNC.Utilities;
using BNC.BLL.Logic.Tables;
using BNC.BLL.Logic.Shared_Logic;

namespace BNC.BLL.UnitOfWork
{
    public class UnitOfWork
    {
        //Sample Data
        private static int LanguageId;
        private BNCEntities Context;
        private DateTime CreationDate;
        public UnitOfWork(int languageId,DateTime creationDate, BNCEntities context)
        {
            LanguageId = languageId;
            Context = context;
            CreationDate = creationDate;
        }
        #region Functions
        public bool Submit(out Exception exResult)
        {
            exResult = new Exception();
            try
            {
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNC-BLL");
                exResult = ex;
                return false;
            }
        }
        #endregion
        //-------------------------------------------------------------------------
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
        //-------------------------------------------------------------------------
        private LogsDetailControlUserBLL logsDetailControlUserBLL;
        public LogsDetailControlUserBLL LogsDetailControlUserBLL
        {
            get
            {

                if (this.logsDetailControlUserBLL == null)
                {
                    this.logsDetailControlUserBLL = new LogsDetailControlUserBLL(Context, CreationDate);
                }
                return logsDetailControlUserBLL;
            }
        }
        //-------------------------------------------------------------------------
        private ReceivingRequestBLL receivingRequestBLL;
        public ReceivingRequestBLL ReceivingRequestBLL
        {
            get
            {

                if (this.receivingRequestBLL == null)
                {
                    this.receivingRequestBLL = new ReceivingRequestBLL(Context, CreationDate);
                }
                return receivingRequestBLL;
            }
        }
        //-------------------------------------------------------------------------
        private ProvidersBLL providersBLL;
        public ProvidersBLL ProvidersBLL
        {
            get
            {

                if (this.providersBLL == null)
                {
                    this.providersBLL = new ProvidersBLL(Context, CreationDate);
                }
                return providersBLL;
            }
        }
        //-------------------------------------------------------------------------

        private AuditCategoryBLL auditCategoryBLL;
        public AuditCategoryBLL AuditCategoryBLL
        {
            get
            {

                if (this.auditCategoryBLL == null)
                {
                    this.auditCategoryBLL = new AuditCategoryBLL(Context, CreationDate);
                }
                return auditCategoryBLL;
            }
        }
        //-------------------------------------------------------------------------

        private AuditFlowBatchDetailsBLL auditFlowBatchDetailsBLL;
        public AuditFlowBatchDetailsBLL AuditFlowBatchDetailsBLL
        {
            get
            {

                if (this.auditFlowBatchDetailsBLL == null)
                {
                    this.auditFlowBatchDetailsBLL = new AuditFlowBatchDetailsBLL(Context, CreationDate);
                }
                return auditFlowBatchDetailsBLL;
            }
        }
        //-------------------------------------------------------------------------

        private AuditFlowBLL auditFlowBLL;
        public AuditFlowBLL AuditFlowBLL
        {
            get
            {

                if (this.auditFlowBLL == null)
                {
                    this.auditFlowBLL = new AuditFlowBLL(Context, CreationDate);
                }
                return auditFlowBLL;
            }
        }
        //-------------------------------------------------------------------------

        private AuditFlowDetailsBLL auditFlowDetailsBLL;
        public AuditFlowDetailsBLL AuditFlowDetailsBLL
        {
            get
            {

                if (this.auditFlowDetailsBLL == null)
                {
                    this.auditFlowDetailsBLL = new AuditFlowDetailsBLL(Context, CreationDate);
                }
                return auditFlowDetailsBLL;
            }
        }

        //-------------------------------------------------------------------------

        private BatchAuditingRequestBLL batchAuditingRequestBLL;
        public BatchAuditingRequestBLL BatchAuditingRequestBLL
        {
            get
            {

                if (this.batchAuditingRequestBLL == null)
                {
                    this.batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
                }
                return batchAuditingRequestBLL;
            }
        }
        //-------------------------------------------------------------------------

        private BatchClosingRequestBLL batchClosingRequestBLL;
        public BatchClosingRequestBLL BatchClosingRequestBLL
        {
            get
            {

                if (this.batchClosingRequestBLL == null)
                {
                    this.batchClosingRequestBLL = new BatchClosingRequestBLL(Context, CreationDate);
                }
                return batchClosingRequestBLL;
            }
        }

        //-------------------------------------------------------------------------

        private BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        public BatchingFilterationDetailsBLL BatchingFilterationDetailsBLL
        {
            get
            {

                if (this.batchingFilterationDetailsBLL == null)
                {
                    this.batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
                }
                return batchingFilterationDetailsBLL;
            }
        }

        //-------------------------------------------------------------------------

        private BatchingRequestBLL batchingRequestBLL;
        public BatchingRequestBLL BatchingRequestBLL
        {
            get
            {

                if (this.batchingRequestBLL == null)
                {
                    this.batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
                }
                return batchingRequestBLL;
            }
        }

        //-------------------------------------------------------------------------

        private ClaimsActCategoryBLL claimsActCategoryBLL;
        public ClaimsActCategoryBLL ClaimsActCategoryBLL
        {
            get
            {

                if (this.claimsActCategoryBLL == null)
                {
                    this.claimsActCategoryBLL = new ClaimsActCategoryBLL(Context, CreationDate);
                }
                return claimsActCategoryBLL;
            }
        }

        //-------------------------------------------------------------------------

        private ClosedBatchReAdministrationRequestBLL closedBatchReAdministrationRequestBLL;
        public ClosedBatchReAdministrationRequestBLL ClosedBatchReAdministrationRequestBLL
        {
            get
            {

                if (this.closedBatchReAdministrationRequestBLL == null)
                {
                    this.closedBatchReAdministrationRequestBLL = new ClosedBatchReAdministrationRequestBLL(Context, CreationDate);
                }
                return closedBatchReAdministrationRequestBLL;
            }
        }
        //-------------------------------------------------------------------------

        private CompanyBLL companyBLL;
        public CompanyBLL CompanyBLL
        {
            get
            {

                if (this.companyBLL == null)
                {
                    this.companyBLL = new CompanyBLL(Context, CreationDate);
                }
                return companyBLL;
            }
        }
        //-------------------------------------------------------------------------

        private DataEntryAdminstrationRequestBLL dataEntryAdminstrationRequestBLL;
        public DataEntryAdminstrationRequestBLL DataEntryAdminstrationRequestBLL
        {
            get
            {

                if (this.dataEntryAdminstrationRequestBLL == null)
                {
                    this.dataEntryAdminstrationRequestBLL = new DataEntryAdminstrationRequestBLL(Context, CreationDate);
                }
                return dataEntryAdminstrationRequestBLL;
            }
        }

        //-------------------------------------------------------------------------

        private DataEntryBatchAssigningRequestBLL dataEntryBatchAssigningRequestBLL;
        public DataEntryBatchAssigningRequestBLL DataEntryBatchAssigningRequestBLL
        {
            get
            {

                if (this.dataEntryBatchAssigningRequestBLL == null)
                {
                    this.dataEntryBatchAssigningRequestBLL = new DataEntryBatchAssigningRequestBLL(Context, CreationDate);
                }
                return dataEntryBatchAssigningRequestBLL;
            }
        }


        //-------------------------------------------------------------------------

        private FilterationCategoriesBLL filterationCategoriesBLL;
        public FilterationCategoriesBLL FilterationCategoriesBLL
        {
            get
            {

                if (this.filterationCategoriesBLL == null)
                {
                    this.filterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);
                }
                return filterationCategoriesBLL;
            }
        }



        //-------------------------------------------------------------------------

        private FilterationRequestBLL filterationRequestBLL;
        public FilterationRequestBLL FilterationRequestBLL
        {
            get
            {

                if (this.filterationRequestBLL == null)
                {
                    this.filterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
                }
                return filterationRequestBLL;
            }
        }


        //-------------------------------------------------------------------------

        private FilterationRequestDetailsBLL filterationRequestDetailsBLL;
        public FilterationRequestDetailsBLL FilterationRequestDetailsBLL
        {
            get
            {

                if (this.filterationRequestDetailsBLL == null)
                {
                    this.filterationRequestDetailsBLL = new FilterationRequestDetailsBLL(Context, CreationDate);
                }
                return filterationRequestDetailsBLL;
            }
        }


        //-------------------------------------------------------------------------

        private FinancialAuditRequestBLL financialAuditRequestBLL;
        public FinancialAuditRequestBLL FinancialAuditRequestBLL
        {
            get
            {

                if (this.financialAuditRequestBLL == null)
                {
                    this.financialAuditRequestBLL = new FinancialAuditRequestBLL(Context, CreationDate);
                }
                return financialAuditRequestBLL;
            }
        }
        //-------------------------------------------------------------------------

        private MedicalAuditRequestBLL medicalAuditRequestBLL;
        public MedicalAuditRequestBLL MedicalAuditRequestBLL
        {
            get
            {

                if (this.medicalAuditRequestBLL == null)
                {
                    this.medicalAuditRequestBLL = new MedicalAuditRequestBLL(Context, CreationDate);
                }
                return medicalAuditRequestBLL;
            }
        }

        //-------------------------------------------------------------------------

        private MIAuditRequestBLL mIAuditRequestBLL;
        public MIAuditRequestBLL MIAuditRequestBLL
        {
            get
            {

                if (this.mIAuditRequestBLL == null)
                {
                    this.mIAuditRequestBLL = new MIAuditRequestBLL(Context, CreationDate);
                }
                return mIAuditRequestBLL;
            }
        }

        //-------------------------------------------------------------------------

        private ProviderContactBLL providerContactBLL;
        public ProviderContactBLL ProviderContactBLL
        {
            get
            {

                if (this.providerContactBLL == null)
                {
                    this.providerContactBLL = new ProviderContactBLL(Context, CreationDate);
                }
                return providerContactBLL;
            }
        }

        //-------------------------------------------------------------------------

        private ReconciliationAuditRequestBLL reconciliationAuditRequestBLL;
        public ReconciliationAuditRequestBLL ReconciliationAuditRequestBLL
        {
            get
            {

                if (this.reconciliationAuditRequestBLL == null)
                {
                    this.reconciliationAuditRequestBLL = new ReconciliationAuditRequestBLL(Context, CreationDate);
                }
                return reconciliationAuditRequestBLL;
            }
        }

        //-------------------------------------------------------------------------

        private ReleaseActionBLL releaseActionBLL;
        public ReleaseActionBLL ReleaseActionBLL
        {
            get
            {

                if (this.releaseActionBLL == null)
                {
                    this.releaseActionBLL = new ReleaseActionBLL(Context, CreationDate);
                }
                return releaseActionBLL;
            }
        }

        //-------------------------------------------------------------------------

        private StatusBLL statusBLL;
        public StatusBLL StatusBLL
        {
            get
            {

                if (this.statusBLL == null)
                {
                    this.statusBLL = new StatusBLL(Context, CreationDate);
                }
                return statusBLL;
            }
        }

        //-------------------------------------------------------------------------

        private InsuranceSystemBLL insuranceSystemBLL;
        public InsuranceSystemBLL InsuranceSystemBLL
        {
            get
            {
                if (this.insuranceSystemBLL == null)
                {
                    this.insuranceSystemBLL = new InsuranceSystemBLL(Context, CreationDate);
                }
                return insuranceSystemBLL;
            }
        }
        //-------------------------------------------------------------------------
        private TeamBLL teamBLL;
        public TeamBLL TeamBLL
        {
            get
            {

                if (this.teamBLL == null)
                {
                    this.teamBLL = new TeamBLL(Context, CreationDate);
                }
                return teamBLL;
            }
        }
        //-------------------------------------------------------------------------

        private SystemEntitiesBLL systemEntitiesBLL;
        public SystemEntitiesBLL SystemEntitiesBLL
        {
            get
            {

                if (this.systemEntitiesBLL == null)
                {
                    this.systemEntitiesBLL = new SystemEntitiesBLL(Context, CreationDate);
                }
                return systemEntitiesBLL;
            }
        }

        //-------------------------------------------------------------------------

        private TransferReasonBLL transferReasonBLL;
        public TransferReasonBLL TransferReasonBLL
        {
            get
            {

                if (this.transferReasonBLL == null)
                {
                    this.transferReasonBLL = new TransferReasonBLL(Context, CreationDate);
                }
                return transferReasonBLL;
            }
        }
        //-------------------------------------------------------------------------

        private TransferRequestHistoryBLL transferRequestHistoryBLL;
        public TransferRequestHistoryBLL TransferRequestHistoryBLL
        {
            get
            {

                if (this.transferRequestHistoryBLL == null)
                {
                    this.transferRequestHistoryBLL = new TransferRequestHistoryBLL(Context, CreationDate);
                }
                return transferRequestHistoryBLL;
            }
        }

        //-------------------------------------------------------------------------

        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get
            {

                if (this.userBLL == null)
                {
                    this.userBLL = new UserBLL(Context, CreationDate);
                }
                return userBLL;
            }
        }
        //-------------------------------------------------------------------------

        private SharedReceivingFilterationBLL sharedReceivingFilterationBLL;
        public SharedReceivingFilterationBLL SharedReceivingFilterationBLL
        {
            get
            {

                if (this.sharedReceivingFilterationBLL == null)
                {
                    this.sharedReceivingFilterationBLL = new SharedReceivingFilterationBLL(Context, CreationDate);
                }
                return sharedReceivingFilterationBLL;
            }
        }
        //-------------------------------------------------------------------------

        private SharedAuditBatchBLL sharedAuditBatchBLL;
        public SharedAuditBatchBLL SharedAuditBatchBLL
        {
            get
            {

                if (this.sharedAuditBatchBLL == null)
                {
                    this.sharedAuditBatchBLL = new SharedAuditBatchBLL(Context, CreationDate);
                }
                return sharedAuditBatchBLL;
            }
        }
        //-------------------------------------------------------------------------

        private SharedDataEntryBatchBLL sharedDataEntryBatchBLL;
        public SharedDataEntryBatchBLL SharedDataEntryBatchBLL
        {
            get
            {

                if (this.sharedDataEntryBatchBLL == null)
                {
                    this.sharedDataEntryBatchBLL = new SharedDataEntryBatchBLL(Context, CreationDate);
                }
                return sharedDataEntryBatchBLL;
            }
        }
        //-------------------------------------------------------------------------

        private SharedSearchBLL sharedSearchBLL;
        public SharedSearchBLL SharedSearchBLL
        {
            get
            {
                if (this.sharedSearchBLL == null)
                {
                    this.sharedSearchBLL = new SharedSearchBLL(Context, CreationDate);
                }
                return sharedSearchBLL;
            }
        }
        //-------------------------------------------------------------------------
        private SharedBncBLL sharedBncBLL;
        public SharedBncBLL SharedBncBLL
        {
            get
            {

                if (this.sharedBncBLL == null)
                {
                    this.sharedBncBLL = new SharedBncBLL(Context, CreationDate);
                }
                return sharedBncBLL;
            }
        }
        //-------------------------------------------------------------------------
        private MapperBLL mapperBLL;
        public MapperBLL MapperBLL
        {
            get
            {

                if (this.mapperBLL == null)
                {
                    this.mapperBLL = new MapperBLL(Context, CreationDate);
                }
                return mapperBLL;
            }
        }
        //-------------------------------------------------------------------------
        private SharedProviderBLL sharedProviderBLL;
        public SharedProviderBLL SharedProviderBLL
        {
            get
            {
                if (this.sharedProviderBLL == null)
                {
                    this.sharedProviderBLL = new SharedProviderBLL(Context, CreationDate);
                }
                return sharedProviderBLL;
            }
        }
        //-------------------------------------------------------------------------
        //special approval 
        private SPRequestBLL spRequestBLL;
        public SPRequestBLL SPRequestBLL
        {
            get
            {
                if (this.spRequestBLL == null)
                {
                    this.spRequestBLL = new SPRequestBLL(Context, CreationDate);
                }
                return spRequestBLL;
            }
        }

        private SPStatusBLL sPStatusBLL;
        public SPStatusBLL SPStatusBLL
        {
            get
            {
                if (this.sPStatusBLL == null)
                {
                    this.sPStatusBLL = new SPStatusBLL(Context, CreationDate);
                }
                return sPStatusBLL;
            }
        }

        private SPReasonBLL sPReasonBLL;
        public SPReasonBLL SPReasonBLL
        {
            get
            {
                if (this.sPReasonBLL == null)
                {
                    this.sPReasonBLL = new SPReasonBLL(Context, CreationDate);
                }
                return sPReasonBLL;
            }
        }


        private SpReqReasonBLL spReqReasonBLL;
        public SpReqReasonBLL SpReqReasonBLL
        {
            get
            {
                if (this.spReqReasonBLL == null)
                {
                    this.spReqReasonBLL = new SpReqReasonBLL(Context, CreationDate);
                }
                return spReqReasonBLL;
            }
        }

        private GovernmentBLL governmentBLL;
        public GovernmentBLL GovernmentBLL
        {
            get
            {
                if (this.governmentBLL == null)
                {
                    this.governmentBLL = new GovernmentBLL(Context, CreationDate);
                }
                return governmentBLL;
            }
        }
        //-------------------------------------------------------------------------
        private SpecialApprovalFinincalReqBLL specialApprovalFinincalReqBLL;
        public SpecialApprovalFinincalReqBLL SpecialApprovalFinincalReqBLL
        {
            get
            {
                if (this.specialApprovalFinincalReqBLL == null)
                {
                    this.specialApprovalFinincalReqBLL = new SpecialApprovalFinincalReqBLL(Context, CreationDate);
                }
                return specialApprovalFinincalReqBLL;
            }
        }
        //-------------------------------------------------------------------------
        private SpecialApprovalMedicalReqBLL specialApprovalMedicalReqBLL;
        public SpecialApprovalMedicalReqBLL SpecialApprovalMedicalReqBLL
        {
            get
            {
                if (this.specialApprovalMedicalReqBLL == null)
                {
                    this.specialApprovalMedicalReqBLL = new SpecialApprovalMedicalReqBLL(Context, CreationDate);
                }
                return specialApprovalMedicalReqBLL;
            }
        }
        //-------------------------------------------------------------------------

        private SPRequestUserBLL sPRequestUserBLL;
        public SPRequestUserBLL SPRequestUserBLL
        {
            get
            {
                if (this.sPRequestUserBLL == null)
                {
                    this.sPRequestUserBLL = new SPRequestUserBLL(Context, CreationDate);
                }
                return sPRequestUserBLL;
            }
        }
        //-------------------------------------------------------------------------
        private SPAuditRequestBLL sPAuditRequestBLL;
        public SPAuditRequestBLL SPAuditRequestBLL
        {
            get
            {
                if (this.sPAuditRequestBLL == null)
                {
                    this.sPAuditRequestBLL = new SPAuditRequestBLL(Context, CreationDate);
                }
                return sPAuditRequestBLL;
            }
        }
        //-------------------------------------------------------------------------
        private SpUserBLL spUserBLL;
        public SpUserBLL SpUserBLL
        {
            get
            {
                if (this.spUserBLL == null)
                {
                    this.spUserBLL = new SpUserBLL(Context, CreationDate);
                }
                return spUserBLL;
            }
        }
        //-------------------------------------------------------------------------
        private ProviderTypeBLL providerTypeBLL;

        public ProviderTypeBLL ProviderTypeBLL
        {
            get
            {
                if (this.providerTypeBLL == null)
                {
                    this.providerTypeBLL = new ProviderTypeBLL(Context, CreationDate);
                }
                return providerTypeBLL;
            }
        }
    }

}