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
   public  class TicketDAL
    {
        TicketingDBEntities Db = new TicketingDBEntities();
        //Add new Ticket
        public   void ADDTicket(Ticket Ticket)
        {
            Db.Tickets.Add(Ticket);
            Db.SaveChanges();
        }
        //Edit a Ticket
        public   void EditTicket(Ticket Ticket)
        {
            Ticket= Db.Tickets.Find(Ticket.ID);
           // Db.Tickets.Attach(Ticket);
            Db.Entry(Ticket).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All Tickets
        public   List<Ticket> GetAllTickets()
        {


            return Db.Tickets.ToList();

        }
        //Select one Ticket By ID
        public   Ticket GetByTicketID(int? ID)
        {


            return Db.Tickets.Where(x => x.ID == ID).SingleOrDefault();

        }
        //Select using customized exprission from Ticket
        public   List<Ticket> GetBycustomizedExpTicket(Expression<Func<Ticket, bool>> expression)
        {
            return Db.Tickets.Where(expression).ToList();
        }
        public   List<Ticket> GetAllTicketsByUserID(int id , int typeid )
        {
            var tickets = (from t in Db.Tickets where t.User_ID == 
                           id && t.Ticket_type_ID==typeid select t).ToList();

            return tickets;

        }
        public   List<Ticket> GetAllTicketsByTypeID(int id)
        {
            var tickets = (from t in Db.Tickets where t.Ticket_type_ID == id select t).ToList();

            return tickets;

        }
        public   List<Ticket> GetAllTicketsByTypeIDAndStatusandUserID(int UserID,int id , int statusID)
        {
            var tickets = (from t in Db.Tickets where t.Ticket_type_ID == id && t.Status_ID==statusID && UserID==t.User_ID select t ).ToList();
            return tickets;
        }
        public   List<Ticket> GetAllTicketsByTypeIDAndStatus( int id, int statusID)
        {
            var tickets = (from t in Db.Tickets where t.Ticket_type_ID == id && t.Status_ID == statusID select t).ToList();
            return tickets;
        }
        public   List<Ticket> GetAllTicketsByTypeIDAndAssignP(int id, int Assignp)
        {
            var tickets = (from t in Db.Tickets where t.Ticket_type_ID == id && t.Assign_Person_ID == Assignp select t).ToList();
            return tickets;
        }

    }
}
