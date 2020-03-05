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
 public    class StatusDAL
    {
          TicketingDBEntities Db = new TicketingDBEntities();
        //Add new Status
        public   void ADDStatus(Status Status)
        {
            Db.Status.Add(Status);
            Db.SaveChanges();
        }
        //Edit a Status
        public   void EditStatus(Status Status)
        {

            Db.Status.Attach(Status);
            Db.Entry(Status).State = EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All Statuss
        public   List<Status> GetAllStatuss()
        {


            return Db.Status.ToList();

        }
        //Select one Status By ID
        public   Status GetStatusByID(int ID)
        {


            return Db.Status.Where(x => x.Status_ID == ID).SingleOrDefault();

        }
        //Select using customized exprission from Status
        public   List<Status> GetStatusBycustomizedExp(Expression<Func<Status, bool>> expression)
        {
            return Db.Status.Where(expression).ToList();
        }


    }
}
