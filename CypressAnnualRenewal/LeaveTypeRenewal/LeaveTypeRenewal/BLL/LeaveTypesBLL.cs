using LeaveTypeRenewal.Enum;
using LeaveTypeRenewal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeaveTypeRenewal.BLL.StaticEnums;

namespace LeaveTypeRenewal.BLL
{
   public class LeaveTypesBLL
    {
        HRMSEntities Context;
        DateTime CreationDate;
        public LeaveTypesBLL()
        {
            Context = new HRMSEntities();
            CreationDate = DateTime.Now;
        }
        ~LeaveTypesBLL()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }
        public bool CheckPendingLeves()
        {

            var listOfLeaveTypies = Context.LeaveTypeCarryOverRules.Where(x => x.CarryOverUnusedEntitlements == true);
            List<LeaveTypeAccuralRule> accuralRuleLeaveTypes = new List<LeaveTypeAccuralRule>();
            foreach (var item in listOfLeaveTypies)
            {
                accuralRuleLeaveTypes.AddRange(Context.LeaveTypeAccuralRules.Where(x =>
             x.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.FirstDayOfTheYear &&
             x.EmployeeEarnsNumberOfUnits != null && x.IsActive == true && x.LeaveTypeFK == item.LeaveTypeFK).ToList());
            }
            var listOfPedndingRequests = Context.Requests.Where(x => x.RequesStatusFK == (int)Status.Pending).ToList();
            foreach (var item in listOfPedndingRequests) {
                if (accuralRuleLeaveTypes.Where(x=>x.LeaveTypeFK.Value==item.LeaveTypeFK.Value).Count()> 0&& item.RequesStatusFK==(int)Status.Pending)
                {
                    return false;
                }
               
            }
            return true ;

        }
        private bool CheckRestrictions(int LeaveTypeID, User User)
        {
            var restrictedGender = Context.LeaveTypeRestrictedGenders.Where(x => x.LeaveTypeFK == LeaveTypeID && x.GenderFK == User.GenderFK && x.IsActive == true)?.FirstOrDefault();
            var restrictedEmployee = Context.LeaveTypeRestrictedEmployees.Where(x => x.LeaveTypeFK == LeaveTypeID && x.EmployeeFK == User.UserID && x.IsActive == true)?.FirstOrDefault();
            var restrictedDep = Context.LeaveTypeRestrictedDeps.Where(x => x.LeaveTypeFK == LeaveTypeID && x.DepartmentFK == User.DepartmentFK && x.IsActive == true)?.FirstOrDefault();
            var restrictedSubDep = Context.LeaveTypeRestrictedSubDeps.Where(x => x.LeaveTypeFK == LeaveTypeID && x.SubDepartmentFK == User.SubDepartmentFK && x.IsActive == true)?.FirstOrDefault();
            var restrictedContractType = Context.LeaveTypeRestrictedContractTypes.Where(x => x.LeaveTypeFK == LeaveTypeID && x.ContractTypeFK == User.ContractTypeFK && x.IsActive == true)?.FirstOrDefault();

            if (restrictedGender != null || restrictedEmployee != null || restrictedDep != null || restrictedSubDep != null || restrictedContractType != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private UserEntitlement AddNewEntitlementAndEntitlementChange(int UserID, int LeaveTypeID, decimal Amount, decimal NewAmount, decimal OldAmount, string Comment,DateTime NewYrarTimePicker)
        {
            UserEntitlement newUserEntitlement = new UserEntitlement();

            newUserEntitlement = new UserEntitlement()
            {
                LeaveTypeFK = LeaveTypeID,
                EntitlementTotal = Amount,//leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().TakeMaxAmountFromParent.Value,
                LeaveDurationUnitFK = Context.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().DurationUnitFK.Value,
                IsActive = true,
                IsDeleted = false,
                Year = NewYrarTimePicker.Year,
                CreationDate = DateTime.Now,
                UserFK = UserID
            };

            Context.UserEntitlements.Add(newUserEntitlement);

            UserEntitlementChange newUserEntitlementChange = new UserEntitlementChange()
            {
                UserEntitlement = newUserEntitlement,
                Comment = Comment,
                CreationDate = DateTime.Now,
                LeaveDurationUnitFK = newUserEntitlement.LeaveDurationUnitFK,
                IsActive = true,
                EntitlementBefore = OldAmount,
                EntitlementAfter = NewAmount,
                EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                ActionDate = DateTime.Now,
                IsDeleted = false,
                UserFK = UserID,
                Year = NewYrarTimePicker.Year,
                 
      
            };

            Context.UserEntitlementChanges.Add(newUserEntitlementChange);
            return newUserEntitlement;
            //try
            //{
            //    Context.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    WriteToFile("Entitlement Error " + ex.Message);

            //}
        }
        private void AddNewEntitlementChange(UserEntitlement NewUserEntitlementID, int UserID, int LeaveTypeID, decimal OldAmount, decimal NewAmount, string Comment,DateTime NewYrarTimePicker)
        {
            UserEntitlementChange newUserEntitlementChange = new UserEntitlementChange()
            {
                 UserEntitlement = NewUserEntitlementID,
                Comment = Comment,
                CreationDate = DateTime.Now,
                LeaveDurationUnitFK = Context.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().DurationUnitFK.Value,
                IsActive = true,
                EntitlementBefore = OldAmount,
                EntitlementAfter = NewAmount,
                EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                ActionDate = DateTime.Now,
                IsDeleted = false,
                UserFK = UserID,
                Year = NewYrarTimePicker.Year
            };

            Context.UserEntitlementChanges.Add(newUserEntitlementChange);

            //try
            //{
            //    Context.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    WriteToFile("Entitlement Error " + ex.Message);

            //}
        }
        private decimal CalculateSeniorityYears(User user)
        {
            decimal seniorityYears = 0;
            if ((user.SeniorityBeforeHireYears != null && user.SeniorityBeforeHireYears.ToString() != " " && user.SeniorityBeforeHireYears.ToString() != string.Empty) &&
                 (user.SeniorityBeforeHireMonth != null && user.SeniorityBeforeHireMonth.ToString() != " ") && user.SeniorityBeforeHireMonth.ToString() != string.Empty)
            {
                //var seniorityMonths = ((DateTime.Today.Year - usersItems.SeniorityBeforeHireYears.Value) * 12) + (DateTime.Today.Month - usersItems.SeniorityBeforeHireMonth.Value);

                seniorityYears = user.SeniorityBeforeHireYears.Value + ((decimal)user.SeniorityBeforeHireMonth.Value / 12);
            }

            DateTime lastDayyear = new DateTime(DateTime.Today.Year - 1, 12, 31);
            //int workingDaysFromHire = (lastDayyear - user.HireDate.Value).Days;
            //Calculate seniority years after hiring
            seniorityYears += ((decimal)(((DateTime.Today.Date.AddDays(-1)) - user.HireDate.Value.Date).Days) / (DateTime.IsLeapYear(DateTime.Today.Year - 1) ? 366 : 365));

            return seniorityYears;
        }
        public HRMSEntities AddForLeaveTypesHasEntitlementsForNewYear(bool CarryOverLastYearEntitlement, bool SaveChanges,DateTime NewYrarTimePicker)
        {
            List<User> users = Context.Users.Where(x => x.IsActive == true).ToList();
            var listOfLeaveTypies = Context.LeaveTypeCarryOverRules.Where(x => x.CarryOverUnusedEntitlements == true);
            List<LeaveTypeAccuralRule> accuralRuleLeaveTypes = new List<LeaveTypeAccuralRule>();
            foreach (var item in listOfLeaveTypies)
            {
                accuralRuleLeaveTypes.AddRange( Context.LeaveTypeAccuralRules.Where(x =>
              x.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.FirstDayOfTheYear &&
              x.EmployeeEarnsNumberOfUnits != null && x.IsActive == true&&x.LeaveTypeFK== item.LeaveTypeFK).ToList());
            }
            foreach (var usersitems in users)
            {
                decimal seniorityYears = CalculateSeniorityYears(usersitems);
                foreach (var accuralitems in accuralRuleLeaveTypes)
                {
                    decimal finalAmount = 0;
                    decimal NewAmount = 0;

                    bool checkResrictions = CheckRestrictions(accuralitems.LeaveTypeFK.Value, usersitems);

                    if (checkResrictions)
                    {
                        UserEntitlement userEntitlements = Context.UserEntitlements.Where(x => x.LeaveTypeFK == accuralitems.LeaveTypeFK && x.UserFK == usersitems.UserID && x.IsActive == true).FirstOrDefault();

                        if (userEntitlements != null)
                        {
                            userEntitlements.IsActive = false;
                            Context.UserEntitlements.AddOrUpdate(userEntitlements);

                            if (seniorityYears >= accuralitems.OverSeniorityYears)
                            {
                                finalAmount = accuralitems.OverSeniorityEmployeeEarns.Value;
                                NewAmount = accuralitems.OverSeniorityEmployeeEarns.Value;
                            }
                            else
                            {
                                finalAmount = accuralitems.EmployeeEarnsNumberOfUnits.Value;
                                NewAmount = accuralitems.EmployeeEarnsNumberOfUnits.Value;

                            }
                            var LeaveType = Context.LeaveTypes.Where(x => x.LeaveTypeID == accuralitems.LeaveTypeFK).FirstOrDefault();

                            if (CarryOverLastYearEntitlement)
                            {
                                finalAmount += userEntitlements.EntitlementTotal;
                               var NewEntitlment= AddNewEntitlementAndEntitlementChange(usersitems.UserID, accuralitems.LeaveTypeFK.Value, finalAmount, NewAmount, 0, LeaveType.LeaveTypeName+ " Yearly Renewal", NewYrarTimePicker);

                               // userEntitlements = Context.UserEntitlements.Where(x => x.LeaveTypeFK == accuralitems.LeaveTypeFK && x.IsActive == true).FirstOrDefault();

                                AddNewEntitlementChange(NewEntitlment, usersitems.UserID, accuralitems.LeaveTypeFK.Value, NewAmount, finalAmount, "Carry Over Last Year "+ LeaveType.LeaveTypeName + " Entitlement", NewYrarTimePicker);
                            }
                            else
                            {
                                AddNewEntitlementAndEntitlementChange(usersitems.UserID, accuralitems.LeaveTypeFK.Value, finalAmount, NewAmount, 0, LeaveType.LeaveTypeName + " Yearly Renewal", NewYrarTimePicker);

                            }
                        }
                        else
                            continue;
                    }
                }
                var LeavesTypes = Context.LeaveTypes.Where(x => x.ParentLeaveTypeFK != null && x.TakeMaxAmountFromParent != null).ToList();
                foreach (var LeavesTypeitem in LeavesTypes)
                {
                    if (CheckRestrictions(LeavesTypeitem.LeaveTypeID, usersitems))
                    {
                        AddForChildLeavesForNewYear(LeavesTypeitem, usersitems, CarryOverLastYearEntitlement, NewYrarTimePicker);

                    }
                }
            }
            if (SaveChanges) {
                try
                {
                    Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    WriteToFile("Entitlement Error " + ex.Message);

                }
                return null;

            }
            else
            {
                return Context;

            }
        } 
        public void AddForChildLeavesForNewYear(LeaveType LeavesType, User User,bool CarryOverLastYearEntitlement,DateTime NewYrarTimePicker) {

           List< UserEntitlement> userEntitlements = Context.UserEntitlements.Where(x => x.LeaveTypeFK == LeavesType.LeaveTypeID 
            && x.IsActive == true&&x.UserFK== User.UserID).ToList();
            decimal finalAmount = (decimal) LeavesType.TakeMaxAmountFromParent;
            if (userEntitlements.FirstOrDefault() != null)
            {
                foreach(var item in userEntitlements)
                {
                    item.IsActive = false;
                }
                if (CarryOverLastYearEntitlement)
                {
                   // finalAmount += userEntitlements.EntitlementTotal;
                    AddNewEntitlementAndEntitlementChange(User.UserID, LeavesType.LeaveTypeID, finalAmount, finalAmount, 0, LeavesType.LeaveTypeName+" Yearly Renewal", NewYrarTimePicker);
                   // userEntitlements = Context.UserEntitlements.Where(x => x.LeaveTypeFK == LeavesType.LeaveTypeID && x.IsActive == true).FirstOrDefault();
                    //AddNewEntitlementChange(userEntitlements.UserEntitlementID, User.UserID, LeavesType.LeaveTypeID, userEntitlements.EntitlementTotal, userEntitlements.EntitlementTotal, "Carry Over Last Year Entitlement");
                }
                else
                {
                    AddNewEntitlementAndEntitlementChange(User.UserID, LeavesType.LeaveTypeID, finalAmount, finalAmount, 0, LeavesType.LeaveTypeName + " Yearly Renewal", NewYrarTimePicker);

                }
            }
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog " + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteToFile("Entitlement Error " + ex.Message);

            }


        }
    }
}
