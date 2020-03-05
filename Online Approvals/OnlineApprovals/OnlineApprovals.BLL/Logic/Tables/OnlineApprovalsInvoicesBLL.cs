using OnlineApprovals.BLL.CommonFunctions;
using OnlineApprovals.BLL.DbObjects;
using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.DTOs;
using OnlineApprovals.Entities;
using OnlineApprovals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.BLL.Logic.Tables
{
    public class OnlineApprovalsInvoicesBLL : Repository<OnlineApprovals_Inovices>
    {
        public readonly StoredFunctions StoredFunction;
        public readonly DTOMapper Mapper;
        OnlineApprovalRequestBLL RequestBLL;
        OnlineApprovalsProviderBLL ProviderBLL;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsInvoicesBLL(PhNetworkEntities Context,int ProviderTypeID,int ProviderID,string LoginKey) : base(Context)
        {
            Mapper = new DTOMapper();
               StoredFunction = new StoredFunctions();
            RequestBLL = new OnlineApprovalRequestBLL(Context, ProviderTypeID, ProviderID, LoginKey);
            ProviderBLL = new OnlineApprovalsProviderBLL(Context, ProviderTypeID, ProviderID, LoginKey);
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }
        public void AddNewInvoice(OnlineApprovals_Requests Request, List<OnlineApprovals_DemandedDrugsDetails> DrugDetailsObj)
        {
            //try
            //{
                ALLRequestDataDTO AllRequestData = new ALLRequestDataDTO();
                AllRequestData.Request = Mapper.MapStoredProsedureToRequest(Request);
               AllRequestData.DemandedDrugs =Mapper. MapDrugsDetailDTOToDrugsDetailDTO(DrugDetailsObj);




                OnlineApprovals_Inovices Invoice = new OnlineApprovals_Inovices
                {

                    ClaimNumber = Request.ClaimNumber.Value,
                    ClientName = Request.ClientName,
                    CoInsuranceValue = Request.CoInsurancePercentage.Value,
                    Diagnose = Request.Diagnose,
                    InvoiceDate = DateTime.Now,
                    IVRNumber = Request.IVRNumber,
                    MedicalID = Request.MedicalID.Value,
                    MemberName = Request.MemberName,
                    Notes = Request.Notes,
                    RequestID = Request.RequestID,
                    ProviderType = Request.ProviderTypeID.ToString(),
                    ProviderName = ProviderBLL.GetProviderByID().PharmAddress,
                    AssignedUser = Request.CallCenterAssignee,
                    RequestCloseTime = Request.CloseTime,
                    GrandTotal =(decimal) Request.TotalApprovalPrice /*MatematicalCalculations.CalculateGrandTotal(AllRequestData)[0]*/,
                    Total = (decimal)Request.TotalPayments/*MatematicalCalculations.CalculateGrandTotal(AllRequestData)[1]*/

                };

                Add(Invoice);
           // }
            //catch(Exception ex) { }




        }

        public InovicesDTO GetInvoiceByInvoiceID(int InvoiceID)
        {
            return Mapper.MapInvoiceToDrugInovicesDTO( StoredFunction.OnlineApprovals_InovicesByInvoiceID(InvoiceID));

        }
        public List< InovicesDTO> GetInvoiceByInvoiceIDByMedical(int MedicalID)
        {
            return Mapper.MapInvoiceToDrugInovicesDTOMedicalID(StoredFunction.OnlineApprovals_InovicesByMedicalID(MedicalID));

        }
        public InovicesDTO GetInvoiceByRequest(int RequestID)
        {
            return Mapper.MapInvoiceToDrugInovicesDTOByRequestID(StoredFunction.OnlineApprovals_InovicesByRequestID(RequestID));

        }

    }
}
