using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace CallCenterNotesApp.Portal
{
    public partial class ManageDoctor_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        DoctorClass mdb = new DoctorClass();
        protected void Page_Load(object sender, EventArgs e)
        {
          
            string DoctorNamestr = "", DoctorAddress = "",DoctorAddressCode="", DoctorPhone1 = "", DoctorPhone2 = "", DoctorPhone3 = "", DoctorPhone4 = "", DoctorTitle = "";
            string DoctorNotes = "", DoctorLangitude = "", DoctorLatitude = "", DoctorSubLocation = "", DoctorNetwork = "", DoctorCategory = "", Categ_D_str = "";
            #region fill Sublocation DropdownList
            var SubLocationList = mdb.GetAllSublocations().Select(u => new ListItem
            {
                Text = u.SubLocName,
                Value = u.SubLocID.ToString()
            }).ToList();
            DoctorSubLocationID.Items.Add("-- Enter Sublocation Name --");

            foreach (var item in SubLocationList)
            {
                DoctorSubLocationID.Items.Add(item);
            }
            #endregion
            #region fill Doctor Category DropdownList
            var DoctorCategoryList = mdb.GetAllCategories().Select(u => new ListItem
            {
                Text = u.Categories_name,
                Value = u.Categories_id.ToString()
            }).ToList();
            DoctorCategoryID.Items.Add("-- Enter Category Name --");

            foreach (var item in DoctorCategoryList)
            {
                DoctorCategoryID.Items.Add(item);
            }
            #endregion

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                UpdateDoctorData.Visible = false;
                //DeleteDoctorData.Visible = false;
                //int id = 3;
                // IDLable.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();

                mdb.GetDoctorDetailByid(id, ref DoctorNamestr, ref DoctorAddressCode ,ref DoctorAddress, ref DoctorPhone1, ref DoctorPhone2, ref DoctorPhone3,ref DoctorPhone4, ref DoctorTitle, ref DoctorNotes, ref DoctorLangitude, ref DoctorLatitude, ref DoctorSubLocation, ref DoctorNetwork, ref DoctorCategory, ref Categ_D_str);

                DoctorNameID.Value = DoctorNamestr;
                DoctorAddressCodeID.Value = DoctorAddressCode;
                DoctorAddressID.Value = DoctorAddress;
                DoctorPhone1ID.Value = DoctorPhone1;
                DoctorPhone2ID.Value = DoctorPhone2;
                DoctorPhone3ID.Value = DoctorPhone3;
                DoctorPhone4ID.Value = DoctorPhone4;
                DoctorTitleID.Value = DoctorTitle;
                DoctorNotesID.Value = DoctorNotes;
                DoctorLangitudeID.Value = DoctorLangitude;
                DoctorLatitudeID.Value = DoctorLatitude;
                DoctorSubLocationID.SelectedValue = DoctorSubLocation;
                DoctorNetworkID.SelectedValue = DoctorNetwork;
                if (Categ_D_str == "1") { ISDCat.Checked = true; }
                DoctorCategoryID.SelectedValue = DoctorCategory;
                if (type == "ProviderUser")
                {
                    UpdateDoctorData.Visible = true;
                    //DeleteDoctorData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteDoctorData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteDoctorData(id);
            Response.Redirect("~/Portal/ShowAllDoctors.aspx");
        }
        protected void UpdateDoctorData_ServerClick(object sender, EventArgs e)
        {
            string DNetworkCheked;
            if (ISDCat.Checked) { DNetworkCheked="1"; } else { DNetworkCheked="0";}
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateDoctorData(id , DoctorNameID.Value,DoctorAddressCodeID.Value ,DoctorAddressID.Value, DoctorPhone1ID.Value,
                        DoctorPhone2ID.Value, DoctorPhone3ID.Value, DoctorPhone4ID.Value, DoctorTitleID.Value, DoctorNotesID.Value, DoctorLangitudeID.Value,
                        DoctorLatitudeID.Value, DoctorSubLocationID.SelectedValue.ToString(),DoctorSubLocationID.SelectedItem.ToString(), DoctorNetworkID.SelectedValue.ToString(),
                        DoctorCategoryID.SelectedValue.ToString(), DoctorCategoryID.SelectedItem.ToString(), DNetworkCheked);

            div_Success_update.Visible = true;
        }
    }
}