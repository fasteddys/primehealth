using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class ProviderDashboard : System.Web.UI.Page
    {
        HospitalClass Hosp = new HospitalClass();
        DoctorClass Docs = new DoctorClass();
        LaboratoriesClass Labs = new LaboratoriesClass();
        PharmaciesClass Parms = new PharmaciesClass();
        OpticalClass Opt = new OpticalClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ProviderDashboard")).Attributes["class"] = "active";
            //int x1 = Hosp.HospitalCount();
            TottalHospitals.InnerHtml = Hosp.HospitalCount().ToString();
            TottalDoctors.InnerHtml = Docs.DoctorCount().ToString();
            TottalLabs.InnerHtml = Labs.LabsCount().ToString();
            TottalPharmacies.InnerHtml = Parms.PharmsCount().ToString();
            TottalOpticals.InnerHtml = Opt.OpticalsCount().ToString();
            TottalProviders.InnerHtml = (Hosp.HospitalCount() + Docs.DoctorCount() + Labs.LabsCount() + Parms.PharmsCount() + Opt.OpticalsCount()).ToString();





        }
    }
}