using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;


namespace CallCenterNotesApp.Portal
{
    public partial class EditDoctor : System.Web.UI.Page
    {
        DoctorClass mdb = new DoctorClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("EditDoctor")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                int x = mdb.getID();
                if (x != -1)
                { DropDownList1.SelectedValue = x.ToString(); }


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
                // try
                { SubLocationCode.Value = mdb.GetAllSubLocationSelected(DoctorSubLocationID.SelectedItem.ToString()); }
                //   catch { SubLocationCode.Value = "gbrg brgb"; }

            }

            div_Error.Visible = false;
            div_Success.Visible = false;



        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList1.SelectedItem.Text != "Choose Doctor")
            {
                if (String.IsNullOrWhiteSpace(DropDownList1.SelectedValue) == false)
                {
                    var data = mdb.GetDoctorData(int.Parse(DropDownList1.SelectedValue));
                    DoctorNameID.Value = data.DoctorName;
                    DoctorAddressCodeID.Value = data.DoctorAddressCode;
                    DoctorAddressID.Value = data.DoctorAddress;
                    if (String.IsNullOrWhiteSpace(data.LocID.ToString()) == false)
                    {
                        DoctorSubLocationID.SelectedValue = data.LocID.ToString();
                    }
                    //DoctorSubLocationID.SelectedItem.Text = data.SubLocationName.ToString();
                    DoctorPhone1ID.Value = data.DoctorPhone;
                    DoctorPhone2ID.Value = data.DoctorPhone2;
                    DoctorPhone3ID.Value = data.DoctorPhone3;
                    DoctorPhone4ID.Value = data.DoctorPhone4;
                    DoctorTitleID.Value = data.DoctorInfo;
                    DoctorNotesID.Value = data.DoctorNotes;
                    DoctorLangitudeID.Value = data.DocLong;
                    DoctorLatitudeID.Value = data.DocLat;
                    DoctorCategoryID.SelectedValue = data.Doc_cat.ToString();

                    if (data.Discount == 0)
                    {
                        ISDCat.Checked = false;
                    }
                    else
                    {
                        ISDCat.Checked = true;
                    }
                    if (data.QNB == 1 && data.Large == 0 && data.Meduim == 0 && data.Discount == 0)
                    {
                        DoctorNetworkID.SelectedValue = "1";
                    }
                    else if (data.QNB == 1 && data.Large == 1 && data.Meduim == 0 && data.Discount == 0)
                    {
                        DoctorNetworkID.SelectedValue = "2";
                    }
                    else if (data.QNB == 1 && data.Large == 1 && data.Meduim == 1 && data.Discount == 0)
                    {
                        DoctorNetworkID.SelectedValue = "3";
                    }
                }
            }
            else
            {
                clean();
            }
        }
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                string str = DoctorNetworkID.SelectedItem.Text;
                if (DropDownList1.SelectedItem.Text != "Choose Doctor")
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
                                mdb.UpdateDoctorDeatails(int.Parse(DropDownList1.SelectedValue), DoctorNameID.Value, DoctorAddressCodeID.Value, DoctorAddressID.Value, DoctorPhone1ID.Value,
                                    DoctorPhone2ID.Value, DoctorPhone3ID.Value, DoctorPhone4ID.Value, DoctorTitleID.Value, DoctorNotesID.Value, DoctorLangitudeID.Value,
                                    DoctorLatitudeID.Value, DoctorSubLocationID.SelectedValue.ToString(), DoctorSubLocationID.SelectedItem.ToString(), DoctorNetworkID.SelectedValue.ToString(),
                                    DoctorCategoryID.SelectedValue.ToString(), DoctorCategoryID.SelectedItem.ToString(), DNetworkCheked);

                                div_Success.Visible = true;
                                div_Error.Visible = false;

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
                else
                {
                    div_Error.InnerHtml = "Error Pleaze Select Doctor";
                    div_Error.Visible = true;
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
            DoctorAddressCodeID.Value = "";
        }



    }
}