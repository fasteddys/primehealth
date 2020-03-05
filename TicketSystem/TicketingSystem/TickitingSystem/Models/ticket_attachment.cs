using DAL.CRUD;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TickitingSystem.Models
{
    public class ticket_attachment
    {
     public   Ticket_Details ticket = new Ticket_Details();
        public List< Attachment> attachment = new List<Attachment> ();

    }
}