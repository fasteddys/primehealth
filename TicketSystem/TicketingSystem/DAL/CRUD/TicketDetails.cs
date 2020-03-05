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
    public class TicketDetail
    {

        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();

        User_DAL userDAL = new User_DAL();
        TicketDAL ticketDAL = new TicketDAL();


        AttachmentDAL attachmentDAL = new AttachmentDAL();
        StatusDAL statusDAL = new StatusDAL();



        //Add new Ticket_Detail
        public void ADDTicket_Detail(Ticket_Details Details)
        {
            //  Details. = null;
            Db.Ticket_Details.Add(Details);
            Db.SaveChanges();
        }
        //Edit a Ticket_Detail
        public void EditTicket_Detail(Ticket_Details Details)
        {
            Db.Ticket_Details.Attach(Details);
            Db.Entry(Details).State = EntityState.Modified;
            Db.SaveChanges();
        }
        //Select All Ticket_Detail
        public List<Ticket_Details> GetAllTicket_Detail()
        {
            return Db.Ticket_Details.AsNoTracking().ToList();
        }
        //Select one Ticket_Detail By ID
        public Ticket_Details GetByTicket_DetailsID(int ID)
        {


            return Db.Ticket_Details.Where(x => x.TicketDetails_ID == ID).SingleOrDefault();

        }
        //Select one Ticket_Detail By ticket ID
        public List<Ticket_Details> GetTicket_DetailsByTicketID(int ID)
        {

            return Db.Ticket_Details.Where(x => x.TicketDetails_ID == ID).ToList();
        }
        //Select using customized exprission from Ticket Details
        public List<Ticket_Details> GetBycustomizedExpTicketDetails(Expression<Func<Ticket_Details, bool>> expression)
        {
            return Db.Ticket_Details.Where(expression).ToList();
        }
        public List<Ticket_Details> GetAllTicket_DetailsByTicketID(int ID)
        {

            return Db.Ticket_Details.Where(x => x.Ticket_ID == ID).ToList();
        }
        public void AddAssignEvent(int tid, int? userid)
        {
            Ticket_Details newdetails = new Ticket_Details();
            newdetails.Ticket_ID = tid;
            newdetails.Date = DateTime.Now;
            var user = userDAL.GetUserById(userid);
            newdetails.Notes = "Ticket Assigned By " + user.User_Name;
            newdetails.User_ID = userid;
           // newdetails.Seen_By_User = "seen";
          //  newdetails.Seen_By_IT = "seen";


            ADDTicket_Detail(newdetails);
        }
        public void AddCloseEvent(int tid, int userid)
        {
            Ticket_Details newdetails = new Ticket_Details();
            newdetails.Ticket_ID = tid;
            newdetails.Date = DateTime.Now;
            newdetails.Notes = "Ticket Closed";
            newdetails.User_ID = userid;
            ADDTicket_Detail(newdetails);
        }
        public void AddReOpenEvent(int tid, int userid)
        {
            Ticket_Details newdetails = new Ticket_Details();
            newdetails.Ticket_ID = tid;
            newdetails.Date = DateTime.Now;
            newdetails.Notes = "Ticket Reopend";
            newdetails.User_ID = userid;
            ADDTicket_Detail(newdetails);
        }
        public void AddOpenDQEvent(int tid, int? dqtid, int userid)
        {
            Ticket_Details newdetails = new Ticket_Details();
            newdetails.Ticket_ID = tid;
            newdetails.Date = DateTime.Now;
            newdetails.Notes = "DataQuest Ticket Opened With ID" + dqtid;
            newdetails.User_ID = userid;
            ADDTicket_Detail(newdetails);
        }
        public void AddOpenITFEvent(int tid, int? itftid, int userid)
        {
            Ticket_Details newdetails = new Ticket_Details();
            newdetails.Ticket_ID = tid;
            newdetails.Date = DateTime.Now;
            newdetails.Notes = "IT Fusion Ticket Opened With ID" + itftid; ;
            newdetails.User_ID = userid;
            ADDTicket_Detail(newdetails);
        }
        public void updatelastdetails(int tid, int? uid)
        {
            var userdep = (from u in Db.Users where u.User_ID == uid select u.Dept_ID).SingleOrDefault();

            var details = (from t in Db.Ticket_Details where t.Ticket_ID == tid orderby t.TicketDetails_ID descending select t).ToList();
            foreach (var item in details)
            {
                if (userdep == 1)
                {
                    item.Seen_By_IT = "seen";
                }
                else
                {
                    item.Seen_By_User = "seen";
                }
            }
            Db.SaveChanges();
        }
    }
}
