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
   public   class Ticket_TypeDAL
    {
          TicketingDBEntities Db = new TicketingDBEntities();
        //Add new Ticket_Type
        public   void ADDTicket_Type(Ticket_Types Ticket_Type)
        {
            Db.Ticket_Types.Add(Ticket_Type);
            Db.SaveChanges();
        }
        //Edit a Ticket_Type
        public   void EditTicket_Type(Ticket_Types Ticket_Type)
        {

            Db.Ticket_Types.Attach(Ticket_Type);
            Db.Entry(Ticket_Type).State = EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All Ticket_Types
        public   List<Ticket_Types> GetAllTicket_Types()
        {


            return Db.Ticket_Types.ToList();

        }
        //Select one Ticket_Type By ID
        public   Ticket_Types GetTicket_TypeByID(int ID)
        {


            return Db.Ticket_Types.Where(x => x.Ticket_Type_ID == ID).SingleOrDefault();

        }
        //Select using customized exprission from Ticket_Type
        public   List<Ticket_Types> GetTicket_TypeBycustomizedExp(Expression<Func<Ticket_Types, bool>> expression)
        {
            return Db.Ticket_Types.Where(expression).ToList();
        }


    }
}

