using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class PharmaciesClass
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public List<SubLocatiobTB> GetAllSublocations()
        {
            List<SubLocatiobTB> data = (from p in db.SubLocatiobTBs orderby p.SubLocName ascending select p).ToList();
            return data;
        }
        public void addPharmDeatails(string pharmNamestr, string pharmAddressCodestr, string pharmAddressstr,string pharmPhone1str, string pharmPhone2str, string pharmPhone3str, string pharmPhone4str, string pharmNotesstr, string pharmLangitudestr, string pharmLatitudestr, string pharmSubLocationID, string pharmSubLocationName, string D_Network)
        {
            PharmacyTB req = new PharmacyTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.PharmName = pharmNamestr;
            req.PharmAddressCode = pharmAddressCodestr;
            req.PharmAddress = pharmAddressstr;
            req.PharmPhone = pharmPhone1str;
            req.PharmPhone1 = pharmPhone2str;
            req.PharmPhone2 = pharmPhone3str;
            req.PharmPhone3 = pharmPhone4str;
            req.PharmNotes = pharmNotesstr;
            req.PharmLong = pharmLangitudestr;
            req.PharmLat = pharmLatitudestr;
            req.LocID = int.Parse(pharmSubLocationID);
            req.PharmSublocationName = pharmSubLocationName;
            req.Discount = int.Parse(D_Network);

            db.PharmacyTBs.Add(req);
            db.SaveChanges();
        }

        public string GetAllSubLocationSelected(string SubLocName)
        {
            var data = (from p in db.SubLocatiobTBs where p.SubLocName == SubLocName select p);
            return data.First().SubLocationCode;
        }

        public List<PharmacyTB> GetAllPharms()
        {
            List<PharmacyTB> data = (from p in db.PharmacyTBs
                                       //where p.Reqtype == type && p.ReqStatus == status
                                       orderby p.ID descending
                                       select p).ToList();
            return data;
        }
        public void GetPharmDetailByid(int id, ref string PharmNamestr, ref string PharmAddressCode, ref string PharmAddress, ref string PharmPhone1, ref string PharmPhone2, ref string PharmPhone3, ref string PharmPhone4, ref string PharmNotes, ref string PharmLangitude, ref string PharmLatitude, ref string PharmSubLocation, ref string Categ_d)
        {
            var data = from p in db.PharmacyTBs where p.ID == id select p;

            //CreationDate = data.First().CreationTime.ToString();
            PharmNamestr = data.First().PharmName;
            PharmAddressCode = data.First().PharmAddressCode;
            PharmAddress = data.First().PharmAddress;
            PharmPhone1 = data.First().PharmPhone;
            PharmPhone2 = data.First().PharmPhone1;
            PharmPhone3 = data.First().PharmPhone2;
            PharmPhone4 = data.First().PharmPhone3;
            PharmNotes = data.First().PharmNotes;
            PharmLangitude = data.First().PharmLong;
            PharmLatitude = data.First().PharmLat;
            PharmSubLocation = data.First().LocID.ToString();
            Categ_d = data.First().Discount.ToString();

        }
        public void UpdatePharmData(int id, string PharmNamestr, string PharmAddressCodestr, string PharmAddressstr, string PharmPhone1str, string PharmPhone2str, string PharmPhone3str, string PharmPhone4str, string PharmNotesstr, string PharmLangitudestr, string PharmLatitudestr, string PharmSubLocationID,string PharmSubLocationName, string networkD)
        {
            PharmacyTB req = db.PharmacyTBs.Single(p => p.ID == id);
            req.PharmName = PharmNamestr;
            req.PharmAddressCode = PharmAddressCodestr;
            req.PharmAddress = PharmAddressstr;
            req.PharmPhone = PharmPhone1str;
            req.PharmPhone1 = PharmPhone2str;
            req.PharmPhone2 = PharmPhone3str;
            req.PharmPhone3 = PharmPhone4str;
            req.PharmNotes = PharmNotesstr;
            req.PharmLong = PharmLangitudestr;
            req.PharmLat = PharmLatitudestr;
            req.PharmSublocationName = PharmSubLocationName;
            req.LocID = int.Parse(PharmSubLocationID);
            req.Discount = int.Parse(networkD);
            db.SaveChanges();
        }
        public void DeletePharmData(int id)
        {
            PharmacyTB acc = db.PharmacyTBs.Single(p => p.ID == id);
            db.PharmacyTBs.Remove(acc);
            db.SaveChanges();
        }
        public int PharmsCount()
        {
            var data = (from p in db.PharmacyTBs
                        select p).Count();
            int d = (int)data;
            return d;
        }

    }
}