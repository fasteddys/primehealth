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
    public partial class AddNewHospital : System.Web.UI.Page
    {
        HospitalClass mdb = new HospitalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewHospital")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
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
                SubLocationCode.Value = "select Code";
            }
            else
            {
                try { SubLocationCode.Value = mdb.GetAllSubLocationSelected(HospitalSubLocationID.SelectedItem.ToString()); }
                catch { SubLocationCode.Value = "star"; }

            }
            div_Error.Visible = false;
            div_Success.Visible = false;

        }
        protected void SubmitNewHospital(object sender, EventArgs e)
        {
            try
            {
                string DNetworkCheked;
                if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
                if (HospitalSubLocationID.Text != "-- Enter Sublocation Name --" || HospitalNetworkID.Text != "-- Enter Network type --" || HospitalCategoryID.Text != "-- Enter Category Name --")
                {
                    HospitalPhone2ID.Value = SubLocationCode.Value + HospitalPhone2ID.Value;


                    mdb.addHospitalDeatails(HospitalNameID.Value, HospitalAddressCodeID.Value, HospitalAddressID.Value, HospitalPhone1ID.Value,
                        HospitalPhone2ID.Value, HospitalPhone3ID.Value, HospitalPhone4ID.Value, HospitalNotesID.Value, HospitalLangitudeID.Value,
                        HospitalLatitudeID.Value, HospitalSubLocationID.SelectedValue.ToString(), HospitalSubLocationID.SelectedItem.ToString(),
                        HospitalNetworkID.SelectedValue.ToString(),
                       HospitalCategoryID.SelectedItem.ToString(), DNetworkCheked);

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
            HospitalNameID.Value = "";
            HospitalAddressCodeID.Value = "";
            HospitalAddressID.Value = "";
            HospitalPhone1ID.Value = "";
            HospitalPhone2ID.Value = "";
            HospitalPhone3ID.Value = "";
            HospitalPhone4ID.Value = "";
            HospitalNotesID.Value = "";
            HospitalLangitudeID.Value = "";
            HospitalLatitudeID.Value = "";
            HospitalSubLocationID.SelectedIndex = 0;
            HospitalNetworkID.SelectedIndex = 0;
            HospitalCategoryID.SelectedIndex = 0;

        }
    }
}