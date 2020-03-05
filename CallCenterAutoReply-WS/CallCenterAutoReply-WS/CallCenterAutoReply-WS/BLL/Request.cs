using CallCenterAutoReply_WS.Model;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterAutoReply_WS.BLL
{
    public class Request
    {
        public void GetNewRequests()
        {
            using (PhNetworkEntities Entities = new PhNetworkEntities())
            {
                var UnSentRequests = Entities.EmailApprovalReceivingReceipts.Where(x => x.IsSent == false).Select(x => new { x.EmailApprovalReceivingReceiptID, x.EmailApprovalRequestFK, x.EmailApprovalReceivingReceiptTo}).ToList();

                foreach(var items in UnSentRequests)
                {
                    try
                    {
                        Email.SendEmail(items.EmailApprovalRequestFK, items.EmailApprovalReceivingReceiptTo);

                        var Request = Entities.EmailApprovalReceivingReceipts.Where(x => x.EmailApprovalReceivingReceiptID == items.EmailApprovalReceivingReceiptID).FirstOrDefault();

                        Request.IsSent = true;
                        Request.ModificationDate = DateTime.Now;

                        Entities.EmailApprovalReceivingReceipts.AddOrUpdate(Request);
                    }
                    catch (Exception ex)
                    {
                        Email.WriteExceptionToFile(ex.Message);
                    }

                    //if (Email.SendEmail(items.EmailApprovalRequestFK, items.EmailApprovalReceivingReceiptTo))
                    //{
                    //    var Request = Entities.EmailApprovalReceivingReceipts.Where(x => x.EmailApprovalReceivingReceiptID == items.EmailApprovalReceivingReceiptID).FirstOrDefault();

                    //    Request.IsSent = true;
                    //    Request.ModificationDate = DateTime.Now;

                    //    Entities.EmailApprovalReceivingReceipts.AddOrUpdate(Request);
                    //}
                }

                Entities.SaveChanges();
            }
        }
       
    }
}
