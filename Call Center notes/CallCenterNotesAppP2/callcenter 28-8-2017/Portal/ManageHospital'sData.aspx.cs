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
    public partial class ManageHospital_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        HospitalClass mdb = new HospitalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string HospitalNamestr = "", HospitalAddressCode="", HospitalAddress = "", HospitalPhone1 = "", HospitalPhone2 = "", HospitalPhone3 = "", HospitalPhone4 = "";
            string HospitalNotes = "", HospitalLangitude = "", HospitalLatitude = "", HospitalSubLocation = "", HospitalNetwork = "", HospitalCategory = "", Categ_D_str = "";
            #region fill Sublocation DropdownList
            var SubLocationList = mdb.GetAllSublocations().Select(u => new ListItem
            {
                Text = u.SubLocName,
                Value = u.SubLocID.ToString()
            }).ToList();
            HospitalSubLocationID.Items.Add("-- Enter Sublocation Name --");

            foreach (var item in SubLocationList)
            {
                HospitalSubLocationID.Items.Add(item);
            }
            #endregion
            

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                UpdateHospitalData.Visible = false;
                //DeleteHospitalData.Visible = false;
                //int id = 3;
                // IDLable.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();

                mdb.GetHospitalDetailByid(id, ref HospitalNamestr, ref HospitalAddressCode, ref HospitalAddress, ref HospitalPhone1, ref HospitalPhone2, ref HospitalPhone3, ref HospitalPhone4, ref HospitalNotes, ref HospitalLangitude, ref HospitalLatitude, ref HospitalSubLocation, ref HospitalNetwork, ref HospitalCategory, ref Categ_D_str);

                HospitalNameID.Value = HospitalNamestr;
                HospitalAddressCodeID.Value = HospitalAddressCode;
                HospitalAddressID.Value = HospitalAddress;
                HospitalPhone1ID.Value = HospitalPhone1;
                HospitalPhone2ID.Value = HospitalPhone2;
                HospitalPhone3ID.Value = HospitalPhone3;
                HospitalPhone4ID.Value = HospitalPhone4;
                HospitalNotesID.Value = HospitalNotes;
                HospitalLangitudeID.Value = HospitalLangitude;
                HospitalLatitudeID.Value = HospitalLatitude;
                HospitalSubLocationID.SelectedValue = HospitalSubLocation;
                HospitalNetworkID.SelectedValue = HospitalNetwork;
                if (Categ_D_str == "1") { ISDCat.Checked = true; }
                HospitalCategoryID.SelectedValue = HospitalCategory;
                if (type == "ProviderUser")
                {
                    UpdateHospitalData.Visible = true;
                    //DeleteHospitalData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteHospitalData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteHospitalData(id);
            Response.Redirect("~/Portal/showAllHospitals.aspx");
        }
        protected void UpdateHospitalData_ServerClick(object sender, EventArgs e)
        {
            string DNetworkCheked;
            if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateHospitalData(id, HospitalNameID.Value, HospitalAddressCodeID.Value, HospitalAddressID.Value, HospitalPhone1ID.Value,
                        HospitalPhone2ID.Value, HospitalPhone3ID.Value, HospitalPhone4ID.Value, HospitalNotesID.Value, HospitalLangitudeID.Value,
                        HospitalLatitudeID.Value, HospitalSubLocationID.SelectedValue.ToString(),HospitalSubLocationID.SelectedItem.ToString() ,HospitalNetworkID.SelectedValue.ToString(),
                        HospitalCategoryID.SelectedItem.ToString(), DNetworkCheked);

            //div_Success_update.Visible = true;
        }
    }
}