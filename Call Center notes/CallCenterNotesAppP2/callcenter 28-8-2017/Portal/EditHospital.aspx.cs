using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class EditHospital : System.Web.UI.Page
    {
        HospitalClass mdb = new HospitalClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int x = mdb.getID();
                if (x != -1)
                { drop_hosptial.SelectedValue = x.ToString(); }
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

        protected void drop_hosptial_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drop_hosptial.SelectedItem.Text != "Choose Hospital")
            {
                if (String.IsNullOrWhiteSpace(drop_hosptial.SelectedValue) == false)
                {
                    var data = mdb.GetHospitalData(int.Parse(drop_hosptial.SelectedValue));
                    HospitalNameID.Value = data.HospName;
                    HospitalAddressCodeID.Value = data.AddressNum;
                    HospitalAddressID.Value = data.HospAddress;


                    HospitalPhone1ID.Value = data.HospPhone;
                    HospitalPhone2ID.Value = data.HospPhone2;
                    HospitalPhone3ID.Value = data.HospPhone3;
                    HospitalPhone4ID.Value = data.HospPhone4;
                    HospitalNotesID.Value = data.HospNotes;
                    HospitalLangitudeID.Value = data.HospLong;
                    HospitalLatitudeID.Value = data.HospLat;
                    HospitalCategoryID.SelectedItem.Text = data.Category;

                    HospitalSubLocationID.SelectedValue = data.LocID.ToString();

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
                        HospitalNetworkID.SelectedValue = "1";
                    }
                    else if (data.QNB == 1 && data.Large == 1 && data.Meduim == 0 && data.Discount == 0)
                    {
                        HospitalNetworkID.SelectedValue = "2";
                    }
                    else if (data.QNB == 1 && data.Large == 1 && data.Meduim == 1 && data.Discount == 0)
                    {
                        HospitalNetworkID.SelectedValue = "3";
                    }
                }
            }
            else
            {
              clean();

            }
        }

        protected void btn_Edit_Hosptial_Click(object sender, EventArgs e)
        {
            try
            {
                if (drop_hosptial.SelectedItem.Text != "Choose Hospital")
                {
                    string DNetworkCheked;
                    if (ISDCat.Checked) { DNetworkCheked = "1"; } else { DNetworkCheked = "0"; }
                    if (HospitalSubLocationID.Text != "-- Enter Sublocation Name --" && HospitalNetworkID.Text != "-- Enter Network type --" && HospitalCategoryID.Text != "-- Enter Category Name --")
                    {
                        HospitalPhone2ID.Value = SubLocationCode.Value + HospitalPhone2ID.Value;
                        mdb.UpdateHospitalDeatails(int.Parse(drop_hosptial.SelectedValue), HospitalNameID.Value, HospitalAddressCodeID.Value, HospitalAddressID.Value, HospitalPhone1ID.Value,
                            HospitalPhone2ID.Value, HospitalPhone3ID.Value, HospitalPhone4ID.Value, HospitalNotesID.Value, HospitalLangitudeID.Value,
                            HospitalLatitudeID.Value, HospitalSubLocationID.SelectedValue.ToString(), HospitalSubLocationID.SelectedItem.ToString(), HospitalNetworkID.SelectedValue.ToString(),
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
                else
                {
                    div_Error.InnerHtml = "Error Pleaze Select The Hospital";
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