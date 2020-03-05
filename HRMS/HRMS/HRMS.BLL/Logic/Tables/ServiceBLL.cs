using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
   public class ServiceBLL: Repository<ServicesDIM>
    {

        DateTime creationDate;
        public ServiceBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;


        }

        public List<ServicesDTO> GetServices()
        {
            List<ServicesDTO> Services = new List<ServicesDTO>();
            var Service = GetAll().ToList();
            foreach (var item in Service)
            {
                ServicesDTO temp = new ServicesDTO();
                temp.ServiceID = item.ServiceID;
                temp.ServiceName = item.ServiceName;
                Services.Add(temp);
            }

            return Services;
        }
    }
}
