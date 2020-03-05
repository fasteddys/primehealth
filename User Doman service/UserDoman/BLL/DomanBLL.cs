using UserDomain.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GetAttendanceAccessControl.DLL;

namespace UserDoman.BLL
{
   public class DomanBLL
    {
        public List<UserDomain.DTO.UserDomain> GetALLDomainUsers()
        {
            List<UserDomain.DTO.UserDomain> listUserDomain = new List<UserDomain.DTO.UserDomain>();
            string DomainPath = "LDAP://DC=Primehealth,DC=local";
            DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("usergroup");
            search.PropertiesToLoad.Add("displayname");
            search.PropertiesToLoad.Add("department");
            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {


                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    result = resultCol[counter];

                    if (result.Properties.Contains("samaccountname") &&
                               result.Properties.Contains("mail") &&
                          result.Properties.Contains("displayname") &&
                          result.Properties.Contains("department")&&
                           
                    !Convert.ToBoolean(Convert.ToInt16( result.Properties["userAccountControl"]) & 0x0002)
                        )

                    {
                        UserDomain.DTO.UserDomain objSurveyUsers = new UserDomain.DTO.UserDomain();
                        objSurveyUsers.Email = (String)result.Properties["mail"][0];
                        objSurveyUsers.Name = (String)result.Properties["samaccountname"][0];
                        objSurveyUsers.Department = (string)result.Properties["department"][0];

                        // objSurveyUsers.active = (String)result.Properties["displayname"][0];

                        listUserDomain.Add(objSurveyUsers);
                    }

                }

            }
            return listUserDomain;



        }

        public void InsertUsersInHRMS(List<UserDomain.DTO.UserDomain> DomainUsers)
        {
            List<User> UserHRMS = new List<User>();
            foreach (var item in DomainUsers)
            {
                UserHRMS.Add(new User
                {
                     UserName= item.Name,
                     UserEmail=item.Email,


                });

            }
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                foreach (var item in UserHRMS)
                {
                    if (HRMS.Users.Where(x => x.AccessControlUserFK == item.AccessControlUserFK).Count() < 1)
                    {
                        HRMS.Users.Add(item);
                        HRMS.SaveChanges();

                    }
                }
            }


        }

       
    }
}
