using HRMS.BLL.StaticData;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class OfficialHolidayBLL : Repository<OfficialHoliday>
    {
        DateTime creationDate;
        UserBLL userBLL;
        public OfficialHolidayBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            userBLL = new UserBLL(Context, CreationDate);


        }
        /*-------------------------------------------------------------------------*/
        /*get all official holidays*/
        public List<OfficialHolidaysOutputDTO>GetAllOfficialHolidays()
        {
            List<OfficialHolidaysOutputDTO> OfficialHolidaysOutputDTOList = new List<OfficialHolidaysOutputDTO>();
            var allOfficialHoldays = this.Find(oh => oh.IsActive == true).OrderBy(h=>h.HolidayDate).ToList(); 
            OfficialHolidaysOutputDTO tempOfficialHolidaysOutputDTO;
            foreach (var item in allOfficialHoldays)
            {
                tempOfficialHolidaysOutputDTO = new OfficialHolidaysOutputDTO();
                tempOfficialHolidaysOutputDTO.officialHolidaysId = item.OfficialHolidayID;
                tempOfficialHolidaysOutputDTO.officialHolidaysName = item.OfficialHolidayName;
                tempOfficialHolidaysOutputDTO.officialHolidaysDate = item.HolidayDate?.ToString("dd/MM/yyyy");
                tempOfficialHolidaysOutputDTO.AddedBy = item.AddedByUserName;
                if (item.IsOfficial==true)
                {
                    tempOfficialHolidaysOutputDTO.IsOfficial = "Yes";

                }
                else
                {
                    tempOfficialHolidaysOutputDTO.IsOfficial = "No";

                }
                OfficialHolidaysOutputDTOList.Add(tempOfficialHolidaysOutputDTO);

            }
            return OfficialHolidaysOutputDTOList;
        }
        public void DeleteOfficialHolidaysById(int id)
        {
            OfficialHoliday CurrentofficialHolidays = Find(oh => oh.OfficialHolidayID == id).FirstOrDefault();
            CurrentofficialHolidays.IsDeleted = true;
            CurrentofficialHolidays.IsActive = false;
            CurrentofficialHolidays.DeletionDate = DateTime.Now;
            Edit(CurrentofficialHolidays);
        }
        public void AddNewOfficialHolidays(OfficialHolidaysInputDTO NewOfficialHolidays)
        {
            OfficialHoliday OfficialHoliday = new OfficialHoliday();
            //Data Enter from View
            OfficialHoliday.OfficialHolidayName = NewOfficialHolidays.OfficialHolidaysName;
            //OfficialHoliday.HolidayDate =DateTime.Parse(NewOfficialHolidays.OfficialHolidaysStart);
            OfficialHoliday.HolidayDate = DateTime.ParseExact(NewOfficialHolidays.OfficialHolidaysStart, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (NewOfficialHolidays.OfficialHolidaysType==1)
            {
                OfficialHoliday.IsOfficial = true;

            }
            else
            {
                OfficialHoliday.IsOfficial = false;

            }
            //____________________________________________________
            OfficialHoliday.IsActive = true;
            OfficialHoliday.IsDeleted = true;
            OfficialHoliday.CreationDate = DateTime.Now;
            OfficialHoliday.ModificationDate = null;
            OfficialHoliday.DeletionDate = null;
            OfficialHoliday.AddedByUserID = NewOfficialHolidays.UserId;
            OfficialHoliday.AddedByUserName = userBLL.Find(u => u.UserID == OfficialHoliday.AddedByUserID).FirstOrDefault().UserName;
            this.Add(OfficialHoliday);

        }
        public int CheckIfThisDayHaveOfficialHoldays(OfficialHolidaysInputDTO NewOfficialHolidays)
        {
            DateTime NewOfficialHolidayDate = DateTime.ParseExact(NewOfficialHolidays.OfficialHolidaysStart, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if(NewOfficialHolidayDate<DateTime.Now)
            {
                return 0;//Date Time is runing
            }
            var officialHoldays = Find(of => of.HolidayDate == NewOfficialHolidayDate).FirstOrDefault();
            if(officialHoldays==null)
            {
                return 1;//not have 
            }
            return 2;//have 
        }

        public OfficialHolidaysOutputDTO GetOfficialHolidayById(int id)
        {
            var getOfficialHolidaysById = Find(h => h.OfficialHolidayID == id).FirstOrDefault();
            OfficialHolidaysOutputDTO tempOfficialHolidaysOutputDTO = new OfficialHolidaysOutputDTO();
            tempOfficialHolidaysOutputDTO.officialHolidaysName = getOfficialHolidaysById.OfficialHolidayName;
            tempOfficialHolidaysOutputDTO.officialHolidaysDate = getOfficialHolidaysById.HolidayDate?.ToString("dd/MM/yyyy");
            tempOfficialHolidaysOutputDTO.AddedBy = getOfficialHolidaysById.AddedByUserName;
            tempOfficialHolidaysOutputDTO.officialHolidaysId = getOfficialHolidaysById.OfficialHolidayID;
            if (getOfficialHolidaysById.IsOfficial==true)
            {
                tempOfficialHolidaysOutputDTO.IsOfficial = "1";

            }
            else
            {
                tempOfficialHolidaysOutputDTO.IsOfficial ="0";

            }

            return tempOfficialHolidaysOutputDTO;
        }
        public void EditOfficialHolidays(OfficialHolidaysInputDTO NewOfficialHolidays)
        {
            string OfficialHolidaysBefor = XMLObjectConverter.Serialize(Find(oh => oh.OfficialHolidayID == NewOfficialHolidays.OfficialHolidaysId).FirstOrDefault());
            OfficialHoliday CurrentofficialHolidays  = Find(oh => oh.OfficialHolidayID == NewOfficialHolidays.OfficialHolidaysId).FirstOrDefault();
            CurrentofficialHolidays.HolidayDate = DateTime.Parse(NewOfficialHolidays.OfficialHolidaysStart);
            CurrentofficialHolidays.OfficialHolidayName = NewOfficialHolidays.OfficialHolidaysName;
            CurrentofficialHolidays.ModificationDate = DateTime.Now;
            if (NewOfficialHolidays.OfficialHolidaysType == 1)
            {
                CurrentofficialHolidays.IsOfficial = true;

            }
            else
            {
                CurrentofficialHolidays.IsOfficial = false;

            }
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditOfficialHolidays, NewOfficialHolidays.UserId, OfficialHolidaysBefor, XMLObjectConverter.Serialize(CurrentofficialHolidays), "Edit Official Holiday");
            Edit(CurrentofficialHolidays);
        }
        /*-------------------------------------------------------------------------*/

    }


}




