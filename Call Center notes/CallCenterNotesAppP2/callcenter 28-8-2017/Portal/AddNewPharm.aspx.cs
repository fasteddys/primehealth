using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class AddNewPharm : System.Web.UI.Page
    {
        PharmaciesClass mdb = new PharmaciesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewPharm")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
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
                SubLocationCode.Value = "select Code";
            }
            else
            {
                try { SubLocationCode.Value = mdb.GetAllSubLocationSelected(PharmSubLocationID.SelectedItem.ToString()); }
                catch { SubLocationCode.Value = "gbrg brgb"; }

            }
            div_Error.Visible = false;
            div_Success.Visible = false;

        }
        protected void SubmitNewPharm(object sender, EventArgs e)
        {
            try
            {
                string DNetworkCheked;
                if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
                if (PharmSubLocationID.Text != "-- Enter Sublocation Name --")
                {
                    PharmPhone2ID.Value = SubLocationCode.Value + PharmPhone2ID.Value;

                    mdb.addPharmDeatails(
                        PharmNameID.Value, PharmAddressCodeID.Value, PharmAddressID.Value, PharmPhone1ID.Value,
                        PharmPhone2ID.Value, PharmPhone3ID.Value, PharmPhone4ID.Value, PharmNotesID.Value, PharmLangitudeID.Value,
                        PharmLatitudeID.Value, PharmSubLocationID.SelectedValue.ToString(), PharmSubLocationID.SelectedItem.ToString(), DNetworkCheked);

                    div_Success.Visible = true;
                    div_Error.Visible = false;
                    clean();
                }
                else
                {
                    div_Error.Visible = true;
                    div_Success.Visible = false;

                }
            }
            catch (Exception)
            {
                div_Error.Visible = true;
                div_Success.Visible = false;

            }
        }
        public void clean()
        {
            PharmNameID.Value = "";
            PharmAddressID.Value = "";
            PharmAddressCodeID.Value = "";
            PharmPhone1ID.Value = "";
            SubLocationCode.Value = "";
            PharmPhone2ID.Value = "";
            PharmPhone3ID.Value = "";
            PharmPhone4ID.Value = "";
            PharmNotesID.Value = "";
            PharmLangitudeID.Value = "";
            PharmLatitudeID.Value = "";
            PharmSubLocationID.SelectedIndex = 0;

        }
    }
}