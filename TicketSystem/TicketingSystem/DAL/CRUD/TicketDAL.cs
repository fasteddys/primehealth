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
        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
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

        public List<Ticket> GetAllTicketsByUserIDwitfiltreation(int TypeID, List<int> listofpermissonsIDs,int UserID)
        {
            var tickets = 
            ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
                 from post in Db.Tickets
                from item in listofpermissonsIDs 
                 where
                 (post.Ticket_type_ID == TypeID && item == post.TicketSubtypeFK) ||
                 ( post.User_ID== UserID&& post.Ticket_type_ID == TypeID) 
                 || (post.Assign_Person_ID == UserID && post.Ticket_type_ID == TypeID)
                 select post).Distinct().ToList();
            return tickets;

        }

        public List<Ticket> GetAllTicketsByByTypeIDAndStatuswitfiltreation(int TypeID, int statusID, List<int> listofpermissonsIDs,int UserID)
        {
        //    var tickets =
        //    ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
        //         from post in Db.Tickets
        //         join item in listofpermissonsIDs on post.TicketSubtypeFK equals item into lrs
        //         from item in lrs.DefaultIfEmpty()
        //         where post.Ticket_type_ID == TypeID && (post.Status_ID== statusID||post.User_ID== UserID ||post.Assign_Person_ID==UserID)
        //         select post).ToList();
        //    return tickets;


            var tickets =
          ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
               from post in Db.Tickets
               from item in listofpermissonsIDs
               where
               (post.Ticket_type_ID == TypeID && item == post.TicketSubtypeFK && post.Status_ID == statusID) ||
               (post.User_ID == UserID && post.Ticket_type_ID == TypeID && post.Status_ID == statusID)
               || (post.Assign_Person_ID == UserID && post.Ticket_type_ID == TypeID && post.Status_ID == statusID)
               select post).Distinct().ToList();
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
