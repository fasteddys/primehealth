using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.DTOs.Business;
using BNC.Entities;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class UserBLL : Repository<User>
    {
        TeamBLL teamBLL;
        DateTime creationDate;

        public UserBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            teamBLL = new TeamBLL(Context, CreationDate);
            creationDate = CreationDate;
        }

        public UserDTO Login(UserDTO UserData)
        {

          //var z= CustomSelection("UserName,UserID",x=>x.UserID==1).ToList();
            //var user = new User();
            User user =  Find(x => x.UserName == UserData.UserName&&x.UserPassword== UserData.Password).FirstOrDefault();
            
            if (user == null)
            {
                return null;

            }
            else
            {
                return new UserDTO
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    TeamName= teamBLL.Find(x => x.TeamID == user.TeamFK).FirstOrDefault().TeamName,
                     TeamID= (int)user.TeamFK
                };
            }
          
        }

        public List<UserDTO> GetDataEntryUsers()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();

            int providerTeamID = teamBLL.Find(x => x.TeamName == "DataEntry" && x.IsActive == true).FirstOrDefault().TeamID;
            List<User> users = Find(x => x.TeamFK == providerTeamID && x.IsActive == true).ToList();

            foreach(var items in users)
            {
                userDTOs.Add(new UserDTO
                {
                    UserID = items.UserID,
                    UserName = items.UserName
                });
            }

            return userDTOs;
        }
        //---------------------------------------------------
        public List<UserDTO> getUserData(int teamFk)
        {
            List<UserDTO> userDataList = new List<UserDTO>();
            UserDTO tempuserData;
            var usersData = this.GetAll().Where(u=>u.TeamFK== teamFk);
            foreach (var userData in usersData)
            {
                tempuserData = new UserDTO();
                tempuserData.UserID = userData.UserID;
                tempuserData.UserName = userData.UserName;
                userDataList.Add(tempuserData);
            }
            return userDataList;
        }
        //--------------------------------------------------------
        public string getUserName(int? UserFk)
        {
            if (UserFk != null)
            {
                return Find(u => u.UserID == UserFk).FirstOrDefault().UserName;

            }
            return "Not Assign to any user";
        }
        //--------------------------------------------------------
        public List<UserDTO> GetUsersData()
        {
            var allUsers = this.GetAll().ToList();
            List<UserDTO> usersList = new List<UserDTO>();
            UserDTO tempUserData;
            foreach (var user in allUsers)
            {
                tempUserData = new UserDTO();
                tempUserData.UserID = user.UserID;
                tempUserData.UserName = user.UserName;
                usersList.Add(tempUserData);
            }
            return usersList;
        }
        //--------------------------------------------------------
        public bool CheckUserTeam(int UserID, int TeamID)
        {
            User user = Find(x => x.UserID == UserID).FirstOrDefault();

            if (user.TeamFK == TeamID || user.TeamFK == (int)StaticData.StaticEnums.Team.Admin || user.TeamFK == (int)StaticData.StaticEnums.Team.Provider)
                return true;
            else
                return false;
        }
    }
}
