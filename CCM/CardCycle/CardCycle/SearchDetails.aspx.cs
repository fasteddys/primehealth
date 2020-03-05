using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardCycle.BLL;

namespace CardCycle
{
    public partial class SearchDetails : System.Web.UI.Page
    {
        Request mdb = new Request();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"].ToString());
                var data = mdb.GETDAtaDeatailsSearch(id);
                txtSubject.Text = data.First().rSubject;
                txtBsubject.Text = data.First().rSubject;
                txtType.Text = data.First().rType;
                txtAM.Text = data.First().rFrom;
                txtAMTime.Text = data.First().rdate;
                txtISname.Text = data.First().apvIssuance;
                Isstime.Text = data.First().LogIssuance;
                txtPname.Text = data.First().apvPrint;
                txtPTime.Text = data.First().LogPrint;
                txtQC.Text = data.First().apvQControl;
                txtQCTime.Text = data.First().LogQC;
                txtISappv.Text = data.First().isApproved;
                txtapvBy.Text = data.First().aproveBy;
                txtClientName.Text = data.First().ClientName;
                txtCardsNumber.Text = data.First().CardsNum.ToString();
                txtAdminConfirm.Text = data.First().ISSExcepAppUser;
                txtAdminTime.Text = data.First().ISSExcepAppDate;
                txtAccountConfirm.Text = data.First().AccExcepAppUser;
                txtAccountTime.Text = data.First().AccExcepAppDate;
            }
            


        }
    }
}