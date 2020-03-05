using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
  public  class SubTyspe_DAL
    {
        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
        //Add new UserToRole
        public void ADDSubTyspe(TicketSubtype UserToRole)
        {
            Db.TicketSubtypes.Add(UserToRole);
            Db.SaveChanges();
        }
        //Edit a UserToRole
        public void EditSubTyspe(TicketSubtype UserToRole)
        {
            UserToRole = Db.TicketSubtypes.Find(UserToRole.ID);
            // Db.UserToRoles.Attach(UserToRole);
            Db.Entry(UserToRole).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All UserToRoles

        //Select one UserToRole By ID
        public List< TicketSubtype> GetAllSubTyspe()
        {


            return Db.TicketSubtypes.ToList();

        }
        public TicketSubtype GetSubTyspe(int? ID)
        {


            return Db.TicketSubtypes.Where(x => x.ID == ID).SingleOrDefault();

        }
        //Select using customized exprission from UserToRole
        public List<TicketSubtype> GetBycustomizedExpUserToRole(Expression<Func<TicketSubtype, bool>> expression)
        {
            return Db.TicketSubtypes.Where(expression).ToList();
        }


    }
}
