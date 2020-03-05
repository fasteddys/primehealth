using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class DoctorClass
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public List<SubLocatiobTB> GetAllSublocations()
        {
            List<SubLocatiobTB> data = (from p in db.SubLocatiobTBs orderby p.SubLocName ascending select p).ToList();
            return data;
        }
        public DoctorTB GetDoctorData(int id)
        {
            var data = (from p in db.DoctorTBs where p.ID == id select p).FirstOrDefault();
            return data;
        }
        public List<Category> GetAllCategories()
        {
            List<Category> data = (from p in db.Categories orderby p.Categories_name ascending select p).ToList();
            return data;
        }

        public string GetAllSubLocationSelected(string SubLocName)
        {
            var data = (from p in db.SubLocatiobTBs where p.SubLocName==SubLocName select p).FirstOrDefault();
            if(data!=null)
            return data.SubLocationCode;
            else
            {
                return "";
            }
        }
        public int getID()
        {
            var req = (from p in db.DoctorTBs where p.DoctorName == "Choose Doctor" select p).FirstOrDefault();
            if (req != null)
                return req.ID;
            else
                return -1;
        }


        public void UpdateDoctorDeatails(int id, string DoctorNamestr, string DoctorAddressCodestr, string DoctorAddressstr, string DoctorPhone1str, string DoctorPhone2str, string DoctorPhone3str, string DoctorPhone4str, string DoctorTitlestr, string DoctorNotesstr, string DoctorLangitudestr, string DoctorLatitudestr, string DoctorSubLocationID, string DoctorSubLocationName, string DoctorNetworkstr, string DoctorCategoryID, string DoctorCategorystr, string networkD)
        {
            var req = (from p in db.DoctorTBs where p.ID == id select p ).FirstOrDefault();
            if (req != null)
            {
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                req.DoctorName = DoctorNamestr;
                req.DoctorAddressCode = DoctorAddressCodestr;
                req.DoctorAddress = DoctorAddressstr;
                req.DoctorPhone = DoctorPhone1str;
                req.DoctorPhone2 = DoctorPhone2str;
                req.DoctorPhone3 = DoctorPhone3str;
                req.DoctorPhone4 = DoctorPhone4str;
                req.DoctorInfo = DoctorTitlestr;
                req.DoctorNotes = DoctorNotesstr;
                req.DocLong = DoctorLangitudestr;
                req.DocLat = DoctorLatitudestr;
                req.LocID = int.Parse(DoctorSubLocationID);
                req.SubLocationName = DoctorSubLocationName;
                req.Doc_cat = int.Parse(DoctorCategoryID);
                req.Category = DoctorCategorystr;

                if (DoctorNetworkstr == "1")
                {
                    req.QNB = 1; req.Large = 0; req.Meduim = 0; req.Discount = 0;
                }
                else if (DoctorNetworkstr == "2")
                {
                    req.QNB = 1; req.Large = 1; req.Meduim = 0; req.Discount = 0;
                }
                else if (DoctorNetworkstr == "3")
                {
                    req.QNB = 1; req.Large = 1; req.Meduim = 1; req.Discount = 0;
                }

                req.Discount = int.Parse(networkD);
           
                db.SaveChanges();
            }
        }

        public void addDoctorDeatails(string DoctorNamestr, string DoctorAddressCodestr, string DoctorAddressstr, string DoctorPhone1str, string DoctorPhone2str, string DoctorPhone3str, string DoctorPhone4str, string DoctorTitlestr, string DoctorNotesstr, string DoctorLangitudestr, string DoctorLatitudestr, string DoctorSubLocationID, string DoctorSubLocationName, string DoctorNetworkstr, string DoctorCategoryID, string DoctorCategorystr, string networkD)
        {
            DoctorTB req = new DoctorTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.DoctorName = DoctorNamestr;
            req.DoctorAddressCode = DoctorAddressCodestr;
            req.DoctorAddress = DoctorAddressstr;
            req.DoctorPhone = DoctorPhone1str;
            req.DoctorPhone2 =DoctorPhone2str;
            req.DoctorPhone3 = DoctorPhone3str;
            req.DoctorPhone4 = DoctorPhone4str;
            req.DoctorInfo = DoctorTitlestr;
            req.DoctorNotes = DoctorNotesstr;
            req.DocLong = DoctorLangitudestr;
            req.DocLat = DoctorLatitudestr;
            req.LocID = int.Parse(DoctorSubLocationID);
            req.SubLocationName = DoctorSubLocationName;
            req.Doc_cat = int.Parse(DoctorCategoryID);
            req.Category = DoctorCategorystr;

            if (DoctorNetworkstr == "1")
            {
                req.QNB = 1; req.Large = 0; req.Meduim = 0; req.Discount = 0;
            }
            else if (DoctorNetworkstr == "2")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 0; req.Discount = 0;
            }
            else if (DoctorNetworkstr == "3")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 1; req.Discount = 0;
            }
            
            req.Discount = int.Parse(networkD);
            db.DoctorTBs.Add(req);
            db.SaveChanges();
        }
        public List<DoctorTB> GetAllDoctors()
        {
            List<DoctorTB> data = (from p in db.DoctorTBs
                                   //where p.Reqtype == type && p.ReqStatus == status
                                   orderby p.ID descending
                                   select p).ToList();
            return data;
        }
        public void GetDoctorDetailByid(int id, ref string DoctorNamestr, ref string DoctorAddressCode , ref string DoctorAddress, ref string DoctorPhone1, ref string DoctorPhone2, ref string DoctorPhone3,ref string DoctorPhone4, ref string DoctorTitle, ref string DoctorNotes, ref string DoctorLangitude, ref string DoctorLatitude, ref string DoctorSubLocation, ref string DoctorNetwork, ref string DoctorCategory, ref string Categ_d)
        {
            var data = from p in db.DoctorTBs where p.ID == id select p;
            string L , M , Q ;
            //CreationDate = data.First().CreationTime.ToString();
            DoctorNamestr = data.First().DoctorName;
            DoctorAddressCode = data.First().DoctorAddressCode;
            DoctorAddress = data.First().DoctorAddress;
            DoctorPhone1 = data.First().DoctorPhone;
            DoctorPhone2 = data.First().DoctorPhone2;
            DoctorPhone3 = data.First().DoctorPhone3;
            DoctorPhone4 = data.First().DoctorPhone4;
            DoctorTitle = data.First().DoctorInfo;
            DoctorNotes = data.First().DoctorNotes;
            DoctorLangitude = data.First().DocLong;
            DoctorLatitude = data.First().DocLat;
            DoctorSubLocation = data.First().LocID.ToString();

            Q = data.First().QNB.ToString();
            L = data.First().Large.ToString();
            M = data.First().Meduim.ToString();

                if (Q == "1" && L == "0" && M == "0") { DoctorNetwork = "1";}
                if (Q == "1" && L == "1" && M == "0") { DoctorNetwork = "2";}
                if (Q == "1" && L == "1" && M == "1") { DoctorNetwork = "3";}
            
            DoctorCategory = data.First().Doc_cat.ToString();
            Categ_d = data.First().Discount.ToString();

           // var subquery = from p in db.SubLocatiobTBs where p.SubLocID == data.First().LocID select p;


           // DoctorTitle = subquery.First().SubLocName;

        }
        public void UpdateDoctorData(int id, string DoctorNamestr, string DoctorAddressCodestr, string DoctorAddressstr, string DoctorPhone1str, string DoctorPhone2str, string DoctorPhone3str, string DoctorPhone4str, string DoctorTitlestr, string DoctorNotesstr, string DoctorLangitudestr, string DoctorLatitudestr, string DoctorSubLocationID, string DoctorSubLocationNameID, string DoctorNetworkstr, string DoctorCategoryID, string DoctorCategorystr, string networkD)
        {
            DoctorTB req = db.DoctorTBs.Single(p => p.ID == id);
            req.DoctorName = DoctorNamestr;
            req.DoctorAddressCode = DoctorAddressCodestr;
            req.DoctorAddress = DoctorAddressstr;
            req.DoctorPhone = DoctorPhone1str;
            req.DoctorPhone2 = DoctorPhone2str;
            req.DoctorPhone3 = DoctorPhone3str;
            req.DoctorPhone4 = DoctorPhone3str;
            req.DoctorInfo = DoctorTitlestr;
            req.DoctorNotes = DoctorNotesstr;
            req.DocLong = DoctorLangitudestr;
            req.DocLat = DoctorLatitudestr;
            req.LocID = int.Parse(DoctorSubLocationID);
            req.SubLocationName = DoctorSubLocationNameID;
            req.Doc_cat = int.Parse(DoctorCategoryID);
            req.Category = DoctorCategorystr;

            if (DoctorNetworkstr == "1")
            {
                req.QNB = 1; req.Large = 0; req.Meduim = 0; req.Discount = 0;
            }
            else if (DoctorNetworkstr == "2")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 0; req.Discount = 0;
            }
            else if (DoctorNetworkstr == "3")
            {
                req.QNB = 1; req.Large = 1; req.Meduim = 1; req.Discount = 0;
            }
            req.Discount = int.Parse(networkD);
            db.SaveChanges();
        }
        public void DeleteDoctorData(int id)
        {
            DoctorTB acc = db.DoctorTBs.Single(p => p.ID == id);
            db.DoctorTBs.Remove(acc);
            db.SaveChanges();
        }
        public int DoctorCount()
        {
            var data = (from p in db.DoctorTBs
                        select p).Count();
            int d = (int)data;
            return d;
        }


    }
}