using DAL.CRUD;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
  
          public class AttachmentDAL
        {
        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
            //Add new Attachment
            public   void ADDAttachment(Attachment attachment)
            {
                Db.Attachments.Add(attachment);
                Db.SaveChanges();
            }
            //Edit a Attachment
            public   void EditAttachment(Attachment attachment)
            {

                Db.Attachments.Attach(attachment);
                Db.Entry(attachment).State = EntityState.Modified;
                Db.SaveChanges();

            }
    
            //Select one Attachment By ID
            public   Attachment GetByAttachmentsID(int ID)
            {


                return Db.Attachments.Where(x => x.ID == ID).SingleOrDefault();

            }
            //Select one Attachment By ticket ID
            public   List<Attachment> GetAttachmentsByTicketDetailsID(int ID)
            {


                return Db.Attachments.Where(x => x.Ticket_Details_ID == ID).ToList();

            }
            //Select using customized exprission from Ticket attachment
            public   List<Attachment> GetBycustomizedExpTicketattachment(Expression<Func<Attachment, bool>> expression)
            {
                return Db.Attachments.Where(expression).ToList();
            }

        public   List<Attachment> GetAttachmentsByTicketID(int ID)
        {


            return Db.Attachments.Where(x => x.Ticket_ID == ID).ToList();

        }


    }
    }

