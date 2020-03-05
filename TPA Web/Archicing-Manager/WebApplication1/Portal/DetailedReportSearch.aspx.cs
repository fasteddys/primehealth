using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class DetailedReportSearch : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"].ToString());
                //int id = 1200;
                var data = mdb.GETDAtaDeatailsSearch(id);
                ProvName.Text = data.First().rBody;
                txtBsubject.Text = data.First().rBody;
                Clname.Text = data.First().ClientName;

                txtCereator.Text = data.First().rFrom;
                txtCerDate.Text = data.First().rDate;

                txtDEntryName.Text = data.First().AssigenPerson;
                txtDEntryAcceptDate.Text = data.First().AssDate.ToString();

                txtQCname.Text = data.First().QualityPerson;
                txtQualityDate.Text = data.First().QDate;

                txtTBAName.Text = data.First().TBAPerson;
                txtTbaDate.Text = data.First().ApprovedDate;

                txtTotClaims.Text = data.First().TottalClaims.ToString();
                txtFoundClaims.Text = data.First().TottalFoundClaims.ToString();
                txtMissClaims.Text = data.First().TottalMissClaims.ToString();
                txtInpatientlaims.Text = data.First().InPatientClaims.ToString();
                txtWrongClaims.Text = data.First().WrongClaims.ToString();
                txtDublClaims.Text = data.First().DublicatedClaims.ToString();
                
                //txtReqState.Text = data.First().rStatus.ToString();

                //txtTotClaims.Text = data.First().aproveBy;
                //txtClientName.Text = data.First().ClintName;
                //txtCardsNumber.Text = data.First().CardsNum.ToString();
                //txtAdminConfirm.Text = data.First().ISSExcepAppUser;
                //txtAdminTime.Text = data.First().ISSExcepAppDate;
                //txtAccountConfirm.Text = data.First().AccExcepAppUser;
                //txtAccountTime.Text = data.First().AccExcepAppDate;
            }
        }
    }
}