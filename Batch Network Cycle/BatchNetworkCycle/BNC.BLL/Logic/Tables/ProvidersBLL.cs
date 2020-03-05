using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class ProvidersBLL : Repository<Provider>
    {
        DateTime creationDate;

        public ProvidersBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        //-------------------------------------------------------------
        //get id and name of provider to full dropDown list 
        public List<ProviderOutputDTO> getProvidersData()
        {
            var allProviders = this.GetAll().ToList();
            List<ProviderOutputDTO> providersList = new List<ProviderOutputDTO>();
            ProviderOutputDTO tempProviderData;
            foreach (var provider in allProviders)
            {
                tempProviderData = new ProviderOutputDTO();
                tempProviderData.ProviderID = provider.ProviderID;
                tempProviderData.ProviderName = provider.ProviderEnglishName;
                providersList.Add(tempProviderData);
            }
            return providersList;
        }
        //-------------------------------------------------------------
        //get id and name of provider to full dropDown list 
        public string getProvidersName(int id)
        {
            var tempProviderData = this.Find(p => p.ProviderID == id).FirstOrDefault();
           if(tempProviderData!=null)
            {
                tempProviderData.ProviderEnglishName.ToString();
            }
            return "Not Founded";
        }
        //-------------------------------------------------------------
        public string getProviderName(int? ProviderFk)
        {
            if (ProviderFk != null)
            {
                return Find(u => u.ProviderID == ProviderFk).FirstOrDefault().ProviderEnglishName;

            }
            return "Not Belong To Any Provider";
        }

      
    
    }
}
