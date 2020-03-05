using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
   public    class Deparments_DAL
    {

          TicketingDBEntities Db = new TicketingDBEntities();

        //Insert Department
        public   void ADD(Department Department)
        {
            Db.Departments.Add(Department);
            Db.SaveChanges();
        }


        //Get Department ById
        public   Department GetDeparmentID(int id)
        {
            return Db.Departments.Where(s => s.Dept_Id == id).SingleOrDefault();
        }


        // Edit Department
        public   void Edit(Department Department)
        {
            var getDept = (from a in Db.Departments where a.Dept_Id == Department.Dept_Id select a).SingleOrDefault();
            getDept.Dept_Id = Department.Dept_Id;
            getDept.Dept_Name = getDept.Dept_Name;
            Db.SaveChanges();
        }

        //get all depatment
        public   List< Department> GetAllDepartments()
        {
            var Department = (from c in Db.Departments select c).ToList();
            return (Department);
        }
        public Department EepartmentExist(string departmentname)
        {
           return (from c in Db.Departments where c.Dept_Name == departmentname select c).SingleOrDefault();
        
        }


    }
}
