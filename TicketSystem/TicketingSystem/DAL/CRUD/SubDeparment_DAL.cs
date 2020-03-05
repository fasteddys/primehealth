using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
    public   class SubDeparment_DAL
    {

        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();

        //Insert SubDepartment
        public   void ADD(SubDeparment SubDepartment)
        {
            Db.SubDeparments.Add(SubDepartment);
            Db.SaveChanges();
        }

        //Get All SubDepartment By Id
        public   SubDeparment GetSubDepartmentById(int id)
        {
            SubDeparment SubDepartment = (from c in Db.SubDeparments where c.SubDept_ID == id select c).SingleOrDefault();
            return (SubDepartment);
        }

        // Edit SubDepartment
        public   void Edit(SubDeparment SubDepartment)
        {
            var getSubDept = (from a in Db.SubDeparments where a.SubDept_ID == SubDepartment.SubDept_ID select a).SingleOrDefault();
            getSubDept.SubDept_ID = SubDepartment.SubDept_ID;
            getSubDept.SubDept_Name = SubDepartment.SubDept_Name;
            Db.SaveChanges();
        }

        //get all SubDepartment
        public   List<SubDeparment> GetAllSubDepartments()
        {
            var subDepartment = (from c in Db.SubDeparments select c).ToList();
            return (subDepartment);
        }
    }
}
