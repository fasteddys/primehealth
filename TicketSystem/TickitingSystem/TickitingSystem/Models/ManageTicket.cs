using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TickitingSystem.Models
{
    public class ManageTicket
    {
      public  Ticket ticket = new Ticket();
      public  List<Ticket_Details >ticket_Details = new List<Ticket_Details>();
       public List<Attachment> attachments = new List<Attachment>();

    }
}