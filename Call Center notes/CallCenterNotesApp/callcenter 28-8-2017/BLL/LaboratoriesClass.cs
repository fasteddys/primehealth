using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;
namespace CallCenterNotesApp.BLL
{
    public class LaboratoriesClass
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public List<SubLocatiobTB> GetAllSublocations()
        {
            List<SubLocatiobTB> data = (from p in db.SubLocatiobTBs orderby p.SubLocName ascending select p).ToList();
            return data;
        }
        public void addLabDeatails(string LabNamestr, string LabAddressCodestr, string LabAddressstr, string LabPhone1str, string LabPhone2str, string LabPhone3str, string LabPhone4str, string LabNotesstr, string LabLangitudestr, string LabLatitudestr, string LabSubLocationID, string LabSubLocationName, string LabCategorystr, string D_Network)
        {
            LaboratoryTB req = new LaboratoryTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.LabName = LabNamestr;
            req.LabAddressCode = LabAddressCodestr;
            req.LabAddress = LabAddressstr;
            req.LabPhone = LabPhone1str;
            req.Labphone2 = LabPhone2str;
            req.Labphone3 = LabPhone3str;
            req.Labphone4 = LabPhone4str;
            req.LabNotes = LabNotesstr;
            req.LabLong = LabLangitudestr;
            req.LabLat = LabLatitudestr;
            req.LabSublocationName = LabSubLocationName;
            req.LocID = int.Parse(LabSubLocationID);
            req.Category = LabCategorystr;
            req.Discount = int.Parse(D_Network);
            db.LaboratoryTBs.Add(req);
            db.SaveChanges();
        }

        public string GetAllSubLocationSelected(string SubLocName)
        {
            var data = (from p in db.SubLocatiobTBs where p.SubLocName == SubLocName select p);
            return data.First().SubLocationCode;
        }

        public List<LaboratoryTB> GetAllLabs()
        {
            List<LaboratoryTB> data = (from p in db.LaboratoryTBs
                                     //where p.Reqtype == type && p.ReqStatus == status
                                     orderby p.ID descending
                                     select p).ToList();
            return data;
        }
        public void GetLabDetailByid(int id, ref string LabNamestr, ref string LabAddressCode, ref string LabAddress, ref string LabPhone1, ref string LabPhone2, ref string LabPhone3, ref string LabPhone4, ref string LabNotes, ref string LabLangitude, ref string LabLatitude, ref string LabSubLocation, ref string LabCategory, ref string Categ_d)
        {
            var data = from p in db.LaboratoryTBs where p.ID == id select p;

            //CreationDate = data.First().CreationTime.ToString();
            LabNamestr = data.First().LabName;
            LabAddress = data.First().LabAddress;
            LabAddressCode = data.First().LabAddressCode;
            LabPhone1 = data.First().LabPhone;
            LabPhone2 = data.First().Labphone2;
            LabPhone3 = data.First().Labphone3;
            LabPhone4 = data.First().Labphone4;
            LabNotes = data.First().LabNotes;
            LabLangitude = data.First().LabLong;
            LabLatitude = data.First().LabLat;
            LabSubLocation = data.First().LocID.ToString();


            string cate_type = data.First().Category.ToString();
            if (cate_type == "مراكز أشعه") { LabCategory = "1"; }
            if (cate_type == "معامل تحاليل") { LabCategory = "2"; }

            Categ_d = data.First().Discount.ToString();

        }
        public void UpdateLabData(int id, string LabNamestr, string LabAddressCodestr, string LabAddressstr, string LabPhone1str, string LabPhone2str, string LabPhone3str, string LabPhone4str, string LabNotesstr, string LabLangitudestr, string LabLatitudestr, string LabSubLocationID, string LabSubLocationName,string LabCategorystr, string networkD)
        {
            LaboratoryTB req = db.LaboratoryTBs.Single(p => p.ID == id);
            req.LabName = LabNamestr;
            req.LabAddressCode = LabAddressCodestr;
            req.LabAddress = LabAddressstr;
            req.LabPhone = LabPhone1str;
            req.Labphone2 = LabPhone2str;
            req.Labphone3 = LabPhone3str;
            req.Labphone4 = LabPhone4str;
            req.LabNotes = LabNotesstr;
            req.LabLong = LabLangitudestr;
            req.LabSublocationName = LabSubLocationName;
            req.LabLat = LabLatitudestr;
            req.LocID = int.Parse(LabSubLocationID);
            req.Category = LabCategorystr;

            req.Discount = int.Parse(networkD);
            db.SaveChanges();
        }
        public void DeleteLabData(int id)
        {
            LaboratoryTB acc = db.LaboratoryTBs.Single(p => p.ID == id);
            db.LaboratoryTBs.Remove(acc);
            db.SaveChanges();
        }
        public int LabsCount()
        {
            var data = (from p in db.LaboratoryTBs
                        select p).Count();
            int d = (int)data;
            return d;
        }

    }
}