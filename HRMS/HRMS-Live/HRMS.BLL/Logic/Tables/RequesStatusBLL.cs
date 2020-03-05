using HRMS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;
using HRMS.DTOs.Business;
using HRMS.BLL.StaticData;

namespace HRMS.BLL.Logic.Tables
{
  public  class RequestStatusBLL : Repository<RequestStatu>
    {
        UserBLL userBLL;
        DateTime creationDate;
        public RequestStatusBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            userBLL = new UserBLL(Context, CreationDate);

        }
        public List<RequestStatusDTO> GetAllRequesStatus()
        {
            List<RequestStatusDTO> ListRequestStatus = new List<RequestStatusDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true))
                ListRequestStatus.Add(new RequestStatusDTO
                {
                     RequestStatusID = item.RequestStatusID,
                     RequestStatusName = item.RequestStatusName

                });
            return ListRequestStatus;
        }

        public List<RequestStatusDTO> GetAllRequesStatus(int LoggedUserID)
        {
            List<RequestStatusDTO> ListRequestStatus = new List<RequestStatusDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true))
            {
                var HRUser = userBLL.Find(x => x.UserID == LoggedUserID && x.IsHR == true && x.IsActive == true).FirstOrDefault();
                if (HRUser != null && (int)StaticEnums.RequestStatus.Approved == item.RequestStatusID)
                {
                    ListRequestStatus.Add(new RequestStatusDTO
                    {
                        RequestStatusID = item.RequestStatusID,
                        RequestStatusName = item.RequestStatusName

                    });
                    break;
                }
                else if(HRUser == null)
                {
                    ListRequestStatus.Add(new RequestStatusDTO
                    {
                        RequestStatusID = item.RequestStatusID,
                        RequestStatusName = item.RequestStatusName

                    });
                }
            }                
            return ListRequestStatus;
        }
    }
}
