using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;
using System.Collections;

namespace CallCenterNotesApp.BLL
{
    public class GetAllSublocationsClass
{
        public int SubLocIDs { get; set; }
        public string SubLocNames { get; set; }
        public string LocationNames { get; set; }
}
    public class LocationsAndCategories
    {
        PhNetworkEntities db = new PhNetworkEntities();
        #region Locations Functions
        public void addLocationDeatails(string LocationNamestr)
        {
            LocationsTB req = new LocationsTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.LocationName = LocationNamestr;
            db.LocationsTBs.Add(req);
            db.SaveChanges();
        }
        public List<LocationsTB> GetAllLocations()
        {
            List<LocationsTB> data = (from p in db.LocationsTBs
                                   //where p.Reqtype == type && p.ReqStatus == status
                                   orderby p.LocationName ascending
                                   select p).ToList();
            return data;
        }
        public void GetLocationDetailByid(int id, ref string LocationNamestr)
        {
            var data = from p in db.LocationsTBs where p.LocID == id select p;

            LocationNamestr = data.First().LocationName;


        }
        public void UpdateLocationData(int id, string LocationNamestr)
        {
            LocationsTB req = db.LocationsTBs.Single(p => p.LocID == id);
            req.LocationName = LocationNamestr;
            db.SaveChanges();
        }
        public void DeleteLocationData(int id)
        {
            LocationsTB acc = db.LocationsTBs.Single(p => p.LocID == id);
            db.LocationsTBs.Remove(acc);
            db.SaveChanges();
        }
        #endregion
        #region Category Functions
        public void addCategoryDeatails(string CategoryNamestr)
        {
            Category req = new Category();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.Categories_name = CategoryNamestr;
            db.Categories.Add(req);
            db.SaveChanges();
        }
        public List<Category> GetAllCategory()
        {
            List<Category> data = (from p in db.Categories
                                      orderby p.Categories_id descending
                                      select p).ToList();
            return data;
        }
        public void GetCategoryDetailByid(int id, ref string CategoryNamestr)
        {
            var data = from p in db.Categories where p.Categories_id == id select p;

            CategoryNamestr = data.First().Categories_name;


        }
        public void UpdateCategoryData(int id, string CategoryNamestr)
        {
            Category req = db.Categories.Single(p => p.Categories_id == id);
            req.Categories_name = CategoryNamestr;
            db.SaveChanges();
        }
        public void DeleteCategoryData(int id)
        {
            Category acc = db.Categories.Single(p => p.Categories_id == id);
            db.Categories.Remove(acc);
            db.SaveChanges();
        }
        #endregion
        #region SubLocations Function
        public void addSubLocationDeatails(string SubLocationNamestr , string LocationID)
        {
            SubLocatiobTB req = new SubLocatiobTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.SubLocName = SubLocationNamestr;
            req.MainLocID = int.Parse(LocationID);
            db.SubLocatiobTBs.Add(req);
            db.SaveChanges();
        }
        public IEnumerable GetAllSubLocations()
        {
            var data = (from p in db.SubLocatiobTBs
                        join p1 in db.LocationsTBs on p.MainLocID equals p1.LocID
                        orderby p.SubLocName descending
                        select (new { p.SubLocID, p.SubLocName, p1.LocationName })).ToList();

            return data;
        }
        public void GetSubLocationDetailByid(int id, ref string SubLocationNamestr, ref string LocationID)
        {
            var data = from p in db.SubLocatiobTBs where p.SubLocID == id select p;

            SubLocationNamestr = data.First().SubLocName;
            LocationID = data.First().MainLocID.ToString();


        }
        public void UpdateSubLocationData(int id, string SubLocationNamestr , string LocationID)
        {
            SubLocatiobTB req = db.SubLocatiobTBs.Single(p => p.SubLocID == id);
            req.SubLocName = SubLocationNamestr;
            req.MainLocID = int.Parse(LocationID);
            db.SaveChanges();
        }
        public void DeleteSubLocationData(int id)
        {
            SubLocatiobTB acc = db.SubLocatiobTBs.Single(p => p.SubLocID == id);
            db.SubLocatiobTBs.Remove(acc);
            db.SaveChanges();
        }

        #endregion
    }
}