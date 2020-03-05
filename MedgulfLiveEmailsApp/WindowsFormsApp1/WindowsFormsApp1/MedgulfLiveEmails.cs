using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MedgulfLiveEmails : Form
    {
        int i = 0;
        bool Login = false;
        public MedgulfLiveEmails()
        {
            InitializeComponent();
        }

        private async void webBrowser1_DocumentCompletedAsync(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            //((Control)CallCenterEMail).Enabled = false;
            if (Login == false)
            {
                try
                {
                    if (i == 1)
                    {
                        CallCenterEMail.Document.Body.GetElementsByTagName("input")[0].InnerText = "callcenter1@prime-health.org";
                        //first form
                        HtmlElementCollection elc = this.CallCenterEMail.Document.GetElementsByTagName("input");
                        foreach (HtmlElement el in elc)
                        {
                            if (el.GetAttribute("type").Equals("submit") && el.GetAttribute("id").Equals("idSIButton9"))
                            {
                                await Task.Delay(500);
                                el.InvokeMember("Click");
                            }
                        }
                    }
                    i = i + 1;

                    //sign in
                    await Task.Delay(1500);
                    //second form
                    HtmlElementCollection Formno2 = this.CallCenterEMail.Document.GetElementsByTagName("input");
                    foreach (HtmlElement els in Formno2)
                    {
                        //var x = els.GetAttribute("type");

                        if (els.GetAttribute("type").Equals("password"))
                        {
                            els.SetAttribute("value", "UnBreakable@123");
                            continue;
                        }
                        else if (els.GetAttribute("type").Equals("submit") && els.GetAttribute("id").Equals("idSIButton9"))
                        {
                            els.Focus();

                            await Task.Delay(300);
                            els.InvokeMember("Click");

                            break;
                        }
                    }
                    //third form
                    await Task.Delay(500);

                    HtmlElementCollection Formno3 = this.CallCenterEMail.Document.GetElementsByTagName("input");
                    foreach (HtmlElement els in Formno3)
                    {
                        //var x = els.GetAttribute("type");

                        if (els.GetAttribute("type").Equals("checkbox"))
                        {
                            els.SetAttribute("checked", "true");
                            await Task.Delay(1000);

                            continue;
                        }
                        else if (els.GetAttribute("type").Equals("submit") && els.GetAttribute("id").Equals("idSIButton9"))
                        {
                            els.Focus();

                            await Task.Delay(300);
                            els.InvokeMember("Click");

                            break;
                        }
                    }
                    //login before
                    HtmlElementCollection Formno4 = this.CallCenterEMail.Document.GetElementsByTagName("div");
                    foreach (HtmlElement els in Formno4)
                    {
                        //  var x = els.GetAttribute("data-test-id");
                        //var z = els.GetAttribute("class");
                        if (els.GetAttribute("role").Equals("option") && els.GetAttribute("data-test-id") == "callcenter1@prime-health.org")
                        {
                            els.Focus();

                            els.InvokeMember("Click");

                            await Task.Delay(1000);

                            HtmlElementCollection Formno5 = this.CallCenterEMail.Document.GetElementsByTagName("input");
                            foreach (HtmlElement elss in Formno5)
                            {
                                //var x = els.GetAttribute("type");

                                if (elss.GetAttribute("type").Equals("password"))
                                {
                                    elss.SetAttribute("value", "UnBreakable@123");
                                    break;
                                }
                            }
                            await Task.Delay(700);
                            HtmlElementCollection Formno6 = this.CallCenterEMail.Document.GetElementsByTagName("input");
                            foreach (HtmlElement elss in Formno6)
                            {
                                if (elss.GetAttribute("type").Equals("submit") && elss.GetAttribute("id").Equals("idSIButton9"))
                                {
                                    elss.Focus();
                                    await Task.Delay(1000);
                                    elss.InvokeMember("Click");

                                    break;
                                }
                            }
                            await Task.Delay(1000);
                            HtmlElementCollection Formno7 = this.CallCenterEMail.Document.GetElementsByTagName("input");
                            foreach (HtmlElement elsss in Formno7)
                            {
                                //var x = els.GetAttribute("type");

                                if (elsss.GetAttribute("type").Equals("checkbox"))
                                {
                                    elsss.Focus();
                                    elsss.SetAttribute("checked", "true");
                                    await Task.Delay(1000);

                                    continue;
                                }
                                else if (elsss.GetAttribute("type").Equals("submit") && elsss.GetAttribute("id").Equals("idSIButton9"))
                                {
                                    elsss.Focus();

                                    elsss.InvokeMember("Click");

                                    break;
                                }
                            }





                            break;



                        }
                    }


                }



                catch (Exception ex) { }
                Login = true;
            }
            else
            {
                ((Control)CallCenterEMail).Enabled = true;
            }
        }
    



            //((Control)webBrowser1).Enabled = true;
            //Cursor.();

         //   Application.UseWaitCursor = true;

        

        private void Form1_Load(object sender, EventArgs e)
        {
            CallCenterEMail.Navigate("https://login.microsoftonline.com/common/oauth2/authorize?client_id=00000002-0000-0ff1-ce00-000000000000&redirect_uri=https%3a%2f%2foutlook.office365.com%2fowa%2f&resource=00000002-0000-0ff1-ce00-000000000000&response_mode=form_post&response_type=code+id_token&scope=openid&msafed=0&client-request-id=5e74f3fe-1d2b-42b1-9381-71b70fde9254&protectedtoken=true&nonce=636981797296201327.18c85888-5767-4068-b102-7477a6b99e91&state=DcsxEoAwCAVRouNxMEAiH46TONaWXt8Ub7stRLQv21JkheDNMxQJSzfRZjg17rgigi84uIsHTxVjdGD4zHxSy3qP-n6j_g");


        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
