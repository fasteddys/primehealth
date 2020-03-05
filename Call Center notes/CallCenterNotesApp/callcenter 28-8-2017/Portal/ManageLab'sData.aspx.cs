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
    public partial class ManageLab_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        LaboratoriesClass mdb = new LaboratoriesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string LabNamestr = "", LabAddressCode = "", LabAddress = "", LabPhone1 = "", LabPhone2 = "", LabPhone3 = "", LabPhone4 = "";
            string LabNotes = "", LabLangitude = "", LabLatitude = "", LabSubLocation = "", LabCategory = "", Categ_D_str = "";
            #region fill Sublocation DropdownList
            var SubLocationList = mdb.GetAllSublocations().Select(u => new ListItem
            {
                Text = u.SubLocName,
                Value = u.SubLocID.ToString()
            }).ToList();
            LabSubLocationID.Items.Add("-- Enter Sublocation Name --");

            foreach (var item in SubLocationList)
            {
                LabSubLocationID.Items.Add(item);
            }
            #endregion


            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                UpdateLabData.Visible = false;
                //DeleteLabData.Visible = false;
                //int id = 3;
                // IDLable.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();

                mdb.GetLabDetailByid(id, ref LabNamestr, ref LabAddressCode, ref LabAddress, ref LabPhone1, ref LabPhone2, ref LabPhone3, ref LabPhone4, ref LabNotes, ref LabLangitude, ref LabLatitude, ref LabSubLocation, ref LabCategory, ref Categ_D_str);

                LabNameID.Value = LabNamestr;
                LabAddressCodeID.Value = LabAddressCode;
                LabAddressID.Value = LabAddress;
                LabPhone1ID.Value = LabPhone1;
                LabPhone2ID.Value = LabPhone2;
                LabPhone3ID.Value = LabPhone3;
                LabPhone4ID.Value = LabPhone4;
                LabNotesID.Value = LabNotes;
                LabLangitudeID.Value = LabLangitude;
                LabLatitudeID.Value = LabLatitude;
                LabSubLocationID.SelectedValue = LabSubLocation;
                if (Categ_D_str == "1") { ISDCat.Checked = true; }
                LabCategoryID.SelectedValue = LabCategory;
                if (type == "ProviderUser")
                {
                    UpdateLabData.Visible = true;
                    //DeleteLabData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteLabData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteLabData(id);
            Response.Redirect("~/Portal/ShowAllLabs.aspx");
        }
        protected void UpdateLabData_ServerClick(object sender, EventArgs e)
        {
            string DNetworkCheked;
            if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateLabData(id, LabNameID.Value, LabAddressCodeID.Value, LabAddressID.Value, LabPhone1ID.Value,
                        LabPhone2ID.Value, LabPhone3ID.Value, LabPhone4ID.Value,LabNotesID.Value, LabLangitudeID.Value,
                        LabLatitudeID.Value, LabSubLocationID.SelectedValue.ToString(),LabSubLocationID.SelectedItem.ToString(),
                        LabCategoryID.SelectedItem.ToString(), DNetworkCheked);

            //div_Success_update.Visible = true;
        }
    }
}