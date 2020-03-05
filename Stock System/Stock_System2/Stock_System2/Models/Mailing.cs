using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;

namespace Stock_System2.Models
{
    public class Mailing
    {
        public void SendMail_NewRequestCreated(string UserName, string Department, string ItemName, int Count)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("Warehouse@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("Warehouse", "P@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "New Add Item";
            message.Body = "Dear  " + Department + "Team" + "  , " + "\t\r" + UserName + " has just Add you New Item  : " + "Name Is : " + ItemName + "\t\r" + "" + "   " + "Number of Adding Is : " + Count + "\t\r" + "" + "";
            message.From = "Warehouse@Prime-Health.org";
            //message.ToRecipients.Add("Mohamed.AbdElsattar@Prime-Health.org");
            //message.CcRecipients.Add(CreatorEmail);
            message.ToRecipients.Add("Ahmed.Mohamed@Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_WithdrawRequestCreated(string UserName, string Department, string ItemName, int Count)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("Warehouse@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("Warehouse", "P@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Withdrawing Item";
            message.Body = "Dear  " + Department + "Team" + "  , " + "\t\r" + UserName + " has just With you withdraw Item  : " + " Name Is : " + ItemName + "\t\r" + "" + "   " + "Number of Withdraw Is : " + Count + "\t\r" + "" + "";
            message.From = "Warehouse@Prime-Health.org";
            message.ToRecipients.Add("Ahmed.Mohamed@Prime-Health.org");
            //message.CcRecipients.Add(CreatorEmail);
            //message.CcRecipients.Add("Ahmed.Shaker@Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_DangerRequestCreated(string Department, string ItemName, int Count)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("Warehouse@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("Warehouse", "P@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Warning Limit count of Item";
            message.Body = "Dear  " + Department + "Team" + "  , " +  "Warning Limit of  Item  : " + ItemName + "\t\r" + "" + "   " + "Number of Item Is : " + Count + "\t\r" + "" + "";
            message.From = "Warehouse@Prime-Health.org";
            message.ToRecipients.Add("Ahmed.Mohamed@Prime-Health.org");
            //message.CcRecipients.Add(CreatorEmail);
            //message.CcRecipients.Add("Ahmed.Shaker@Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_AddUser(string UserName, string Password, string Email)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("Warehouse@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("Warehouse", "P@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Create New User";
            message.Body = "Dear  " + UserName  + "  , " + "\t\r" + "   : " + "New Account in Warehouse system is created for you , your Name Is : " + UserName + "\t\r" + "" + "   " + "your password  Is : " + Password + "\t\r" + "" + "";
            message.From = "Warehouse@Prime-Health.org";
            message.ToRecipients.Add("Ahmed.Mohamed@Prime-Health.org");
            //message.CcRecipients.Add(CreatorEmail);
            message.CcRecipients.Add(Email);
            message.Save();
            message.SendAndSaveCopy();
        }

    }
}