using CallCenterAutoReply_WS.Model;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterAutoReply_WS.BLL
{
    public static class Email
    {
        public static bool SendEmail(int RequestID, string UserEmail)
        {
            using (PhNetworkEntities Entities = new PhNetworkEntities())
            {
                try
                {
                    string AutodiscoverUrl = Entities.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "NoReplyAutodiscoverUrl").FirstOrDefault().ConfigurationValue;
                    string MailingUserName = Entities.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "NoReplyMailingUserName").FirstOrDefault().ConfigurationValue;
                    string MailingPassword = Entities.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "NoReplyMailingPassword").FirstOrDefault().ConfigurationValue;
                    string MailingDomain = Entities.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "NoReplyMailingDomain").FirstOrDefault().ConfigurationValue;
                    //string MailingBody = Entities.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "BirthdayEmailTemplate").FirstOrDefault().ConfigurationValue;

                    ExchangeService service = new ExchangeService();
                    service.AutodiscoverUrl(AutodiscoverUrl);
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials(MailingUserName, MailingPassword, MailingDomain);
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "Your Request #" + RequestID;
                    message.Body = "<html>" + 
                    "<body>" +
                        "<div style = " + "text-align:right;"  + ">" + 
                             "<p>" +
                                "-:Prime Health الساده الكرام عملاء <br/><br/>" +
                                "،نشكركم على التواصل مع إدارة الموافقات الطبية<br/>" +
                                "<b>" + RequestID + "</b>" +
                                "تم أستلام طلبكم المرسل عبر البريد الالكتروني برقم " +                                
                                "<br/>" +
                                " إن العمل جاري على طلبكم وسيتم الرد على سيادتكم من قبل القسم المختص" +
                                "<b> خلال أقرب وقت ممكن </b> <br/><br/>" +                               
                                "<b>:</b>" +  
                                "<b> لضمان سرعة الرد على سيادتكم، يرجى الحرص على </b> <br/>" + 
                                "." +
                                "كتابة رقم الكارنيه الطبي في موضوع البريد الإلكتروني •" +
                                "<br/>" +
                                "." +
                                "كتابة رقم الهاتف لسهولة التواصل مع حضراتكم •" +                                
                                "<br/>" +
                                "." +
                                "إرفاق النموذج الطبي أو الروشتة الخاصة بالخدمة الطبية المطلوبة •" +
                                "<br/>" +
                                "." +
                                "تحديد نوع الخدمة المطلوبة مع توضيح اسم مقدم الخدمة وإسم الفرع •" +
                                "<br/><br/>" +
                                "<b>.الاهتمام بصحتكم هي أولويتنا </b>" +
                                "<br/><br/>" +  
                                "<b>.إدارة الموافقات الطبية</b>" +   
                                "<br/><br/>" +
                                "<b>.قد تطبّق الشروط والأحكام </b>" +                                         
                                "</p>" +     
                              "</div>" +      

                              "<div>" +
      
                                  "<p>" +
                                      "Dear Prime Health Valued Clients: - <br/><br/>" +
                                      "Thank you for contacting the Medical Approvals Department, <br/>" +
                                      "We Received your request which is sent via E - Mail with number " +
                                      "<b>"+ RequestID + "</b> <br/>" +
                                      "Your request is being processed and will be answered by the concerned department as soon as possible. <br/><br/>" +


                                      "<b> To ensure a prompt reply, please review the following: </b><br/>" +
                                      "• Mention your medical number on the subject of the E - Mail. <br/>" +
                                      "• Mention your phone number to ensure easy communication with your presence. <br/>" +
                                      "• Attach the medical form or prescription for the required medical service. <br/>" +
                                      "• Determine the type of service required with the name of the service provider and the name of the branch. <br/>" +

                                      "<br/><br/>" +

                                      "<b> Taking care of your health is our priority.</b>" +

                                      "<br/><br/>" +

                                      "<b> Medical Approvals Management.</b>" +

                                      "<br/><br/>" +

                                      "<b> Terms and conditions may apply.</b>" +

                                  "</p>" +

                              "</div>" +
                          "</body>" +
                          "</html> ";

                    message.ToRecipients.Add(UserEmail);
                    //message.BccRecipients.Add("IT-Developers@Prime-Health.org");

                    message.Save();
                    message.SendAndSaveCopy();
                    return true;
                }
                catch (Exception ex)
                {
                    WriteExceptionToFile(ex.Message);
                    return false;
                }
            }
        }

        public static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog " + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        public static void WriteExceptionToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Exceptions";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Exceptions\\ExceptionLog " + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
