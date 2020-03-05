//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using DAL.CRUD;
//using Microsoft.Exchange.WebServices.Data;

//namespace TickitingSystem.Models
//{
//    public class Mailing
//    {
//        public static void Send_Mail_NewTicket(int userid, int? tickettype, string supject, int? ticketid)

//        {
//            User_DAL userDAL = new User_DAL();
//            string mailtosendto = null;
//            var user = userDAL.GetUserById(userid);

//            if (tickettype == 1)
//            {
//                mailtosendto = "DQ-SYS@Prime-Health.org";
//            }
//            if (tickettype == 2)
//            {
//                mailtosendto = "ITF-SYS@Prime-Health.org";
//            }
//            ExchangeService service = new ExchangeService();
//            service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
//            service.UseDefaultCredentials = false;
//            service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
//            EmailMessage message = new EmailMessage(service);
//            message.Subject = "New Ticket :" + supject;
//            message.Body = "Dear IT Team" + "  , " + "\t\r" + user.User_Name + " has just Opened a New Ticket  " +
//              "To View ticket Login and Visit http://localhost:55341/Ticket/ManageTicket?id=" + ticketid;
//            message.From = "Ticket.System@Prime-Health.org";
//            message.ToRecipients.Add(mailtosendto);
//            message.CcRecipients.Add(user.E_mail);
//            message.Save();
//            message.SendAndSaveCopy();
//        }
//        public static void Send_Mail_CloseTicket(int? userid, int? tickettype, string supject, int? ticketid)

//        {
//            User_DAL userDAL = new User_DAL();
//            string mailtosendto = null;
//            var user = userDAL.GetUserById(userid);

//            if (tickettype == 1)
//            {
//                mailtosendto = "DQ-SYS@Prime-Health.org";
//            }
//            if (tickettype == 2)
//            {
//                mailtosendto = "ITF-SYS@Prime-Health.org";
//            }
//            ExchangeService service = new ExchangeService();
//            service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
//            service.UseDefaultCredentials = false;
//            service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
//            EmailMessage message = new EmailMessage(service);
//            message.Subject = "Ticket Closed :" + supject;
//            message.Body = "Dear IT Team" + "  , " + "\t\r" + user.User_Name + " has just Closed Ticket  " +
//                "To View ticket Login and Visit http://localhost:55341/Ticket/ManageTicket?id=" + ticketid;
//            message.From = "Ticket.System@Prime-Health.org";
//            message.ToRecipients.Add(mailtosendto);
//            message.CcRecipients.Add(user.E_mail);
//            message.Save();
//            message.SendAndSaveCopy();
//        }
//        public static void Send_Mail_ReOpenTicket(int? userid, int? tickettype, string supject, int? ticketid)

//        {
//            User_DAL userDAL = new User_DAL();
//            string mailtosendto = null;
//            var user = userDAL.GetUserById(userid);

//            if (tickettype == 1)
//            {
//                mailtosendto = "DQ-SYS@Prime-Health.org";
//            }
//            if (tickettype == 2)
//            {
//                mailtosendto = "ITF-SYS@Prime-Health.org";
//            }
//            ExchangeService service = new ExchangeService();
//            service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
//            service.UseDefaultCredentials = false;
//            service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
//            EmailMessage message = new EmailMessage(service);
//            message.Subject = "Ticket Reopened :" + supject;
//            message.Body = "Dear IT Team" + "  , " + "\t\r" + user.User_Name + " has just ReOpened Ticket  " +
//                "To View ticket Login and Visit http://localhost:55341/Ticket/ManageTicket?id=" + ticketid;
//            message.From = "Ticket.System@Prime-Health.org";
//            message.ToRecipients.Add(mailtosendto);
//            message.CcRecipients.Add(user.E_mail);
//            message.Save();
//            message.SendAndSaveCopy();
//        }
//        public static void Send_Mail_ReplyTicket(int? userid, int? tickettype, string supject, int? ticketid)

//        {
//            User_DAL userDAL = new User_DAL();
//            TicketDAL gettickt = new TicketDAL();
//            var ticket = gettickt.GetByTicketID(ticketid);

//            string mailtosendto = null;
//            string mailtoCC = null;
//            var useropenedticket = userDAL.GetUserById(ticket.User_ID);
//            var userreplied = userDAL.GetUserById(userid);
//            string messagebody = null;
//            if (userreplied.Dept_ID == 1)
//            {
//                mailtosendto = useropenedticket.E_mail;
//                messagebody = "Dear " + useropenedticket.User_Name + "  , " + "\t\r" + "IT Team has just Replied to your Ticket ";
//                if (tickettype == 1)
//                {
//                    mailtoCC = "DQ-SYS@Prime-Health.org";
//                }
//                if (tickettype == 2)
//                {
//                    mailtoCC = "ITF-SYS@Prime-Health.org";
//                }
//            }
//            if (userreplied.Dept_ID != 1)
//            {
//                mailtoCC = useropenedticket.E_mail;
//                messagebody = "Dear IT Team  , " + "\t\r" + useropenedticket.User_Name + " has just Replied on his Ticket ";
//                if (tickettype == 1)
//                {
//                    mailtosendto = "DQ-SYS@Prime-Health.org";
//                }
//                if (tickettype == 2)
//                {
//                    mailtosendto = "ITF-SYS@Prime-Health.org";
//                }
//            }



//            ExchangeService service = new ExchangeService();
//            service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
//            service.UseDefaultCredentials = false;
//            service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
//            EmailMessage message = new EmailMessage(service);
//            message.Subject = "Ticket Reply :" + supject;
//            message.Body = messagebody + "\t\r" +
//                "To View ticket Login and Visit http://localhost:55341/Ticket/ManageTicket?id=" + ticketid;
//            message.From = "Ticket.System@Prime-Health.org";
//            message.ToRecipients.Add(mailtosendto);
//            message.CcRecipients.Add(mailtoCC);
//            message.Save();
//            message.SendAndSaveCopy();
//        }
//        public static void Send_Mail_OpenOutScourseTicket(int? userid, int? tickettype, string supject, int? ticketid, int? outsourceid)

//        {
//            User_DAL userDAL = new User_DAL();
//            string mailtoCC = null;
//            string ticktname = null;
//            var user = userDAL.GetUserById(userid);

//            if (tickettype == 1)
//            {
//                ticktname = "DataQuest";
//                mailtoCC = "DQ-SYS@Prime-Health.org";
//            }
//            if (tickettype == 2)
//            {
//                ticktname = "IT Fusion";
//                mailtoCC = "ITF-SYS@Prime-Health.org";
//            }
//            ExchangeService service = new ExchangeService();
//            service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
//            service.UseDefaultCredentials = false;
//            service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
//            EmailMessage message = new EmailMessage(service);
//            message.Subject = "Ticket opened with :" + ticktname + ", Subject:" + supject;
//            message.Body = "Dear " + user.User_Name + "  , " + "\t\r" + "Kindly be informed that we opened a new ticket  with " + ticktname + ", Request ID :" + outsourceid + "\t\r" +
//                "To View ticket Login and Visit http://localhost:55341/Ticket/ManageTicket?id=" + ticketid;
//            message.From = "Ticket.System@Prime-Health.org";
//            message.ToRecipients.Add(user.E_mail);
//            message.CcRecipients.Add(mailtoCC);
//            message.Save();
//            message.SendAndSaveCopy();
//        }
//    }
//}
