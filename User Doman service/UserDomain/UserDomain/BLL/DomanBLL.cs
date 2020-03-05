using UserDomain.DLL;
using UserDomain.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.DirectoryServices;

namespace UserDoman.BLL
{
   public class DomainBLL
    {
        public List<UserDomain.DTO.UserDomain> GetALLDomainUsers()
        {
            List<UserDomain.DTO.UserDomain> ListNewUserDomain = new List<UserDomain.DTO.UserDomain>();
            List<UserDomain.DTO.UserDomain> ListDeactivatedUserDomain = new List<UserDomain.DTO.UserDomain>();

            string DomainPath = "LDAP://DC=Primehealth,DC=local";
            DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("usergroup");
            search.PropertiesToLoad.Add("displayname");
            search.PropertiesToLoad.Add("department");
            search.PropertiesToLoad.Add("userAccountControl");
            search.PropertiesToLoad.Add("telephonenumber");



            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                try
                {

                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {



                        result = resultCol[counter];

                        if (result.Properties.Contains("samaccountname") &&
                           // result.Properties.Contains("mail") &&
                            result.Properties.Contains("displayname") &&
                            result.Properties.Contains("department") &&
                            this.Disable(result)

                           )

                        {
                            UserDomain.DTO.UserDomain objSurveyUsers = new UserDomain.DTO.UserDomain();
                            try
                            {
                                objSurveyUsers.Email = (String)result.Properties["mail"][0];
                            }
                            catch { }
                                objSurveyUsers.Name = (String)result.Properties["samaccountname"][0];
                            objSurveyUsers.Department = (string)result.Properties["department"][0];
                            try
                            {
                                objSurveyUsers.ExtensionNumber = (string)result.Properties["telephonenumber"][0];
                            }

                            catch {


                            }
                          //  objSurveyUsers.active = (String)result.Properties["displayname"][0];

                            ListNewUserDomain.Add(objSurveyUsers);
                        }
                        //else if (result.Properties.Contains("samaccountname") &&
                        //    result.Properties.Contains("mail") &&
                        //    result.Properties.Contains("displayname") &&
                        //    result.Properties.Contains("department") )
                           
                        //{
                        //    UserDomain.DTO.UserDomain objSurveyUsers = new UserDomain.DTO.UserDomain();
                        //    objSurveyUsers.Email = (String)result.Properties["mail"][0];
                        //    objSurveyUsers.Name = (String)result.Properties["samaccountname"][0];
                        //    objSurveyUsers.Department = (string)result.Properties["department"][0];
                        //    ListDeactivatedUserDomain.Add(objSurveyUsers);

                        //}
                        
                    }

                    //var users = HRMS.Users.Where(x=>x.IsOutDomainUser!=true).Select(x => new { x.LDAPUserName, x.UserID });

                    //foreach (var item in users)
                    //{
                    //    if (ListNewUserDomain.Where(x => x.Name == item.LDAPUserName).Count() < 1)
                    //    {
                    //        User DeletedUser = HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName&&x.IsOutDomainUser!=true)?.FirstOrDefault();
                    //        if (DeletedUser != null)
                    //        {
                    //            DeletedUser.IsDeleted = true;
                    //            DeletedUser.IsActive = false;
                    //        }
                    //    }

                    //}

                    //foreach (var item in ListDeactivatedUserDomain)
                    //{
                    //    if (users.Where(x => x.LDAPUserName == item.Name).Count() == 1)
                    //    {
                    //        User DeletedUser = HRMS.Users.Where(x => x.LDAPUserName == item.Name)?.FirstOrDefault();
                    //        if (DeletedUser != null)
                    //        {
                    //            DeletedUser.IsDeleted = false;
                    //            DeletedUser.IsActive = false;
                    //        }
                    //    }

                    //}
                    


                    //HRMS.SaveChanges();




                }
                catch (Exception ex)
                {

                }



            }
            return ListNewUserDomain;

        }
        public bool Disable(SearchResult userDn)
        {
            const int UF_ACCOUNTDISABLE = 0x0002;

            int flags = (int)userDn.Properties["userAccountControl"][0];

            if (Convert.ToBoolean(flags & UF_ACCOUNTDISABLE))
            {
                return false;
               
            }
            return true;
        }
        public void InsertUsersInHRMS(List<UserDomain.DTO.UserDomain> DomainUsers)
        {
            List<User> UserHRMS = new List<User>();
            foreach (var item in DomainUsers)
            {
                UserHRMS.Add(new User
                {
                     LDAPUserName= item.Name,
                     UserEmail=item.Email,
                     ExtNumber = item.ExtensionNumber


                });

            }
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                foreach (var item in UserHRMS)
                {
                    if (HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName).Count() < 1)
                    {
                        item.UserName = item.LDAPUserName.Replace(".", " ");
                        item.CreationDate = DateTime.Now;
                        HRMS.Users.Add(item);
                        HRMS.SaveChanges();
                        item.ProfilePictureURL = item.UserID.ToString() + ".jpg";
                        item.IsActive = null;
                        item.IsDeleted = false;


                    }
                }
                HRMS.SaveChanges();

                foreach (var item in UserHRMS)
                {
                    var y = HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName).Count();
                    var z = item.ExtNumber != "";
                    if (HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName).Count() == 1 && item.ExtNumber != null)
                    {

                        var user = HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName).FirstOrDefault();
                        user.ExtNumber = item.ExtNumber;
                        HRMS.SaveChanges();



                    }
                    else if (HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName).Count() == 1 && item.ExtNumber == null)
                    {

                        var user = HRMS.Users.Where(x => x.LDAPUserName == item.LDAPUserName).FirstOrDefault();
                        user.ExtNumber = null;
                        HRMS.SaveChanges();

                    }



                }
            }

        }

       
    }
}
