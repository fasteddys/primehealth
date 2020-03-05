using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
   public   class User_DAL
    {
        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
        // add new user 
        public   void ADD(User User)
        {
            Db.Users.Add(User);
            Db.SaveChanges();
          
        }
        // Edit some of user data 
        public   void EditUser(User user)
        {
            var getuser = (from u in Db.Users where u.User_ID == user.User_ID select u).SingleOrDefault();
            getuser.User_Name = user.User_Name;
            getuser.Dept_ID = user.Dept_ID;
            getuser.Sub_Dept_ID = user.Sub_Dept_ID;
            getuser.E_mail = user.E_mail;
            getuser.User_Status = user.User_Status;
            Db.SaveChanges();
        }
        // change password of user 
        public   void ChangeUserPassWord(int ID , string OldPAssword , string NewPassword )
        {
            var getuser = (from u in Db.Users where u.User_ID == ID select u).SingleOrDefault();
            if (getuser.Password==OldPAssword)
            {
                getuser.Password = NewPassword;
                Db.SaveChanges();
            }
        }
        // delete user by changing his status to "Deleted"
        public   void DeleteUser(User user)
        {
            var getuser = (from u in Db.Users where u.User_ID == user.User_ID select u).SingleOrDefault();
            getuser.User_Status = "Deleted";
            Db.SaveChanges();
        }
        // get user by id 
        public   User GetUserById(int? id)
        {
            User user = (from c in Db.Users where c.User_ID == id select c).SingleOrDefault();
            return (user);
        }
        // get users by department
        public   List<User> GetUsersByDepratment (int DepID)
        {
            var users = (from u in Db.Users where u.Dept_ID == DepID select u).ToList();
            return (users);
        }
        // get users by supdepartment
        public   List<User> GetUsersBySupDepratment(int SupDepID)
        {
            var users = (from u in Db.Users where u.Sub_Dept_ID == SupDepID select u).ToList();
            return (users);
        }
        //get all users 
        public   List<User> GetAllUsers()
        {
            var users = (from u in Db.Users select u).ToList();
            return (users);
        }
        // when login check if existing user check for the correct password
        public   string Login (int id , string password)
        {
            User getuser = (from u in Db.Users where u.User_ID == id select u).SingleOrDefault();
            if (getuser!=null)
            {
                if (getuser.Password==password)
                {
                    if (getuser.User_Status == "DELETED")
                    {
                        return ("deleteduser");
                    }
                    else 
                    {
                        return ("success");
                    }
                    
                }
                 if (getuser.Password != password)
                {
                    return ("wrongpassword");
                }
                else
                {
                    return ("wrongpassword");
                }
                 
            }
            else
            {
                return ("notfound");
            }
        }
        public bool UserExist(UserLDAP user)
        {
       if( (from c in Db.Users where c.User_Name == user.Name &c.User_Status== "active" select c).Count()==1)
            {
                return true;
            }
            else { return false; }
     
        }


        public User getuserbyname(string name)
        {
         return  (from c in Db.Users where c.User_Name == name select c).SingleOrDefault();
         

        }




    }
}
