using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.BLL.Logic.Tables
{
  public  class InternalApplicationTextBLL : Repository<InternalApplicationText>
    {
        DateTime creationDate;
         
       public InternalApplicationTextBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<InternalApplicationTextDTO> GetAllInternalApplicationTextByLangugeID(int LangugeID)
        {
            List<InternalApplicationTextDTO> ListTexts = new List<InternalApplicationTextDTO>();
           foreach(var item in Find(x => x.LangugeFK == LangugeID).ToList())
            {
                ListTexts.Add(
                    new InternalApplicationTextDTO
                    {
                        InternalApplicationTextID = item.InternalApplicationTextID,
                        LangugeFk = item.LangugeFK,
                         SelectorName = item.SelectorName,
                        Text = item.Text,
                         SelectorIsTageID= item.SelectorIsTageID
                    });


            }
            return ListTexts;

        }




    }
}
