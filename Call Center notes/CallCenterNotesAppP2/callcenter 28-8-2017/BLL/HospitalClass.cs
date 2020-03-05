using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class HospitalClass
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public List<SubLocatiobTB> GetAllSublocations()
        {
            List<SubLocatiobTB> data = (from p in db.SubLocatiobTBs orderby p.SubLocName ascending select p).ToList();
            return data;
        }

        public HospitalTB GetHospitalData(int id)
        {
            var data = (from p in db.HospitalTBs where p.ID == id select p).FirstOrDefault();
            return data;
        }
        public int getID()
        {
            var req = (from p in db.HospitalTBs where p.HospName == "Choose Hospital" select p).FirstOrDefault();
            if (req != null)
                return req.ID;
            else
                return -1;
        }
        public void UpdateHospitalDeatails(int id,string HospitalNamestr, string HospitalAddressCodestr,
         string HospitalAddressstr, string HospitalPhone1str,
         string HospitalPhone2str, string HospitalPhone3str,
         string HospitalPhone4str, string HospitalNotesstr,
         string HospitalLangitudestr, string HospitalLatitudestr,
         string HospitalSubLocationID, string HospitalSubLocationNameID,
         string HospitalNetworkstr, string HospitalCategorystr, string networkD)
        {
            var req = (from p in db.HospitalTBs where p.ID == id select p).FirstOrDefault();
            if (req != null)
            {
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                req.HospName = HospitalNamestr;
                req.AddressNum = HospitalAddressCodestr;
                req.HospAddress = HospitalAddressstr;
                req.HospPhone = HospitalPhone1str;
                req.HospPhone2 = HospitalPhone2str;
                req.HospPhone3 = HospitalPhone3str;
                req.HospPhone4 = HospitalPhone4str;
                req.HospNotes = HospitalNotesstr;
                req.HospLong = HospitalLangitudestr;
                req.HospLat = HospitalLatitudestr;
                req.SubLocationName = HospitalSubLocationNameID;
                req.LocID = int.Parse(HospitalSubLocationID);
                req.Category = HospitalCategorystr;

                if (HospitalNetworkstr == "1")
                {
                    req.QNB = 1; req.Large = 0; req.Meduim = 0; req.Discount = 0;
                }
                else if (HospitalNetworkstr == "2")
                {
                    req.QNB = 1; req.Large = 1; req.Meduim = 0; req.Discount = 0;
                }
                else if (HospitalNetworkstr == "3")
                {
                    req.QNB = 1; req.Large = 1; req.Meduim = 1; req.Discount = 0;
                }
                req.Discount = int.Parse(networkD);
                db.SaveChanges();
            }
        }

        public void addHospitalDeatails(string HospitalNamestr, string HospitalAddressCodestr,
            string HospitalAddressstr, string HospitalPhone1str, 
            string HospitalPhone2str, string HospitalPhone3str, 
            string HospitalPhone4str, string HospitalNotesstr,
            string HospitalLangitudestr, string HospitalLatitudestr,
            string HospitalSubLocationID, string HospitalSubLocationNameID,
            string HospitalNetworkstr, string HospitalCategorystr, string networkD)
        {
            HospitalTB req = new HospitalTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.HospName = HospitalNamestr;
            req.AddressNum = HospitalAddressCodestr;
            req.HospAddress = HospitalAddressstr;
            req.HospPhone = HospitalPhone1str;
            req.HospPhone2 = HospitalPhone2str;
            req.HospPhone3 = HospitalPhone3str;
            req.HospPhone4 = HospitalPhone4str;
            req.HospNotes = HospitalNotesstr;
            req.HospLong = HospitalLangitudestr;
            req.HospLat = HospitalLatitudestr;
            req.SubLocationName = HospitalSubLocationNameID;
            req.LocID = int.Parse(HospitalSubLocationID);
            req.Category = HospitalCategorystr;
           
            if (HospitalNetworkstr == "1")
            {
                req.QNB = 1; req.Large = 0; req.Meduim = 0; req.Discount = 0;
            }
            else if (HospitalNetworkstr == "2")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 0; req.Discount = 0;
            }
            else if (HospitalNetworkstr == "3")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 1; req.Discount = 0;
            }
            req.Discount = int.Parse(networkD);
            db.HospitalTBs.Add(req);
            db.SaveChanges();
        }

        public string GetAllSubLocationSelected(string SubLocName)
        {
            var data = (from p in db.SubLocatiobTBs where p.SubLocName == SubLocName select p).FirstOrDefault();
            if (data != null)
                return data.SubLocationCode;
            else
            {
                return "";
            }
          
        }
        public List<HospitalTB> GetAllHospital()
        {
            List<HospitalTB> data = (from p in db.HospitalTBs
                                     orderby p.ID descending
                                     select p).ToList();
            return data;
        }
        public void GetHospitalDetailByid(int id, ref string HospitalNamestr, ref string HospitalAddressCode, ref string HospitalAddress, ref string HospitalPhone1, ref string HospitalPhone2, ref string HospitalPhone3, ref string HospitalPhone4, ref string HospitalNotes, ref string HospitalLangitude, ref string HospitalLatitude, ref string HospitalSubLocation, ref string HospitalNetwork, ref string HospitalCategory, ref string Categ_d)
        {
            var data = from p in db.HospitalTBs where p.ID == id select p;
            string L, M, Q;
            HospitalNamestr = data.First().HospName;
            HospitalAddressCode = data.First().AddressNum;
            HospitalAddress = data.First().HospAddress;
            HospitalPhone1 = data.First().HospPhone;
            HospitalPhone2 = data.First().HospPhone2;
            HospitalPhone3 = data.First().HospPhone3;
            HospitalPhone4 = data.First().HospPhone4;
            HospitalNotes = data.First().HospNotes;
            HospitalLangitude = data.First().HospLong;
            HospitalLatitude = data.First().HospLat;
            HospitalSubLocation = data.First().LocID.ToString();

            Q = data.First().QNB.ToString();
            L = data.First().Large.ToString();
            M = data.First().Meduim.ToString();

            if (Q == "1" && L == "0" && M == "0") { HospitalNetwork = "1"; }
            if (Q == "1" && L == "1" && M == "0") { HospitalNetwork = "2"; }
            if (Q == "1" && L == "1" && M == "1") { HospitalNetwork = "3"; }
            string cate_type = data.First().Category.ToString();
            if (cate_type == "مستشفيات") { HospitalCategory = "1"; }
            if (cate_type == "مراكز متخصصة") { HospitalCategory = "2"; }
            if (cate_type == "بصريات") { HospitalCategory = "3"; }
            if (cate_type == "عيادات متخصصة") { HospitalCategory = "4"; }
            if (cate_type == "عيادات خارجيه و مركز جراحات أطفال") { HospitalCategory = "5"; }

            Categ_d = data.First().Discount.ToString();

        }
        public void UpdateHospitalData(int id, string HospitalNamestr, string HospitalAddressCodestr, string HospitalAddressstr, string HospitalPhone1str, string HospitalPhone2str, string HospitalPhone3str, string HospitalPhone4str, string HospitalNotesstr, string HospitalLangitudestr, string HospitalLatitudestr, string HospitalSubLocationID, string HospitalSubLocationCodeID, string HospitalNetworkstr, string HospitalCategorystr, string networkD)
        {
            HospitalTB req = db.HospitalTBs.Single(p => p.ID == id);
            req.HospName = HospitalNamestr;
            req.AddressNum = HospitalAddressCodestr;
            req.HospAddress = HospitalAddressstr;
            req.HospPhone = HospitalPhone1str;
            req.HospPhone2 = HospitalPhone2str;
            req.HospPhone3 = HospitalPhone3str;
            req.HospPhone4 = HospitalPhone4str;
            req.HospNotes = HospitalNotesstr;
            req.HospLong = HospitalLangitudestr;
            req.HospLat = HospitalLatitudestr;
            req.SubLocationName = HospitalSubLocationCodeID;
            req.LocID = int.Parse(HospitalSubLocationID);
            req.Category = HospitalCategorystr;

            if (HospitalNetworkstr == "1")
            {
                req.QNB = 1; req.Large = 0; req.Meduim = 0; req.Discount = 0;
            }
            else if (HospitalNetworkstr == "2")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 0; req.Discount = 0;
            }
            else if (HospitalNetworkstr == "3")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 1; req.Discount = 0;
            }
            req.Discount = int.Parse(networkD);
            db.SaveChanges();
        }
        public void DeleteHospitalData(int id)
        {
            HospitalTB acc = db.HospitalTBs.Single(p => p.ID == id);
            db.HospitalTBs.Remove(acc);
            db.SaveChanges();
        }
        public int HospitalCount()
        {
            var data = (from p in db.HospitalTBs
                        select p).Count();
            int d = (int)data;
            return d;
        }












        //public List<HospitalTB> GetLastFiveHospital()
        //{
        //    List<HospitalTB> data = (from p in db.HospitalTBs
        //                             //where p.Reqtype == type && p.ReqStatus == status
        //                             orderby p.ID descending
        //                             select p).Take(5).ToList();
        //    return data;
        //}

    }
}