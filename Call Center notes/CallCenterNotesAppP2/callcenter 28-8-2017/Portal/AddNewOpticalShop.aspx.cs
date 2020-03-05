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
    public partial class AddNewOpticalShop : System.Web.UI.Page
    {
        OpticalClass mdb = new OpticalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewOpticalShop")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
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
                SubLocationCode.Value = "select Code";
            }
            else
            {
                try { SubLocationCode.Value = mdb.GetAllSubLocationSelected(OpticalSubLocationID.SelectedItem.ToString()); }
                catch { SubLocationCode.Value = "gbrg brgb"; }

            }
            div_Error.Visible = false;
            div_Success.Visible = false;

        }
        protected void SubmitNewOptical(object sender, EventArgs e)
        {
            try
            {
                string DNetworkCheked;
                if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
                if (OpticalSubLocationID.Text != "-- Enter Sublocation Name --" || OpticalCategoryID.Text != "-- Enter Category Name --")
                {
                    OpticalPhone2ID.Value = SubLocationCode.Value + OpticalPhone2ID.Value;

                    mdb.addOpticalDeatails(OpticalNameID.Value, OpticalAddressCodeID.Value, OpticalAddressID.Value, OpticalPhone1ID.Value,
                        OpticalPhone2ID.Value, OpticalPhone3ID.Value, OpticalPhone4ID.Value, OpticalNotesID.Value, OpticalLangitudeID.Value,
                        OpticalLatitudeID.Value, OpticalSubLocationID.SelectedValue.ToString(), OpticalSubLocationID.SelectedItem.ToString(),
                        OpticalCategoryID.SelectedItem.ToString(), DNetworkCheked);

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
            OpticalNameID.Value = "";
            OpticalAddressCodeID.Value = "";
            OpticalAddressID.Value = "";
            OpticalPhone1ID.Value = "";
            OpticalPhone2ID.Value = "";
            OpticalPhone3ID.Value = "";
            OpticalPhone4ID.Value = "";
            OpticalNotesID.Value = "";
            OpticalLangitudeID.Value = "";
            OpticalLatitudeID.Value = "";
            SubLocationCode.Value = "";
            OpticalSubLocationID.SelectedIndex = 0;
            OpticalCategoryID.SelectedIndex = 0;

        }
    }
}