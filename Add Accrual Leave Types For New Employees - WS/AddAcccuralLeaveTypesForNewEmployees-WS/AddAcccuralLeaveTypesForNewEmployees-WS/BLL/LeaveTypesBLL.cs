using AddAcccuralLeaveTypesForNewEmployees_WS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AddAcccuralLeaveTypesForNewEmployees_WS.BLL.Enums.StaticEnums;

namespace AddAcccuralLeaveTypesForNewEmployees_WS.BLL
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

        public async Task ScheduleOfExecutionAsync()
        {
            List<TimeSpan> ListOfTimes = new List<TimeSpan>();
            var timeValue = Context.Configurations.Where(x => x.ConfigurationKey == "AccuralLeaveTypeTime" && x.IsActive == true).Select(x => x.ConfigurationValue).FirstOrDefault().ToString();

            TimeSpan executionTime = TimeSpan.Parse(timeValue);
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = executionTime - current.TimeOfDay;
            if (timeToGo >= TimeSpan.Zero)
            {
                WriteToFile("Set Delay time"+ timeToGo + "On" + DateTime.Now);
                await Task.Delay(timeToGo);
                AddAccuralLeaveTypesForNewEmployeesAsync();
                AddAccuralLeaveTypesForEmployeesAsync();
            }
            TimeSpan EndDayTime = new TimeSpan(24, 30, 00);
            DateTime CurrentDayTime = DateTime.Now;
            TimeSpan TimeToNextDay = EndDayTime - CurrentDayTime.TimeOfDay;
            await Task.Delay(TimeToNextDay);
            await ScheduleOfExecutionAsync();
        }
        public decimal RoundUP(double d)
        {
            var absoluteValue = Math.Abs(d);
            var integralPart = (int)absoluteValue;
            var decimalPart = absoluteValue - integralPart;

            double roundedNumber;

            if (decimalPart > 0.5)
            {
                roundedNumber = integralPart + 1;
            }
            else if (decimalPart == 0)
            {
                roundedNumber = absoluteValue;
            }
            else
            {
                roundedNumber = integralPart;
            }

            return (decimal)roundedNumber;
        }
        public decimal RoundDown(double d)
        {
            var absoluteValue = Math.Abs(d);
            var integralPart = (int)absoluteValue;
            var decimalPart = absoluteValue - integralPart;

            double roundedNumber;

            if (decimalPart > 0.5)
            {
                roundedNumber = integralPart + 0.5;
            }
            else if (decimalPart == 0)
            {
                roundedNumber = absoluteValue;
            }
            else
            {
                roundedNumber = integralPart;
            }

            return (decimal)roundedNumber;
        }
        public bool CheckUserHaveEntitlementForLeaveType(int UserID, int LeaveTypeID)
        {
            var userEntitlement = Context.UserEntitlements.Where(x => x.UserFK == UserID && x.LeaveTypeFK == LeaveTypeID).ToList();

            if (userEntitlement.Count() == 0)
                return true;
            else
                return false;
        }
        public bool CheckRestrictions(int LeaveTypeID, User User)
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
        public decimal CalculateEntitlementQuantity(decimal TotalEarnsUnit, int WorkingDaysFromHire, int ProrteMode, int ProrteMethod, int Year)
        {
            decimal amount = 0;

            if (ProrteMode == (int)LeaveTypeProrateCalculationMode.BasedOnDays)
                amount = TotalEarnsUnit / (DateTime.IsLeapYear(Year) ? 366 : 365);
            else if (ProrteMode == (int)LeaveTypeProrateCalculationMode.BasedOnFullMonths)
                amount = TotalEarnsUnit / 12;

            decimal finalAmount = ((decimal)WorkingDaysFromHire / 30) * amount;

            if (ProrteMethod == (int)LeaveTypeProrateMethod.RoundAlwaysUp)
                return RoundUP((double)finalAmount);
            else if(ProrteMethod == (int)LeaveTypeProrateMethod.RoundToWhole)
                return RoundDown((double)finalAmount);

            return finalAmount;
        } 
        public void AddOrEditEntitlementAndEntitlementChange(int UserID, int LeaveTypeID, decimal Amount, string Comment)
        {
            UserEntitlement newUserEntitlement = new UserEntitlement();

            if (!Comment.Equals("Monthly Entitlement"))
            {
                newUserEntitlement = Context.UserEntitlements.Where(x => x.UserFK == UserID && x.LeaveTypeFK == LeaveTypeID && x.IsActive == true).FirstOrDefault();
                if(newUserEntitlement != null)
                {
                    newUserEntitlement.EntitlementTotal = Amount;
                    newUserEntitlement.ModificationDate = DateTime.Now;
                }
                else
                {
                    newUserEntitlement.LeaveTypeFK = LeaveTypeID;
                    newUserEntitlement.EntitlementTotal = Amount;//leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().TakeMaxAmountFromParent.Value,
                    newUserEntitlement.LeaveDurationUnitFK = Context.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().DurationUnitFK.Value;
                    newUserEntitlement.IsActive = true;
                    newUserEntitlement.IsDeleted = false;
                    newUserEntitlement.Year = DateTime.Today.Year;
                    newUserEntitlement.CreationDate = DateTime.Now;
                    newUserEntitlement.UserFK = UserID;

                    Context.UserEntitlements.Add(newUserEntitlement);
                }
                
            }

            else if (Comment.Equals("Monthly Entitlement"))
            { 
                newUserEntitlement = Context.UserEntitlements.Where(x => x.LeaveTypeFK == LeaveTypeID &&
                    x.UserFK == UserID && x.IsActive == true).FirstOrDefault();
                if (newUserEntitlement != null)
                {
                    newUserEntitlement.EntitlementTotal = Amount;
                    newUserEntitlement.ModificationDate = DateTime.Now;   
                }
                //Context.UserEntitlements.Edit(newUserEntitlement);
                else
                {
                    newUserEntitlement.LeaveTypeFK = LeaveTypeID;
                    newUserEntitlement.EntitlementTotal = Amount;//leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().TakeMaxAmountFromParent.Value,
                    newUserEntitlement.LeaveDurationUnitFK = Context.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().DurationUnitFK.Value;
                    newUserEntitlement.IsActive = true;
                    newUserEntitlement.IsDeleted = false;
                    newUserEntitlement.Year = DateTime.Today.Year;
                    newUserEntitlement.CreationDate = DateTime.Now;
                    newUserEntitlement.UserFK = UserID;

                    Context.UserEntitlements.Add(newUserEntitlement);
                }
            }
            if (newUserEntitlement != null)
            {
                UserEntitlementChange newUserEntitlementChange = new UserEntitlementChange()
                {
                    UserEntitlement = newUserEntitlement,
                    Comment = Comment,
                    CreationDate = DateTime.Today,
                    LeaveDurationUnitFK = newUserEntitlement.LeaveDurationUnitFK,
                    IsActive = true,
                    EntitlementBefore = 0,
                    EntitlementAfter = newUserEntitlement.EntitlementTotal,
                    EntitlementChangedByFK = (int)EntitlementChangedBy.System,
                    ActionDate = DateTime.Now,
                    IsDeleted = false,
                    UserFK = UserID,
                    Year = DateTime.Now.Year
                };

                Context.UserEntitlementChanges.Add(newUserEntitlementChange);
                Context.SaveChanges();
            }
        }
        public void AddAccuralLeaveTypesForNewEmployeesAsync()
        {
            WriteToFile("Start Add Accural Leave Types For New Employees on " + DateTime.Now);
            string comment = "";
            List<User> users = Context.Users.Where(x => x.IsActive == true).ToList();

            List<int> accuralLeaveTypes = Context.LeaveTypes.Where(x => x.ParentLeaveTypeFK == null && x.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Accured && x.IsSuspended == false && x.IsActive == true).Select(x => x.LeaveTypeID).ToList();
            List<LeaveTypeAccuralRule> accuralRuleLeaveTypes = Context.LeaveTypeAccuralRules.Where(x => accuralLeaveTypes.Contains(x.LeaveTypeFK.Value) && x.GainingEligibilityMethodFK == (int)LeaveTypeGainingEligibilityMethod.AfterHire && x.IsActive == true).ToList();

            foreach (var usersItems in users)
            {
                decimal seniorityYears = 0;
                foreach (var accuralItems in accuralRuleLeaveTypes)
                {
                    string leaveTypeName = Context.LeaveTypes.Where(x => x.LeaveTypeID == accuralItems.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                    var EligiableDateForAccuredLeaves = usersItems.HireDate.Value.AddDays(1).AddMonths(accuralItems.AfterHireEligibilityMonths.Value);
                    if (EligiableDateForAccuredLeaves.Date == DateTime.Today.Date && CheckUserHaveEntitlementForLeaveType(usersItems.UserID, accuralItems.LeaveTypeFK.Value))
                    {
                        if (accuralItems.GainingEligibilityMethodFK == (int)LeaveTypeGainingEligibilityMethod.AfterHire && accuralItems.AfterHireEligibilityMonths != null)
                        {
                            //DateTime HiringDateAfterAddingMonths = usersItems.HireDate.Value.AddMonths(AccuralItems.AfterHireEligibilityMonths.Value);
                            //To check if the experience of employee exceed 10 years 
                            if((usersItems.SeniorityBeforeHireYears != null || usersItems.SeniorityBeforeHireYears.ToString() != " " || usersItems.SeniorityBeforeHireYears.ToString() != string.Empty) && 
                                (usersItems.SeniorityBeforeHireMonth != null || usersItems.SeniorityBeforeHireMonth.ToString() != " ") || usersItems.SeniorityBeforeHireMonth.ToString() != string.Empty)
                            {
                                //var seniorityMonths = ((DateTime.Today.Year - usersItems.SeniorityBeforeHireYears.Value) * 12) + (DateTime.Today.Month - usersItems.SeniorityBeforeHireMonth.Value);

                                seniorityYears = usersItems.SeniorityBeforeHireYears.Value + ((decimal)usersItems.SeniorityBeforeHireMonth.Value / 12);
                            }

                            //Calculate days from hire
                            DateTime lastDayyear = new DateTime(DateTime.Today.Year, 12, 31);
                            int workingDaysFromHire = (lastDayyear - usersItems.HireDate.Value).Days;
                            //Calculate seniority years after hiring
                            seniorityYears += ((decimal)((DateTime.Today.Date - usersItems.HireDate.Value.Date).Days) / (DateTime.IsLeapYear(DateTime.Today.Year) ? 366 : 365));
                            decimal finalAmount = 0;

                            //if (HiringDateAfterAddingMonths.Date == DateTime.Today.Date)
                            //{
                            bool check = CheckRestrictions(accuralItems.LeaveTypeFK.Value, usersItems);
                            if (check)
                            {
                                if (accuralItems.FirstAccuralMethodFK == (int)LeaveTypeFirstAccuralMethod.ProrateAmount)
                                {
                                    if (seniorityYears >= accuralItems.OverSeniorityYears)
                                    {
                                        finalAmount = CalculateEntitlementQuantity(accuralItems.OverSeniorityEmployeeEarns.Value, workingDaysFromHire, accuralItems.ProrateCalculationModeFK.Value, accuralItems.ProrateMethodFK.Value, usersItems.HireDate.Value.Year);
                                    }
                                    else
                                    {
                                        finalAmount = CalculateEntitlementQuantity(accuralItems.EmployeeEarnsNumberOfUnits.Value, workingDaysFromHire, accuralItems.ProrateCalculationModeFK.Value, accuralItems.ProrateMethodFK.Value, usersItems.HireDate.Value.Year);
                                    }
                                }
                                else if (accuralItems.FirstAccuralMethodFK == (int)LeaveTypeFirstAccuralMethod.FullAmount)
                                {
                                    finalAmount = accuralItems.EmployeeEarnsNumberOfUnits.Value;
                                }

                                comment = leaveTypeName + " Entitlement After " + accuralItems.AfterHireEligibilityMonths + " Months";
                                AddOrEditEntitlementAndEntitlementChange(usersItems.UserID, accuralItems.LeaveTypeFK.Value, finalAmount, comment);

                                List<LeaveType> ChildLeaveTypes = Context.LeaveTypes.Where(x => x.ParentLeaveTypeFK == accuralItems.LeaveTypeFK && x.TakeMaxAmountFromParent != null && x.IsActive == true).ToList();
                                if(ChildLeaveTypes != null)
                                {
                                    foreach(var leaveTypeItem in ChildLeaveTypes)
                                    {
                                        finalAmount = CalculateEntitlementQuantity(leaveTypeItem.TakeMaxAmountFromParent.Value, workingDaysFromHire, accuralItems.ProrateCalculationModeFK.Value, accuralItems.ProrateMethodFK.Value, usersItems.HireDate.Value.Year);
                                        
                                        //Not add entitlement from last year
                                        if (finalAmount > leaveTypeItem.TakeMaxAmountFromParent)
                                            finalAmount = leaveTypeItem.TakeMaxAmountFromParent.Value;

                                        comment = leaveTypeItem.LeaveTypeName + " Entitlement After " + accuralItems.AfterHireEligibilityMonths + " Months";
                                        AddOrEditEntitlementAndEntitlementChange(usersItems.UserID, leaveTypeItem.LeaveTypeID, finalAmount, comment);
                                    }
                                }
                            }
                            //}
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    
                }
            }
            //await Task.Delay(50000000);

            //await AddAccuralLeaveTypesForNewEmployeesAsync();
            WriteToFile("End Add Accural Leave Types For New Employees on " + DateTime.Now);

        }
        public void AddAccuralLeaveTypesForEmployeesAsync()
        {
            WriteToFile("Start Add Accural Leave Types For Employees on " + DateTime.Now);


            string comment = "Monthly Entitlement";
            List<User> users = Context.Users.Where(x => x.IsActive == true).ToList();

            List<int> accuralLeaveTypes = Context.LeaveTypes.Where(x => x.ParentLeaveTypeFK == null && x.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Accured && x.IsSuspended == false && x.IsActive == true).Select(x => x.LeaveTypeID).ToList();
            List<LeaveTypeAccuralRule> accuralRuleLeaveTypes = Context.LeaveTypeAccuralRules.Where(x => accuralLeaveTypes.Contains(x.LeaveTypeFK.Value) && x.GainingEligibilityMethodFK == (int)LeaveTypeGainingEligibilityMethod.FromTheFirstDayAtWork && x.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt && x.IsActive == true).ToList();

            foreach (var accuralItems in accuralRuleLeaveTypes)
            {
                if (accuralItems.EveryMonthAccuralDay == DateTime.Today.Day)
                {

                    foreach (var usersItems in users)
                    {
                        decimal seniorityYears = 0;
                        if ((usersItems.SeniorityBeforeHireYears != null && usersItems.SeniorityBeforeHireYears.ToString() != " " && usersItems.SeniorityBeforeHireYears.ToString() != string.Empty) &&
                                (usersItems.SeniorityBeforeHireMonth != null && usersItems.SeniorityBeforeHireMonth.ToString() != " ") && usersItems.SeniorityBeforeHireMonth.ToString() != string.Empty)
                        {
                            seniorityYears = usersItems.SeniorityBeforeHireYears.Value + (usersItems.SeniorityBeforeHireMonth.Value / 12);
                        }

                        DateTime lastDayyear = new DateTime(DateTime.Today.Year, 12, 31);
                        int workingDaysFromHire = (lastDayyear - usersItems.HireDate.Value).Days;
                        //Calculate seniority years after hiring
                        seniorityYears += ((decimal)((DateTime.Today.Date - usersItems.HireDate.Value.Date).Days) / (DateTime.IsLeapYear(DateTime.Today.Year) ? 366 : 365));
                        decimal finalAmount = 0;

                        bool check = CheckRestrictions(accuralItems.LeaveTypeFK.Value, usersItems);
                        if (check)
                        {
                            if (accuralItems.FirstAccuralMethodFK == (int)LeaveTypeFirstAccuralMethod.ProrateAmount)
                            {
                                if (seniorityYears >= accuralItems.OverSeniorityYears)
                                {
                                    finalAmount = CalculateEntitlementQuantity(accuralItems.OverSeniorityEmployeeEarns.Value, workingDaysFromHire, accuralItems.ProrateCalculationModeFK.Value, accuralItems.ProrateMethodFK.Value, usersItems.HireDate.Value.Year);
                                }
                                else
                                {
                                    finalAmount = CalculateEntitlementQuantity(accuralItems.EmployeeEarnsNumberOfUnits.Value, workingDaysFromHire, accuralItems.ProrateCalculationModeFK.Value, accuralItems.ProrateMethodFK.Value, usersItems.HireDate.Value.Year);
                                }
                            }
                            else if (accuralItems.FirstAccuralMethodFK == (int)LeaveTypeFirstAccuralMethod.FullAmount)
                            {
                                finalAmount = accuralItems.EmployeeEarnsNumberOfUnits.Value;
                            }

                            AddOrEditEntitlementAndEntitlementChange(usersItems.UserID, accuralItems.LeaveTypeFK.Value, finalAmount, comment);
                        }
                    }
                    
                }

                else
                {
                    continue;
                }

            }
            WriteToFile("End Add Accural Leave Types For Employees on " + DateTime.Now);


        }
        public void WriteToFile(string Message)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

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


    }

}
