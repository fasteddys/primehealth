using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;

namespace BNC.BLL.Logic.Tables
{
    public class CompanyBLL : Repository<Company>
    {
        DateTime creationDate;

        public CompanyBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        
        public List<CompanyDTO> GetAllCompanies()
        {
            List<CompanyDTO> companies = new List<CompanyDTO>(); 
            foreach (var items in GetAll())
            {
                companies.Add(new CompanyDTO
                {
                    CompanyID = items.CompanyID,
                    CompanyName = items.CompanyName
                });
            }
            return companies;
        }
    }
}
