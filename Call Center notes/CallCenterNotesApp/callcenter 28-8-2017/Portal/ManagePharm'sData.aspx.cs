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
    public partial class ManagePharm_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        PharmaciesClass mdb = new PharmaciesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string PharmNamestr = "", PharmAddressCode = "", PharmAddress = "", PharmPhone1 = "", PharmPhone2 = "", PharmPhone3 = "", PharmPhone4 = "";
            string PharmNotes = "", PharmLangitude = "", PharmLatitude = "", PharmSubLocationName = "", PharmSubLocation = "", Categ_D_str = "";
            #region fill Sublocation DropdownList
            var SubLocationList = mdb.GetAllSublocations().Select(u => new ListItem
            {
                Text = u.SubLocName,
                Value = u.SubLocID.ToString()
            }).ToList();
            PharmSubLocationID.Items.Add("-- Enter Sublocation Name --");

            foreach (var item in SubLocationList)
            {
                PharmSubLocationID.Items.Add(item);
            }
            #endregion


            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                UpdatePharmData.Visible = false;
                //DeletePharmData.Visible = false;
                //int id = 3;
                // IDLable.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();

                mdb.GetPharmDetailByid(id, ref PharmNamestr, ref PharmAddressCode, ref PharmAddress, ref PharmPhone1, ref PharmPhone2, ref PharmPhone3 ,ref PharmPhone4, ref PharmNotes, ref PharmLangitude, ref PharmLatitude, ref PharmSubLocation, ref Categ_D_str);

                PharmNameID.Value = PharmNamestr;
                PharmAddressCodeID.Value = PharmAddressCode;
                PharmAddressID.Value = PharmAddress;
                PharmPhone1ID.Value = PharmPhone1;
                PharmPhone2ID.Value = PharmPhone2;
                PharmPhone3ID.Value = PharmPhone3;
                PharmPhone4ID.Value = PharmPhone4;
                PharmNotesID.Value = PharmNotes;
                PharmLangitudeID.Value = PharmLangitude;
                PharmLatitudeID.Value = PharmLatitude;
                PharmSubLocationID.SelectedValue = PharmSubLocation;
                if (Categ_D_str == "1") { ISDCat.Checked = true; }
                if (type == "ProviderUser")
                {
                    UpdatePharmData.Visible = true;
                    //DeletePharmData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeletePharmData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeletePharmData(id);
            Response.Redirect("~/Portal/ShowAllPharms.aspx");
        }
        protected void UpdatePharmData_ServerClick(object sender, EventArgs e)
        {
            string DNetworkCheked;
            if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdatePharmData(id, PharmNameID.Value, PharmAddressCodeID.Value, PharmAddressID.Value, PharmPhone1ID.Value,
                        PharmPhone2ID.Value, PharmPhone3ID.Value, PharmPhone4ID.Value, PharmNotesID.Value, PharmLangitudeID.Value,
                        PharmLatitudeID.Value, PharmSubLocationID.SelectedValue.ToString(),PharmSubLocationID.SelectedItem.ToString(),DNetworkCheked);
        }
    }
}