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
    public partial class AddNewDoctor : System.Web.UI.Page
    {
        DoctorClass mdb = new DoctorClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewDoctor")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
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
                SubLocationCode.Value = "select Code";
            }
            else
            {
                try { SubLocationCode.Value = mdb.GetAllSubLocationSelected(DoctorSubLocationID.SelectedItem.ToString()); }
                catch { SubLocationCode.Value = "gbrg brgb"; }

            }

            div_Error.Visible = false;
            div_Success.Visible = false;
        }
        protected void SubmitNewDoctor(object sender, EventArgs e)
        {
            try
            {

                string DNetworkCheked;
                if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
                if (DoctorSubLocationID.SelectedItem.Text != "-- Enter Sublocation Name --")
                {
                    if (DoctorNetworkID.SelectedItem.Text != "-- Enter Network type --")
                    {
                        if (DoctorCategoryID.SelectedItem.Text != "-- Enter Category Name --")
                        {
                            DoctorPhone2ID.Value = SubLocationCode.Value + DoctorPhone2ID.Value;

                            mdb.addDoctorDeatails(DoctorNameID.Value, DoctorAddressCodeID.Value, DoctorAddressID.Value, DoctorPhone1ID.Value,
                                DoctorPhone2ID.Value, DoctorPhone3ID.Value, DoctorPhone4ID.Value, DoctorTitleID.Value, DoctorNotesID.Value, DoctorLangitudeID.Value,
                                DoctorLatitudeID.Value, DoctorSubLocationID.SelectedValue.ToString(), DoctorSubLocationID.SelectedItem.ToString(), DoctorNetworkID.SelectedValue.ToString(),
                                DoctorCategoryID.SelectedValue.ToString(), DoctorCategoryID.SelectedItem.ToString(), DNetworkCheked);

                            div_Success.Visible = true;
                            div_Error.Visible = false;
                            clean();
                        }
                        else
                        {
                            div_Error.InnerHtml = "Error Pleaze Select Category Name";
                            div_Error.Visible = true;
                        }
                    }
                    else
                    {
                        div_Error.InnerHtml = "Error Pleaze Select Network type";
                        div_Error.Visible = true;
                    }

                }
                else
                {
                    div_Error.InnerHtml = "Error Pleaze Select Sublocation Name";
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
            DoctorNameID.Value = "";
            DoctorAddressID.Value = "";
            DoctorAddressCodeID.Value = "";
            DoctorPhone1ID.Value = "";
            DoctorPhone2ID.Value = "";
            DoctorPhone3ID.Value = "";
            DoctorPhone4ID.Value = "";
            DoctorTitleID.Value = "";
            DoctorNotesID.Value = "";
            DoctorLangitudeID.Value = "";
            DoctorLatitudeID.Value = "";
            SubLocationCode.Value = "";
            DoctorSubLocationID.SelectedIndex = 0;
            DoctorNetworkID.SelectedIndex = 0;
            DoctorCategoryID.SelectedIndex = 0;

        }
    }
}