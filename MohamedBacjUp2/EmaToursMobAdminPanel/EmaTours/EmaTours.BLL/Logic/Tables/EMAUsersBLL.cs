using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;

namespace EmaTours.BLL.Logic.Tables
{
    public class EMAUsersBLL : Repository<EMAUser>
    {
        DateTime creationDate;

        public EMAUsersBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<UserDTO> ViewAllUsers()
        {
            List<UserDTO> UserDTO = new List<UserDTO>();
          foreach (var item in  GetAll())
          {
                UserDTO.Add(new
                    UserDTO
                {
                    UserID = item.UserID,
                    UserName = item.UserName,
                     UserTypeID= (int)item.UserTypeFK,
                      
                      EmployeeEmail= item.EmployeeEmail,
                       EmployeeName= item.EmployeeName

                });

           }
            return UserDTO;
        }
        public void AddUsers(UserDTO User)
        {

            Add(new EMAUser
            {
                IsActive = true,
                CreationDate = creationDate,
                EmployeeEmail = User.EmployeeEmail,
                UserName = User.UserName,
                UserTypeFK = User.UserTypeID,
                EmployeeName = User.EmployeeName               
            });
        }
        public void EditUsers(UserDTO User)
        {
            EMAUser EditUser = Find(x => x.UserID == User.UserID).FirstOrDefault();
            EditUser.IsActive = true;
            EditUser.CreationDate = creationDate;
            EditUser.EmployeeEmail = User.EmployeeEmail;
            EditUser.UserName = User.UserName;
            //UserTypeFK = User.UserTypeID,
            EditUser.EmployeeName = User.EmployeeName;

            Edit(EditUser);
        }


        public UserDTO GetUserByID(int UserID)
        {
           EMAUser user= Find(x => x.UserID == UserID).FirstOrDefault();

            return new UserDTO
            {
                UserID = user.UserID,
                EmployeeEmail = user.EmployeeEmail,
                EmployeeName = user.EmployeeName,
                Password = user.UserPassword,
                UserName = user.UserName,
                
            };
        }


        public bool Login(string UserName,string Password,out string Message)
        {
          if(  Find(x => x.UserName == UserName && x.UserPassword == Password).Count() > 0)
            {
                Message = "Sucess";
                return true;
            }
            else 
            {
                Message = "Wrong User Name Or Password";
                return false;
            }

        }





        }

}
