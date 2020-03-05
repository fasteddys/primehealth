using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class OpticalClass
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public List<OpticalTB> GetAllOpticalShops()
        {
            List<OpticalTB> data = (from p in db.OpticalTBs
                                       //where p.Reqtype == type && p.ReqStatus == status
                                       orderby p.ID descending
                                       select p).ToList();
            return data;
        }
        public List<SubLocatiobTB> GetAllSublocations()
        {
            List<SubLocatiobTB> data = (from p in db.SubLocatiobTBs orderby p.SubLocName ascending select p).ToList();
            return data;
        }
        public string GetAllSubLocationSelected(string SubLocName)
        {
            var data = (from p in db.SubLocatiobTBs where p.SubLocName == SubLocName select p);
            return data.First().SubLocationCode;
        }
        public void addOpticalDeatails(string OpticalNamestr, string OpticalAddressCodestr, string OpticalAddressstr, string OpticalPhone1str, string OpticalPhone2str, string OpticalPhone3str, string OpticalPhone4str, string OpticalNotesstr, string OpticalLangitudestr, string OpticalLatitudestr, string OpticalSubLocationID, string OpticalSubLocationName, string OpticalCategorystr, string D_Network)
        {
            OpticalTB req = new OpticalTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.OpticalName = OpticalNamestr;
            req.OpticalAddressCode = OpticalAddressCodestr;
            req.OpticalAddress = OpticalAddressstr;
            req.OpticalPhone = OpticalPhone1str;
            req.Opticalphone2 = OpticalPhone2str;
            req.Opticalphone3 = OpticalPhone3str;
            req.Opticalphone4 = OpticalPhone4str;
            req.OpticalNotes = OpticalNotesstr;
            req.OpticalLong = OpticalLangitudestr;
            req.OpticalLat = OpticalLatitudestr;
            req.OpticalSublocationName = OpticalSubLocationName;
            req.LocID = int.Parse(OpticalSubLocationID);
            req.Category = OpticalCategorystr;
            req.Discount = int.Parse(D_Network);
            db.OpticalTBs.Add(req);
            db.SaveChanges();
        }
        public void GetOpticalDetailByid(int id, ref string OpticalNamestr, ref string OpticalAddressCode, ref string OpticalAddress, ref string OpticalPhone1, ref string OpticalPhone2, ref string OpticalPhone3, ref string OpticalPhone4, ref string OpticalNotes, ref string OpticalLangitude, ref string OpticalLatitude, ref string OpticalSubLocation, ref string OpticalCategory, ref string Categ_d)
        {
            var data = from p in db.OpticalTBs where p.ID == id select p;

            //CreationDate = data.First().CreationTime.ToString();
            OpticalNamestr = data.First().OpticalName;
            OpticalAddress = data.First().OpticalAddress;
            OpticalAddressCode = data.First().OpticalAddressCode;
            OpticalPhone1 = data.First().OpticalPhone;
            OpticalPhone2 = data.First().Opticalphone2;
            OpticalPhone3 = data.First().Opticalphone3;
            OpticalPhone4 = data.First().Opticalphone4;
            OpticalNotes = data.First().OpticalNotes;
            OpticalLangitude = data.First().OpticalLong;
            OpticalLatitude = data.First().OpticalLat;
            OpticalSubLocation = data.First().LocID.ToString();


            string cate_type = data.First().Category.ToString();
            if (cate_type == "مراكز نظارات") { OpticalCategory = "1"; }
            if (cate_type == "معامل نظارات") { OpticalCategory = "2"; }

            Categ_d = data.First().Discount.ToString();

        }
        public void UpdateOpticalData(int id, string OpticalNamestr, string OpticalAddressCodestr, string OpticalAddressstr, string OpticalPhone1str, string OpticalPhone2str, string OpticalPhone3str, string OpticalPhone4str, string OpticalNotesstr, string OpticalLangitudestr, string OpticalLatitudestr, string OpticalSubLocationID, string OpticalSubLocationName, string OpticalCategorystr, string networkD)
        {
            OpticalTB req = db.OpticalTBs.Single(p => p.ID == id);
            req.OpticalName = OpticalNamestr;
            req.OpticalAddressCode = OpticalAddressCodestr;
            req.OpticalAddress = OpticalAddressstr;
            req.OpticalPhone = OpticalPhone1str;
            req.Opticalphone2 = OpticalPhone2str;
            req.Opticalphone3 = OpticalPhone3str;
            req.Opticalphone4 = OpticalPhone4str;
            req.OpticalNotes = OpticalNotesstr;
            req.OpticalLong = OpticalLangitudestr;
            req.OpticalSublocationName = OpticalSubLocationName;
            req.OpticalLat = OpticalLatitudestr;
            req.LocID = int.Parse(OpticalSubLocationID);
            req.Category = OpticalCategorystr;

            req.Discount = int.Parse(networkD);
            db.SaveChanges();
        }
        public void DeleteOpticalData(int id)
        {
            OpticalTB acc = db.OpticalTBs.Single(p => p.ID == id);
            db.OpticalTBs.Remove(acc);
            db.SaveChanges();
        }
        public int OpticalsCount()
        {
            var data = (from p in db.OpticalTBs
                        select p).Count();
            int d = (int)data;
            return d;
        }


    }
}