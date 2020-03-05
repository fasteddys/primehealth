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
    public partial class AddNewLab : System.Web.UI.Page
    {
        LaboratoriesClass mdb = new LaboratoriesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewLab")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
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
                SubLocationCode.Value = "select Code";
            }
            else
            {
                try { SubLocationCode.Value = mdb.GetAllSubLocationSelected(LabSubLocationID.SelectedItem.ToString()); }
                catch { SubLocationCode.Value = "gbrg brgb"; }

            }
            div_Error.Visible = false;
            div_Success.Visible = false;

        }
        protected void SubmitNewLab(object sender, EventArgs e)
        {
            try
            {
                string DNetworkCheked;
                if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
                if (LabSubLocationID.Text != "-- Enter Sublocation Name --" || LabCategoryID.Text != "-- Enter Category Name --")
                {
                    LabPhone2ID.Value = SubLocationCode.Value + LabPhone2ID.Value;

                    mdb.addLabDeatails(LabNameID.Value, LabAddressCodeID.Value, LabAddressID.Value, LabPhone1ID.Value,
                        LabPhone2ID.Value, LabPhone3ID.Value, LabPhone4ID.Value, LabNotesID.Value, LabLangitudeID.Value,
                        LabLatitudeID.Value, LabSubLocationID.SelectedValue.ToString(), LabSubLocationID.SelectedItem.ToString(),
                        LabCategoryID.SelectedItem.ToString(), DNetworkCheked);

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
            LabNameID.Value = "";
            LabAddressCodeID.Value = "";
            LabAddressID.Value = "";
            LabPhone1ID.Value = "";
            LabPhone2ID.Value = "";
            LabPhone3ID.Value = "";
            LabPhone4ID.Value = "";
            LabNotesID.Value = "";
            LabLangitudeID.Value = "";
            LabLatitudeID.Value = "";
            SubLocationCode.Value = "";
            LabSubLocationID.SelectedIndex = 0;
            LabCategoryID.SelectedIndex = 0;

        }
    }
}