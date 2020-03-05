using LeaveTypeRenewal.DTO;
using LeaveTypeRenewal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeaveTypeRenewal.BLL.StaticEnums;

namespace LeaveTypeRenewal.BLL
{
  public  class ExportAnnualYearEntitlmentBLL
    {

        public List<UserDTO> GetActiveUsers(int EffictiveYear, int Fillter)
        {
            List<UserDTO> UsersList = new List<UserDTO>();
            using (HRMSEntities HRMS =new HRMSEntities())
            {
                List<User> Users = new List<User>();
                if (Fillter == 3)
                {
                    Users = HRMS.Users.Where(x => x.IsActive == true && x.IsDeleted == false && x.AccessControlUserFK != null).ToList();
                }
                else if (Fillter == 1)
                {
                    Users = HRMS.Users.Where(x => x.IsActive == true && x.IsDeleted == false && x.AccessControlUserFK != null && x.CompanyFK == (int)Company.MedGulf).ToList();
                }
                else if (Fillter==2)
                {
                    Users = HRMS.Users.Where(x => x.IsActive == true && x.IsDeleted == false && x.AccessControlUserFK != null && x.CompanyFK == (int)Company.PrimeHealth).ToList();
                }


                foreach (var item in Users)
                {
                    List<int> UserEntitlementIDS = HRMS.UserEntitlements.Where(x => x.UserFK == item.UserID && x.Year == EffictiveYear
                    &&(x.LeaveTypeFK== (int)LeaveTypes.Annual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Casual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Sick
                    || 
                    x.LeaveTypeFK == (int)LeaveTypes.Part_TimeAnnual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Part_TimeCasual
                    || 
                    x.LeaveTypeFK == (int)LeaveTypes.Part_TimeSick
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Maternity
                    )
                    ).Select(x => x.UserEntitlementID).ToList();

                    string CompanyName = "";
                    if (item.CompanyFK == (int)Company.MedGulf)
                    {
                        CompanyName = "شركة المتوسط والخليج للتأمين";
                    }
                    else if (item.CompanyFK == (int)Company.PrimeHealth)
                    {
                        CompanyName = "الاولى للخدمات الطبيه";

                    }

                    UsersList.Add(new UserDTO
                    {  BirthDate= item.BirthDate.Value.ToShortDateString(),
                       CypressID=(int) item.UserID,
                       UserName= item.ArName,
                       HireDate= item.HireDate.Value.ToShortDateString(),
                       UserEntitlementIDS= UserEntitlementIDS,
                       AccID= (int)item.AccessControlUserFK,
                        CompanyName= CompanyName
                    });
                }
           }
            return UsersList;
        }


        public List<UserDTO> GetActiveUserByID(int EffictiveYear,int AccUserID)
        {
            List<UserDTO> UsersList = new List<UserDTO>();
            using (HRMSEntities HRMS = new HRMSEntities())
            {

                foreach (var item in HRMS.Users.Where(x => x.IsActive == true && x.IsDeleted == false && x.AccessControlUserFK != null && x.AccessControlUserFK == AccUserID))
                {
                    List<int> UserEntitlementIDS = HRMS.UserEntitlements.Where(x => x.UserFK == item.UserID && x.Year == EffictiveYear
                    && (x.LeaveTypeFK == (int)LeaveTypes.Annual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Casual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Sick
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Part_TimeAnnual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Part_TimeCasual
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Part_TimeSick
                    ||
                    x.LeaveTypeFK == (int)LeaveTypes.Maternity
                    )
                    ).Select(x => x.UserEntitlementID).ToList();
                    string CompanyName = "";
                    if (item.CompanyFK ==(int) Company.MedGulf)
                    {
                        CompanyName = "شركة المتوسط والخليج للتأمين";
                    }
                    else if (item.CompanyFK == (int)Company.PrimeHealth)
                    {
                        CompanyName = "الاولى للخدمات الطبيه";

                    }
                    UsersList.Add(new UserDTO
                    {
                        BirthDate = item.BirthDate.Value.ToShortDateString(),
                        CypressID = (int)item.UserID,
                        UserName = item.ArName,
                        HireDate = item.HireDate.Value.ToShortDateString(),
                        UserEntitlementIDS = UserEntitlementIDS,
                        AccID = (int)item.AccessControlUserFK,
                        CompanyName= CompanyName
                    });
                }
            }
            return UsersList;
        }


        public LogsAndEntitlmentsContainer GetLeavesLogs(int UserID,List<int> UserEntitlementID,int EffictiveYear)
        {
            List<LeavesLogsDTO> UsersLeavesLogs = new List<LeavesLogsDTO>();
            StartEntitlmentDTO StartEntitlment = new StartEntitlmentDTO();
            StartEntitlment.LeaveEntitlmentOfStartYear = new List<EntitlmentDTO>();
            EndEntitlmentDTO EndEntitlment = new EndEntitlmentDTO();
            EndEntitlment.LeaveEntitlmentOfEndYear = new List<EntitlmentDTO>();

            using (HRMSEntities HRMS = new HRMSEntities())
            {
                var LogsList = HRMS.UserEntitlementChanges.Where(x => x.UserFK == UserID && x.IsActive == true
                  && x.IsDeleted == false && UserEntitlementID.Contains(x.UserEntitlementFK.Value)).GroupBy(test => test.CreationDate).ToList().Select(grp => grp.LastOrDefault()).ToList();

                for (int i=0; i< LogsList.Count();i++ )
                {

                    int LeaveTypeFK = LogsList[i].UserEntitlement.LeaveTypeFK;

                    string LeaveTypeName = "";
                    if (HRMS.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeFK).FirstOrDefault().LeaveTypeName== "Annual")
                    {
                        LeaveTypeName = "اعتيادي";
                    }
                    else if (HRMS.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeFK).FirstOrDefault().LeaveTypeName=="Casual")
                    {
                        LeaveTypeName = "عارضة";

                    }
                    else if (HRMS.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeFK).FirstOrDefault().LeaveTypeName == "Maternity")
                    {
                        LeaveTypeName = "وضع";

                    }

                    string Period = "";
                    decimal PeriodValue;
                   
                    if (LogsList[i].RequestDuration == 0)
                    {
                        if(LogsList[i].EntitlementAfter> LogsList[i].EntitlementBefore)
                        {
                            Period = "+"+(LogsList[i].EntitlementAfter- LogsList[i].EntitlementBefore).ToString();
                            PeriodValue = LogsList[i].EntitlementAfter - LogsList[i].EntitlementBefore;
                        }
                        else
                        {
                            Period = "-"+(LogsList[i].EntitlementBefore- LogsList[i].EntitlementAfter).ToString();
                            PeriodValue = LogsList[i].EntitlementBefore - LogsList[i].EntitlementAfter;

                        }


                    }
                    //else if (LogsList[i].RequestDuration>(LogsList[i].EntitlementBefore-LogsList[i].EntitlementAfter)
                    //    && LogsList[i].EntitlementBefore >LogsList[i].EntitlementAfter)
                    //{
                    //    Period = LogsList[i].RequestDuration + "("+ (LogsList[i].EntitlementAfter - LogsList[i].EntitlementBefore) + ")".ToString();
                    //    PeriodValue = LogsList[i].EntitlementAfter - LogsList[i].EntitlementBefore;

                    //}

                    else
                    {
                         Period = LogsList[i].RequestDuration.ToString();
                        PeriodValue = LogsList[i].RequestDuration;
                    }
                    UsersLeavesLogs.Add(new LeavesLogsDTO
                    {
                        Date = LogsList[i].CreationDate.Value.Date,
                        LeaveTypeName = LeaveTypeName,
                        Note = LogsList[i].Comment,
                        Period = Period,
                         PeriodValue= PeriodValue,
                        LeaveTypeID = LeaveTypeFK
                    });
                }
                if (UsersLeavesLogs.FirstOrDefault().PeriodValue == (decimal)0)
                {
                    UsersLeavesLogs.Remove(UsersLeavesLogs.FirstOrDefault());
                }

                var LastYearEntitlment=  HRMS.UserEntitlements.Where(x => UserEntitlementID.Contains(x.UserEntitlementID)&&x.Year== EffictiveYear).ToList();
                foreach (var item in LastYearEntitlment)
                {
                    string LeaveTypeName = HRMS.LeaveTypes.Where(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                    if (HRMS.LeaveTypes.Where(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName == "Annual")
                    {
                        LeaveTypeName = "اعتيادي";
                    }
                    else if (HRMS.LeaveTypes.Where(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName == "Casual")
                    {
                        LeaveTypeName = "عارضة";

                    }
                    

                    if (item.LeaveTypeFK == (int)LeaveTypes.Casual)
                    {
                        EndEntitlment.LeaveEntitlmentOfEndYear.Add(new EntitlmentDTO
                        {
                            Entitlment = item.EntitlementTotal,
                            LeaveTypeId = item.LeaveTypeFK,
                            LeaveTypeName = LeaveTypeName
                        });
                    }
                    else if(item.LeaveTypeFK == (int)LeaveTypes.Annual)
                    {
                        var Casual = LastYearEntitlment.Where(x => x.LeaveTypeFK == (int)LeaveTypes.Casual).FirstOrDefault().EntitlementTotal;
                         if (Casual > item.EntitlementTotal)
                        {
                            EndEntitlment.LeaveEntitlmentOfEndYear.Add(new EntitlmentDTO
                            {
                                Entitlment = item.EntitlementTotal ,
                                LeaveTypeId = item.LeaveTypeFK,
                                LeaveTypeName = LeaveTypeName
                            });
                        }
                        else
                        {
                            EndEntitlment.LeaveEntitlmentOfEndYear.Add(new EntitlmentDTO
                            {
                                Entitlment = item.EntitlementTotal - Casual,
                                LeaveTypeId = item.LeaveTypeFK,
                                LeaveTypeName = LeaveTypeName
                            });

                        }
                    }
                }


                var StartEntitlCasual = UsersLeavesLogs.Where(x => x.LeaveTypeID == (int)LeaveTypes.Casual).FirstOrDefault();

                StartEntitlment.LeaveEntitlmentOfStartYear.Add(new EntitlmentDTO
                {
                    Entitlment = StartEntitlCasual.PeriodValue,
                    LeaveTypeName = StartEntitlCasual.LeaveTypeName

                });

                var StartEntitlAnnual= UsersLeavesLogs.Where(x => x.LeaveTypeID ==(int) LeaveTypes.Annual&&x.PeriodValue>0).FirstOrDefault();


                StartEntitlment.LeaveEntitlmentOfStartYear.Add(new EntitlmentDTO
                {
                    Entitlment = StartEntitlAnnual.PeriodValue - StartEntitlCasual.PeriodValue,
                    LeaveTypeName = StartEntitlAnnual.LeaveTypeName

                });

                StartEntitlment.LeaveEntitlmentOfStartYear.Reverse();
               var ListOfUnChangingYears= HRMS.Requests.Where(x =>x.UserFK== UserID&&
                (x.LeaveTypeFK == (int)LeaveTypes.Deduction || x.LeaveTypeFK == (int)LeaveTypes.DeductedSick) && x.CreationDate.Value.Year == EffictiveYear).ToList();
             foreach(var item in ListOfUnChangingYears)
                {
                    string LeaveTypeName = "";
                    if((int)item.LeaveTypeFK== (int)LeaveTypes.DeductedSick)
                    {
                        LeaveTypeName = "مرضي بالخصم";

                    }
                    else if ((int)item.LeaveTypeFK ==(int) LeaveTypes.Deduction)
                    {
                        LeaveTypeName = "اجازة بالخصم";


                    }
                    UsersLeavesLogs.Add(new LeavesLogsDTO
                    {
                        LeaveTypeName = LeaveTypeName,
                        Date =(DateTime) item.RequestHandlers.FirstOrDefault().Offday,
                        Period = "("+item.RequestHandlers.Count().ToString()+")",
                        LeaveTypeID = (int)item.LeaveTypeFK,
                        Note = "بالخصم من الراتب",
                        PeriodValue = item.RequestHandlers.Count()
                    });
                }

            }
            UsersLeavesLogs= UsersLeavesLogs.OrderBy(x => x.Date).ToList();
            
            return new LogsAndEntitlmentsContainer
            { EndEntitlment= EndEntitlment,
              StartEntitlmentDTO= StartEntitlment,
              LeavesLogs= UsersLeavesLogs
            };
        }

    }       
}
