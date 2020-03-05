using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineApprovals.Utilities;
using OnlineApprovals.Entities;
using OnlineApprovals.DTOs;


namespace OnlineApprovals.BLL.CommonFunctions
{
    public class DTOMapper
    {
        public OnlineApprovals_Requests MapRequestDTO(RequestDTO RequestDTOObj)
        {
            OnlineApprovals_Requests RequestObj = new OnlineApprovals_Requests();

            RequestObj.ProviderID = RequestDTOObj.ProviderID;
            RequestObj.ProviderTypeID = RequestDTOObj.ProviderType;
            RequestObj.MedicalID = RequestDTOObj.MedicalID;
            RequestObj.MemberName = RequestDTOObj.MemberName;
            RequestObj.MobileNumber = RequestDTOObj.MobileNumber;
            RequestObj.ClientName = RequestDTOObj.ClientName;
            RequestObj.ClaimNumber = RequestDTOObj.ClaimNumber;
            RequestObj.ClaimDate = RequestDTOObj.ClaimDate;
            RequestObj.CoInsurancePercentage = RequestDTOObj.CoInsurancePerc;
            RequestObj.Diagnose = RequestDTOObj.Diagnose;
            RequestObj.Notes = RequestDTOObj.Note;
            RequestObj.RequestCreationTime = RequestDTOObj.CreationTime;
            RequestObj.RequestStatusID = RequestDTOObj.StatusID;
            RequestObj.IsFinnacialApproval = RequestDTOObj.FinnacialApproval;
            RequestObj.IsMedicalApproval = RequestDTOObj.MedicalApproval;
            RequestObj.IVRNumber = RequestDTOObj.IVRNumber;
            RequestObj.Delivered = RequestDTOObj.Delivered;
            RequestObj.Seen = RequestDTOObj.Seen;
            RequestObj.SeenByUserID = RequestDTOObj.SeenByUserID;
            RequestObj.IsDeleted = RequestDTOObj.Deleted;
            RequestObj.RequestTypeID = RequestDTOObj.RequestTypeID;
            RequestObj.TotalPayments = RequestDTOObj.TotalPayments;
            RequestObj.TotalMemberpays = RequestDTOObj.TotalMemberpays;
            RequestObj.TotalApprovalPrice = RequestDTOObj.TotalApprovalPrice;
            RequestObj.CallCenterAssignee = RequestDTOObj.CallCenterAssignee;
            RequestDTOObj.CloseTime = RequestDTOObj.CloseTime;
            RequestDTOObj.ProviderName = RequestDTOObj.ProviderName;
            return RequestObj;
        }






        public OnlineApprovals_RequestDetails MapRequestDetailsDTO(RequestDetailsDTO RequestDetailsDTOObj)
        {
            OnlineApprovals_RequestDetails RequestDetailsObj = new OnlineApprovals_RequestDetails();

            RequestDetailsObj.RequestID = RequestDetailsDTOObj.RequestID;
            RequestDetailsObj.RequestDetailsTypeID = RequestDetailsDTOObj.RequestDetailsType;
            RequestDetailsObj.Notes = RequestDetailsDTOObj.Notes;
            RequestDetailsObj.DetailsDate = RequestDetailsDTOObj.DetailsDate;
            RequestDetailsObj.Delivered = RequestDetailsDTOObj.Delivered;
            RequestDetailsObj.Seen = RequestDetailsDTOObj.Seen;
            RequestDetailsObj.IsDeleted = RequestDetailsDTOObj.IsDeleted;
            RequestDetailsObj.UserID = RequestDetailsDTOObj.UserID.Value;
            RequestDetailsObj.UserTypeID = RequestDetailsDTOObj.UserTypeID;

            return RequestDetailsObj;
        }
        public OnlineApprovals_Requests MapStoredProsedureToRequest(SP_Select_OnlineApprovalsRequests_Result SP_object)
        {

            return new OnlineApprovals_Requests
            {
                ClaimDate = SP_object.ClaimDate,
                ClientName = SP_object.ClientName,
                ClaimNumber = SP_object.ClaimNumber,
                CoInsurancePercentage = SP_object.CoInsurancePercentage,
                Delivered = SP_object.Delivered,
                Seen = SP_object.Seen,
                RequestID = SP_object.RequestID,
                Diagnose = SP_object.Diagnose,
                IsDeleted = SP_object.IsDeleted,
                IsFinnacialApproval = SP_object.IsFinnacialApproval,
                IsMedicalApproval = SP_object.IsMedicalApproval,
                IVRNumber = SP_object.IVRNumber,
                MedicalID = SP_object.MedicalID,
                MemberName = SP_object.MemberName,
                MobileNumber = SP_object.MobileNumber,
                Notes = SP_object.Notes,
                ProviderID = SP_object.ProviderID,
                ProviderTypeID = SP_object.ProviderTypeID,
                RequestCreationTime = SP_object.RequestCreationTime,
                RequestStatusID = SP_object.RequestStatusID,
                RequestTypeID = SP_object.RequestTypeID,
                SeenByUserID = SP_object.SeenByUserID,
                 CallCenterAssignee= SP_object.CallCenterAssignee,
                  CloseTime= SP_object.CloseTime
                   
            };


        }

        public RequestDTO MapStoredProsedureToRequest(OnlineApprovals_Requests Request)
        {
            return new RequestDTO
            {
                ClaimDate = Request.ClaimDate.Value,
                ClientName = Request.ClientName,
                ClaimNumber = Request.ClaimNumber,
                CoInsurancePerc = Request.CoInsurancePercentage,
                Delivered = Request.Delivered,
                Seen = Request.Seen,
                ID = Request.RequestID,
                Diagnose = Request.Diagnose,
                Deleted = Request.IsDeleted,
                FinnacialApproval = Request.IsFinnacialApproval,
                MedicalApproval = Request.IsMedicalApproval,
                IVRNumber = Request.IVRNumber,
                MedicalID = Request.MedicalID,
                MemberName = Request.MemberName,
                MobileNumber = Request.MobileNumber,
                Note = Request.Notes,
                ProviderID = Request.ProviderID,
                ProviderType = Request.ProviderTypeID,
                CreationTime = Request.RequestCreationTime,
                StatusID = Request.RequestStatusID,
                RequestTypeID = Request.RequestTypeID,
                SeenByUserID = Request.SeenByUserID
                , CallCenterAssignee = Request.CallCenterAssignee,
                CloseTime = Request.CloseTime
                 

            };


        }
        public List<RequestDTO> MapListStoredProsedureToRequest(List<SP_OnlineApprovals_RequestsByStatusAndProvider_Result> RequestList)
        {

            List<RequestDTO> ListOfRequestDTO = new List<RequestDTO>();

            foreach (var Request in RequestList)
            {
                ListOfRequestDTO.Add(
                    new RequestDTO
                    {
                        ClaimDate = Request.ClaimDate.Value,
                        ClientName = Request.ClientName,
                        ClaimNumber = Request.ClaimNumber,
                        CoInsurancePerc = Request.CoInsurancePercentage,
                        Delivered = Request.Delivered,
                        Seen = Request.Seen,
                        ID = Request.RequestID,
                        Diagnose = Request.Diagnose,
                        Deleted = Request.IsDeleted,
                        FinnacialApproval = Request.IsFinnacialApproval,
                        MedicalApproval = Request.IsMedicalApproval,
                        IVRNumber = Request.IVRNumber,
                        MedicalID = Request.MedicalID,
                        MemberName = Request.MemberName,
                        MobileNumber = Request.MobileNumber,
                        Note = Request.Notes,
                        ProviderID = Request.ProviderID,
                        ProviderType = Request.ProviderTypeID,
                        CreationTime = Request.RequestCreationTime,
                        StatusID = Request.RequestStatusID,
                        RequestTypeID = Request.RequestTypeID,
                        SeenByUserID = Request.SeenByUserID,
                        CallCenterAssignee = Request.CallCenterAssignee,
                        CloseTime = Request.CloseTime,


                    });

            }
            return ListOfRequestDTO;


        }
        public List<RequestDTO> MapSearchStoredProsedureToRequest(List<SP_OnlineApprovalsSearch_Result> RequestList)
        {

            List<RequestDTO> ListOfRequestDTO = new List<RequestDTO>();

            foreach (var Request in RequestList)
            {
                ListOfRequestDTO.Add(
                    new RequestDTO
                    {
                        ClaimDate = Request.ClaimDate.Value,
                        ClientName = Request.ClientName,
                        ClaimNumber = Request.ClaimNumber,
                        CoInsurancePerc = Request.CoInsurancePercentage,
                        Delivered = Request.Delivered,
                        Seen = Request.Seen,
                        ID = Request.RequestID,
                        Diagnose = Request.Diagnose,
                        Deleted = Request.IsDeleted,
                        FinnacialApproval = Request.IsFinnacialApproval,
                        MedicalApproval = Request.IsMedicalApproval,
                        IVRNumber = Request.IVRNumber,
                        MedicalID = Request.MedicalID,
                        MemberName = Request.MemberName,
                        MobileNumber = Request.MobileNumber,
                        Note = Request.Notes,
                        ProviderID = Request.ProviderID,
                        ProviderType = Request.ProviderTypeID,
                        CreationTime = Request.RequestCreationTime,
                        StatusID = Request.RequestStatusID,
                        RequestTypeID = Request.RequestTypeID,
                        SeenByUserID = Request.SeenByUserID,
                        CallCenterAssignee = Request.CallCenterAssignee,
                        CloseTime = Request.CloseTime,
                        

                    });

            }
            return ListOfRequestDTO;


        }

        public List<RequestDetailsDTO> MapListStoredProsedureToRequest(List<SP_Select_OnlineApprovalsRequestDetails_Result> RequestList)
        {

            List<RequestDetailsDTO> ListOfRequestDetailsDTO = new List<RequestDetailsDTO>();

            foreach (var RequestDetails in RequestList)
            {
                ListOfRequestDetailsDTO.Add(
                    new RequestDetailsDTO
                    {
                        Delivered = RequestDetails.Delivered,
                        ID = RequestDetails.RequestDetailsID,
                        DetailsDate = RequestDetails.DetailsDate,
                        IsDeleted = RequestDetails.IsDeleted,
                        Notes = RequestDetails.Notes,
                        RequestDetailsType = RequestDetails.RequestDetailsTypeID,
                        Seen = RequestDetails.Seen,
                        RequestID = RequestDetails.RequestID,
                        UserID = RequestDetails.UserID,
                        UserTypeID = RequestDetails.UserTypeID

                    });

            }
            return ListOfRequestDetailsDTO;


        }

        public List<RequestDetailsDTO> MapListRequestToRequestDTO(List<OnlineApprovals_RequestDetails> RequestList)
        {

            List<RequestDetailsDTO> ListOfRequestDetailsDTO = new List<RequestDetailsDTO>();

            foreach (var RequestDetails in RequestList)
            {
                ListOfRequestDetailsDTO.Add(
                    new RequestDetailsDTO
                    {
                        Delivered = RequestDetails.Delivered,
                        ID = RequestDetails.RequestID,
                        DetailsDate = RequestDetails.DetailsDate,
                        IsDeleted = RequestDetails.IsDeleted,
                        Notes = RequestDetails.Notes,
                        RequestDetailsType = RequestDetails.RequestDetailsTypeID,
                        Seen = RequestDetails.Seen,
                        RequestID = RequestDetails.RequestID,
                        UserID = RequestDetails.UserID,
                        UserTypeID = RequestDetails.UserTypeID

                    });

            }
            return ListOfRequestDetailsDTO;


        
    


            foreach (var RequestDetails in RequestList)
            {
                ListOfRequestDetailsDTO.Add(
                    new RequestDetailsDTO
                    {
                        Delivered = RequestDetails.Delivered,
                        ID = RequestDetails.RequestID,
                        DetailsDate = RequestDetails.DetailsDate,
                        IsDeleted = RequestDetails.IsDeleted,
                        Notes = RequestDetails.Notes,
                        RequestDetailsType = RequestDetails.RequestDetailsTypeID,
                        Seen = RequestDetails.Seen,
                        RequestID = RequestDetails.RequestID,
                        UserID = RequestDetails.UserID,
                        UserTypeID = RequestDetails.UserTypeID
                         
                    });

            }
            return ListOfRequestDetailsDTO;


        }



        public List<AttachmentDTO> MapListStoredProsedureToRequestAttachment(List<SP_Select_OnlineApprovalsAttachment_Result> RequestList)
        {

            List<AttachmentDTO> ListOfRequestAttachmentDTO = new List<AttachmentDTO>();

            foreach (var Attachment in RequestList)
            {
                ListOfRequestAttachmentDTO.Add(
                    new AttachmentDTO
                    {
                        AttachmentName = Attachment.AttachmentName,
                        AttachmentPath = Attachment.AttachmentPath,
                        RequestID = Attachment.RequestID,
                        AttachmentTypeID = Attachment.AttachmentTypeID,
                        ID = Attachment.ID,
                        IsDeleted = Attachment.IsDeleted,
                        RequestDetailsID = Attachment.RequestDetailsID
                         
                    });

            }
            return ListOfRequestAttachmentDTO;


        }

        public RequestDTO MapRequestDTOToRequest(OnlineApprovals_Requests RequestDTOObj)
        {
            RequestDTO RequestObj = new RequestDTO();

            RequestObj.ID = RequestDTOObj.RequestID;
            RequestObj.ProviderID = RequestDTOObj.ProviderID;
            RequestObj.ProviderType = RequestDTOObj.ProviderTypeID;
            RequestObj.MedicalID = RequestDTOObj.MedicalID;
            RequestObj.MemberName = RequestDTOObj.MemberName;
            RequestObj.MobileNumber = RequestDTOObj.MobileNumber;
            RequestObj.ClientName = RequestDTOObj.ClientName;
            RequestObj.ClaimNumber = RequestDTOObj.ClaimNumber;
            RequestObj.ClaimDate = RequestDTOObj.ClaimDate.Value;
            RequestObj.CoInsurancePerc = RequestDTOObj.CoInsurancePercentage;
            RequestObj.Diagnose = RequestDTOObj.Diagnose;
            RequestObj.Note = RequestDTOObj.Notes;
            RequestObj.CreationTime = RequestDTOObj.RequestCreationTime;
            RequestObj.StatusID = RequestDTOObj.RequestStatusID;
            RequestObj.FinnacialApproval = RequestDTOObj.IsFinnacialApproval;
            RequestObj.MedicalApproval = RequestDTOObj.IsMedicalApproval;
            RequestObj.IVRNumber = RequestDTOObj.IVRNumber;
            RequestObj.Delivered = RequestDTOObj.Delivered;
            RequestObj.Seen = RequestDTOObj.Seen;
            RequestObj.SeenByUserID = RequestDTOObj.SeenByUserID;
            RequestObj.Deleted = RequestDTOObj.IsDeleted;
            RequestObj.RequestTypeID = RequestDTOObj.RequestTypeID;
            RequestObj.CallCenterAssignee = RequestDTOObj.CallCenterAssignee;
            RequestObj.CloseTime = RequestObj.CloseTime;
             
            return RequestObj;
        }


        public List<AttachmentDTO> MapListRequestAttachmentToDTOObject(List<OnlineApprovals_RequestAttachment> RequestList)
        {

            List<AttachmentDTO> ListOfRequestAttachmentDTO = new List<AttachmentDTO>();

            foreach (var Attachment in RequestList)
            {
                ListOfRequestAttachmentDTO.Add(
                    new AttachmentDTO
                    {
                        AttachmentName = Attachment.AttachmentName,
                        AttachmentPath = Attachment.AttachmentPath,
                        AttachmentTypeID = Attachment.AttachmentTypeID,
                        ID = Attachment.ID,
                        IsDeleted = Attachment.IsDeleted,
                        RequestDetailsID = Attachment.RequestDetailsID,
                        RequestID = Attachment.RequestID
                         
                    });

            }
            return ListOfRequestAttachmentDTO;
        }
        public List<AttachmentDTO> MapListRequestAttachmentToAttachement(List<AttachmentDTO> RequestList)
        {

            List<AttachmentDTO> ListOfRequestAttachmentDTO = new List<AttachmentDTO>();

            foreach (var Attachment in RequestList)
            {
                ListOfRequestAttachmentDTO.Add(
                    new AttachmentDTO
                    {
                        AttachmentName = Attachment.AttachmentName,
                        AttachmentPath = Attachment.AttachmentPath,
                        AttachmentTypeID = Attachment.AttachmentTypeID,
                        ID = Attachment.ID,
                        IsDeleted = Attachment.IsDeleted,
                        RequestDetailsID = Attachment.RequestDetailsID,
                        RequestID = Attachment.RequestID
                         
                    });

            }
            return ListOfRequestAttachmentDTO;


        }



        public List<OnlineApprovals_DemandedDrugsDetails> MapDrugListDTOToDrugList(List<DrugsDetailDTO> DrugListDTO)
        {

            List<OnlineApprovals_DemandedDrugsDetails> listOfDrugs = new List<OnlineApprovals_DemandedDrugsDetails>();
            foreach (var item in DrugListDTO)
            {

                listOfDrugs.Add(
                    new OnlineApprovals_DemandedDrugsDetails
                    {
                        DemandedQuantity = item.DemandedQuantity,
                        DrugID = item.DrugID,
                        DrugName = item.DrugName,
                        ID = item.ID,
                        IsDeleted = item.IsDeleted,
                        IsDrugList = item.IsDrugList,
                        RequestID = item.RequestID,
                        TotalPrice = item.TotalPrice,
                        UnitPrice = item.UnitPrice,
                        UnitQuantity = item.UnitQuantity

                         
                    });


            }
            return listOfDrugs;



        }


        public List<DrugsDetailDTO> MapDrugListToDrugListDTO(List<OnlineApprovals_DemandedDrugsDetails> listOfDrugs)
        {

            List<DrugsDetailDTO> DrugListDTO = new List<DrugsDetailDTO>();
            foreach (var item in listOfDrugs)
            {

                DrugListDTO.Add(
                    new DrugsDetailDTO
                    {
                         DemandedQuantity=item.DemandedQuantity,
                         DrugID= item.DrugID
                          




                    });


            }
            return DrugListDTO;



        }

        public List<DrugsDetailDTO> MapDrugListToDrugList(List<DrugsDetailDTO> listOfDrugs)
        {

            List<DrugsDetailDTO> DrugListDTO = new List<DrugsDetailDTO>();
            foreach (var item in listOfDrugs)
            {

                DrugListDTO.Add(
                    new DrugsDetailDTO
                    {
                        DemandedQuantity = item.DemandedQuantity,
                        DrugID = item.DrugID,





                    });


            }
            return DrugListDTO;

        }

        public InovicesDTO MapInvoiceToDrugInovicesDTO(SP_OnlineApprovals_InovicesByInvoiceID_Result invoiceListDTO)
        {



            InovicesDTO Drugs =
                    new InovicesDTO
                    {
                        AssignedUser = invoiceListDTO.AssignedUser,
                        ClaimNumber = invoiceListDTO.ClaimNumber,
                        ClientName = invoiceListDTO.ClientName,
                        CoInsuranceValue = invoiceListDTO.CoInsuranceValue
                       ,
                        Diagnose = invoiceListDTO.Diagnose,
                        GrandTotal = invoiceListDTO.GrandTotal,
                        ID = invoiceListDTO.InvoiceID,
                        InvoiceDate = invoiceListDTO.InvoiceDate,
                        IVRNumber = invoiceListDTO.IVRNumber,
                        MedicalID = invoiceListDTO.MedicalID,
                        MemberName = invoiceListDTO.MemberName,
                        Notes = invoiceListDTO.Notes,
                        ProviderName = invoiceListDTO.ProviderName,
                        ProviderType = invoiceListDTO.ProviderType,
                        RequestCloseTime = invoiceListDTO.RequestCloseTime,
                        RequestIDs = invoiceListDTO.RequestID,
                        Total = invoiceListDTO.Total


                    };



            return Drugs;



        }
        public InovicesDTO MapInvoiceToDrugInovicesDTOByRequestID(SP_OnlineApprovals_InovicesByRequestID_Result invoiceListDTO)
        {



            InovicesDTO Drugs =
                    new InovicesDTO
                    {
                        AssignedUser = invoiceListDTO.AssignedUser,
                        ClaimNumber = invoiceListDTO.ClaimNumber,
                        ClientName = invoiceListDTO.ClientName,
                        CoInsuranceValue = invoiceListDTO.CoInsuranceValue
                       ,
                        Diagnose = invoiceListDTO.Diagnose,
                        GrandTotal = invoiceListDTO.GrandTotal,
                        ID = invoiceListDTO.InvoiceID,
                        InvoiceDate = invoiceListDTO.InvoiceDate,
                        IVRNumber = invoiceListDTO.IVRNumber,
                        MedicalID = invoiceListDTO.MedicalID,
                        MemberName = invoiceListDTO.MemberName,
                        Notes = invoiceListDTO.Notes,
                        ProviderName = invoiceListDTO.ProviderName,
                        ProviderType = invoiceListDTO.ProviderType,
                        RequestCloseTime = invoiceListDTO.RequestCloseTime,
                        RequestIDs = invoiceListDTO.RequestID,
                        Total = invoiceListDTO.Total
                        

                    };



            return Drugs;



        }

        
        public List<InovicesDTO> MapInvoiceToDrugInovicesDTOMedicalID(List<SP_OnlineApprovals_InovicesByMedicalID_Result> lisMedicalID)
        {

            List<InovicesDTO> listOfDrugs = new List<InovicesDTO>();
            foreach (var item in lisMedicalID)
            {

                listOfDrugs.Add(
                    new InovicesDTO
                    {
                        AssignedUser = item.AssignedUser,
                        ClaimNumber = item.ClaimNumber,
                        ClientName = item.ClientName,
                        CoInsuranceValue = item.CoInsuranceValue
                        ,
                        Diagnose = item.Diagnose,
                        GrandTotal = item.GrandTotal,
                        ID = item.InvoiceID,
                        InvoiceDate = item.InvoiceDate,
                        IVRNumber = item.IVRNumber,
                        MedicalID = item.MedicalID,
                        MemberName = item.MemberName,
                        Notes = item.Notes,
                        ProviderName = item.ProviderName,
                        ProviderType = item.ProviderType,
                        RequestCloseTime = item.RequestCloseTime,
                        RequestIDs = item.RequestID,
                        Total = item.Total
                        

                    });


            }
            return listOfDrugs;



        }

        public ProviderDTO MapPharmcyToSPProviderByAccountID(SP_Select_OnlineApprovalByAccountID_Result provider)
        {
            ProviderDTO Provider = new ProviderDTO
            {
                AccountID = provider.AccountID,
                Discount = provider.Discount,
                ID = provider.ID,
                IsDefaultPassowrd = provider.IsDefaultPassowrd,
                IsOnline = provider.IsOnline,
                LocID = provider.LocID,
                Password = provider.Password,
                PharmAddress = provider.PharmAddress,
                PharmAddressCode = provider.PharmAddressCode,
                PharmLat = provider.PharmLat,
                PharmLong = provider.PharmLong,
                PharmNotes = provider.PharmNotes,
                PharmPhone = provider.PharmPhone,
                PharmPhone1 = provider.PharmPhone1,
                PharmPhone2 = provider.PharmPhone2,
                PharmPhone3 = provider.PharmPhone3,
                PharmSublocationName = provider.PharmSublocationName
            };
            return Provider;
        }
        public ProviderDTO MapPharmcyToSPProviderByID(SP_Select_OnlineApprovalsProvidersByID_Result provider)
        {
            ProviderDTO Provider = new ProviderDTO
            {
                AccountID = provider.AccountID,
                Discount = provider.Discount,
                ID = provider.ID,
                IsDefaultPassowrd = provider.IsDefaultPassowrd,
                IsOnline = provider.IsOnline,
                LocID = provider.LocID,
                Password = provider.Password,
                PharmAddress = provider.PharmAddress,
                PharmAddressCode = provider.PharmAddressCode,
                PharmLat = provider.PharmLat,
                PharmLong = provider.PharmLong,
                PharmNotes = provider.PharmNotes,
                PharmPhone = provider.PharmPhone,
                PharmPhone1 = provider.PharmPhone1,
                PharmPhone2 = provider.PharmPhone2,
                PharmPhone3 = provider.PharmPhone3,
                PharmSublocationName = provider.PharmSublocationName,
                PharmName = provider.PharmName
            };
            return Provider;
        }

        public List<DrugsListDIMDTO> MapDrugListToDrugListDTO(List<SP_Select_OnlineApprovalsDrugList_Result> listOfDrugs)
        {

            List<DrugsListDIMDTO> DrugListDTO = new List<DrugsListDIMDTO>();
            foreach (var item in listOfDrugs)
            {

                DrugListDTO.Add(
                    new DrugsListDIMDTO
                    {
                        DrugID = item.DrugID,
                        DrugName = item.DrugName,
                        IsDeleted = item.IsDeleted,
                        Quaninty = item.Quaninty,
                        UnitQuaninty = item.UnitQuaninty
                         


                    });


            }
            return DrugListDTO;



        }
        public List<DrugsDetailDTO> MapDrugDemanadDrugToDrugsDetailDTO(List<SP_Select_OnlineApprovalsDemandDrugs_Result> listOfDrugsDetails)
        {

            List<DrugsDetailDTO> DrugListDTO = new List<DrugsDetailDTO>();
            foreach (var item in listOfDrugsDetails)
            {

                DrugListDTO.Add(
                    new DrugsDetailDTO
                    {
                       DemandedQuantity=item.DemandedQuantity,
                        DrugID=item.DrugID,
                          DrugName= item.DrugName,
                           ID= item.ID,
                            IsDeleted= item.IsDeleted,
                             IsDrugList= item.IsDrugList,
                              RequestID= item.RequestID,
                               TotalPrice= item.TotalPrice,
                                UnitPrice= item.UnitPrice,
                                 UnitQuantity= item.UnitQuantity
                                 


                    });


            }
            return DrugListDTO;



        }

        public List< RequestDTO> MapStoredProsedureToRequestDTo(List< SP_OnlineApprovals_RequestsByprovider_Result >SP_object)
        {
           List< RequestDTO> Request = new List< RequestDTO>();

            foreach (var item in SP_object)
            {
                Request.Add(new RequestDTO
                {
                    ClaimDate = item.ClaimDate.Value,
                    ClientName = item.ClientName,
                    ClaimNumber = item.ClaimNumber,
                    CoInsurancePerc = item.CoInsurancePercentage,
                    Delivered = item.Delivered,
                    Seen = item.Seen,
                    ID = item.RequestID,
                    Diagnose = item.Diagnose,
                    Deleted = item.IsDeleted,
                    FinnacialApproval = item.IsFinnacialApproval,
                    MedicalApproval = item.IsMedicalApproval,
                    IVRNumber = item.IVRNumber,
                    MedicalID = item.MedicalID,
                    MemberName = item.MemberName,
                    MobileNumber = item.MobileNumber,
                    Note = item.Notes,
                    ProviderID = item.ProviderID,
                    ProviderType = item.ProviderTypeID,
                    CreationTime = item.RequestCreationTime,
                    StatusID = item.RequestStatusID,
                    RequestTypeID = item.RequestTypeID,
                    SeenByUserID = item.SeenByUserID,
                     CallCenterAssignee= item.CallCenterAssignee,
                      CloseTime= item.CloseTime
                      
                });
            }
            return Request;

            }
        public List<DrugsDetailDTO> MapDrugsDetailDTOToDrugsDetailDTO(List<OnlineApprovals_DemandedDrugsDetails> listOfDrugsDetails)
        {

            List<DrugsDetailDTO> DrugListDTO = new List<DrugsDetailDTO>();
            foreach (var item in listOfDrugsDetails)
            {

                DrugListDTO.Add(
                    new DrugsDetailDTO
                    {
                        DemandedQuantity = item.DemandedQuantity,
                        DrugID = item.DrugID,
                        DrugName = item.DrugName,
                        ID = item.ID,
                        IsDeleted = item.IsDeleted,
                        IsDrugList = item.IsDrugList,
                        RequestID = item.RequestID,
                        TotalPrice = item.TotalPrice,
                        UnitPrice = item.UnitPrice,
                        UnitQuantity = item.UnitQuantity



                    });


            }
            return DrugListDTO;



        }





    }



}
