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
    public partial class ManageOpticalShop_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        OpticalClass mdb = new OpticalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string OpticalNamestr = "", OpticalAddressCode = "", OpticalAddress = "", OpticalPhone1 = "", OpticalPhone2 = "", OpticalPhone3 = "", OpticalPhone4 = "";
            string OpticalNotes = "", OpticalLangitude = "", OpticalLatitude = "", OpticalSubLocation = "", OpticalCategory = "", Categ_D_str = "";
            #region fill Sublocation DropdownList
            var SubLocationList = mdb.GetAllSublocations().Select(u => new ListItem
            {
                Text = u.SubLocName,
                Value = u.SubLocID.ToString()
            }).ToList();
            OpticalSubLocationID.Items.Add("-- Enter Sublocation Name --");

            foreach (var item in SubLocationList)
            {
                OpticalSubLocationID.Items.Add(item);
            }
            #endregion


            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                UpdateOpticalData.Visible = false;
                //DeleteOpticalData.Visible = false;
                //int id = 3;
                // IDOpticalle.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();

                mdb.GetOpticalDetailByid(id, ref OpticalNamestr, ref OpticalAddressCode, ref OpticalAddress, ref OpticalPhone1, ref OpticalPhone2, ref OpticalPhone3, ref OpticalPhone4, ref OpticalNotes, ref OpticalLangitude, ref OpticalLatitude, ref OpticalSubLocation, ref OpticalCategory, ref Categ_D_str);

                OpticalNameID.Value = OpticalNamestr;
                OpticalAddressCodeID.Value = OpticalAddressCode;
                OpticalAddressID.Value = OpticalAddress;
                OpticalPhone1ID.Value = OpticalPhone1;
                OpticalPhone2ID.Value = OpticalPhone2;
                OpticalPhone3ID.Value = OpticalPhone3;
                OpticalPhone4ID.Value = OpticalPhone4;
                OpticalNotesID.Value = OpticalNotes;
                OpticalLangitudeID.Value = OpticalLangitude;
                OpticalLatitudeID.Value = OpticalLatitude;
                OpticalSubLocationID.SelectedValue = OpticalSubLocation;
                if (Categ_D_str == "1") { ISDCat.Checked = true; }
                OpticalCategoryID.SelectedValue = OpticalCategory;
                if (type == "ProviderUser")
                {
                    UpdateOpticalData.Visible = true;
                    //DeleteOpticalData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteOpticalData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteOpticalData(id);
            Response.Redirect("~/Portal/ShowAllOpticals.aspx");
        }
        protected void UpdateOpticalData_ServerClick(object sender, EventArgs e)
        {
            string DNetworkCheked;
            if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateOpticalData(id, OpticalNameID.Value, OpticalAddressCodeID.Value, OpticalAddressID.Value, OpticalPhone1ID.Value,
                        OpticalPhone2ID.Value, OpticalPhone3ID.Value, OpticalPhone4ID.Value, OpticalNotesID.Value, OpticalLangitudeID.Value,
                        OpticalLatitudeID.Value, OpticalSubLocationID.SelectedValue.ToString(), OpticalSubLocationID.SelectedItem.ToString(),
                        OpticalCategoryID.SelectedItem.ToString(), DNetworkCheked);

            //div_Success_update.Visible = true;
        }
    }
}