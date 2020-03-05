using HRMS.BLL.StaticData;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
  public   class RequestDetailsBLL : Repository<RequestDetail>
    {
        UserBLL userBLL;
        DateTime creationDate;
     public   RequestDetailsBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            userBLL = new UserBLL(Context, CreationDate);
            creationDate = CreationDate;


        }
        public void AddNewRequestDetails(RequestDetailsDTO RequestDetail)
        {
            Add(new Entities.RequestDetail
            { 
             CreationDate= RequestDetail.CreationDate,
             IsActive=true,
             IsDeleted=false,
             RequestFK= RequestDetail.RequestID,
             RequestDetailsTypeID= RequestDetail.RequestDetailsTypeID,
             RequestDetailsComment= RequestDetail.RequestDetailsComment,
             RequestDetailsDate=(DateTime) RequestDetail.CreationDate,
             UserFK= RequestDetail.UserID
            });
        }

        public List<RequestDetailsDTO> GetAllRequestDetails(int RequestID)
        {
            List<RequestDetailsDTO> ListRequestDetailsDTO = new List<RequestDetailsDTO>();
                foreach (var item in Find(x => x.RequestFK == RequestID).ToList())
            {
                int? UserID = item?.UserFK;

                ListRequestDetailsDTO.Add(
               new RequestDetailsDTO
               {
                    Action = item.Action,
                    CreationDate =(DateTime) item.CreationDate,
                    RequestDetailsComment = item.RequestDetailsComment,
                    RequestDetailsTypeID=item.RequestDetailsTypeID,
                    RequestDetailsID=item.RequestDetailsID,
                    RequestDetailsTypeName= ((StaticEnums.RequestStatus)Enum.Parse(typeof(StaticEnums.RequestStatus), item.RequestDetailsID.ToString())).ToString() ,
                    DetailsCreator = userBLL.Find(x => x.UserID == UserID).FirstOrDefault()?.UserName

               });

                

            }

            return ListRequestDetailsDTO;
        }
    }
}
