using AddAcccuralLeaveTypesApp.DTO;
using AddAcccuralLeaveTypesApp.Model;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AddAcccuralLeaveTypesApp.BLL.Enums.StaticEnums;

namespace AddAcccuralLeaveTypesApp.BLL
{
    class LeaveTypesBLL
    {
        HRMSEntities Context;
        DateTime CreationDate;
        RequestBLL RequestBLL;
        public LeaveTypesBLL()
        {
            Context = new HRMSEntities();
            CreationDate = DateTime.Now;
            RequestBLL = new RequestBLL(Context, CreationDate);
        }

        ~LeaveTypesBLL()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }

        //public async Task ScheduleOfExecutionAsync()
        //{
        //    List<TimeSpan> ListOfTimes = new List<TimeSpan>();
        //    var timeValue = Context.Configurations.Where(x => x.ConfigurationKey == "AccuralLeaveTypeTime" && x.IsActive == true).Select(x => x.ConfigurationValue).FirstOrDefault().ToString();
        //    TimeSpan executionTime = TimeSpan.Parse(timeValue);
        //    DateTime current = DateTime.Now;
        //    TimeSpan timeToGo = executionTime - current.TimeOfDay;
        //    if (timeToGo >= TimeSpan.Zero)
        //    {
        //        WriteToFile("Set Delay time" + timeToGo + "On" + DateTime.Now);
        //        await Task.Delay(timeToGo);
        //        AddAccuralLeaveTypesForNewEmployeesAsync();
        //        AddAccuralLeaveTypesForEmployeesAsync();
        //    }
        //    TimeSpan EndDayTime = new TimeSpan(24, 30, 00);
        //    DateTime CurrentDayTime = DateTime.Now;
        //    TimeSpan TimeToNextDay = EndDayTime - CurrentDayTime.TimeOfDay;
        //    await Task.Delay(TimeToNextDay);
        //    await ScheduleOfExecutionAsync();
        //}
        //Round Up the total amount 
        private decimal RoundUP(double d)
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
        //Round Down the total amount 
        private decimal RoundDown(double d)
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
        //To make sure if the user have intitlement record or not
        private bool CheckUserHaveEntitlementForLeaveType(int UserID, int LeaveTypeID)
        {
            var userEntitlement = Context.UserEntitlements.Where(x => x.UserFK == UserID && x.LeaveTypeFK == LeaveTypeID).ToList();

            if (userEntitlement.Count() == 0)
                return true;
            else
                return false;
        }
        //To check restrictions
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
        //Calculate the total quantity of entitlement for user
        private decimal CalculateEntitlementQuantity(decimal TotalEarnsUnit, int WorkingDaysFromHire, int ProrteMode, int ProrteMethod, int Year)
        {
            decimal amount = 0;

            if (ProrteMode == (int)LeaveTypeProrateCalculationMode.BasedOnDays)
                amount = TotalEarnsUnit / (DateTime.IsLeapYear(Year) ? 366 : 365);
            else if (ProrteMode == (int)LeaveTypeProrateCalculationMode.BasedOnFullMonths)
                amount = TotalEarnsUnit / 12;

            decimal finalAmount = ((decimal)WorkingDaysFromHire / 30) * amount;

            if (ProrteMethod == (int)LeaveTypeProrateMethod.RoundAlwaysUp)
                return RoundUP((double)finalAmount);
            else if (ProrteMethod == (int)LeaveTypeProrateMethod.RoundToWhole)
                return RoundDown((double)finalAmount);

            return finalAmount;
        }
        //insert or update entitlement & entitlement change
        private void AddOrEditEntitlementAndEntitlementChange(int UserID, int LeaveTypeID, decimal Amount, string Comment, MonthlyEffectiveAccuredLeaveTypeDTO monthlyEffectiveAccuredLeaveTypeDTO)
        {
            UserEntitlement newUserEntitlement = new UserEntitlement();

            if (!Comment.Equals("Monthly Entitlement"))
            {
                newUserEntitlement = Context.UserEntitlements.Where(x => x.UserFK == UserID && x.LeaveTypeFK == LeaveTypeID && x.IsActive == true).FirstOrDefault();
                if (newUserEntitlement != null)
                {
                    newUserEntitlement.EntitlementTotal = Amount;
                    newUserEntitlement.ModificationDate = DateTime.Now;
                }
                else
                {
                    newUserEntitlement = new UserEntitlement() {
                        LeaveTypeFK = LeaveTypeID,
                        EntitlementTotal = Amount,//leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().TakeMaxAmountFromParent.Value,
                        LeaveDurationUnitFK = Context.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().DurationUnitFK.Value,
                        IsActive = true,
                        IsDeleted = false,
                        Year = DateTime.Today.Year,
                        CreationDate = DateTime.Now,
                        UserFK = UserID
                    };
                    
                    Context.UserEntitlements.Add(newUserEntitlement);
                }

            }

            else if (Comment.Equals("Monthly Entitlement") && monthlyEffectiveAccuredLeaveTypeDTO != null)
            {
                newUserEntitlement = Context.UserEntitlements.Where(x => x.LeaveTypeFK == LeaveTypeID &&
                    x.UserFK == UserID && x.IsActive == true).FirstOrDefault();
                if (newUserEntitlement != null)
                {
                    //newUserEntitlement.EntitlementTotal = Amount;
                    newUserEntitlement.IsActive = false;
                    newUserEntitlement.ModificationDate = DateTime.Now;
                }
                //Context.UserEntitlements.Edit(newUserEntitlement);
                //else
                //{
                newUserEntitlement = new UserEntitlement() {
                    LeaveTypeFK = LeaveTypeID,
                    EntitlementTotal = Amount,//leaveTypeBLL.Find(x => x.LeaveTypeID == items.LeaveTypeFK.Value).FirstOrDefault().TakeMaxAmountFromParent.Value,
                    LeaveDurationUnitFK = Context.LeaveTypes.Where(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault().DurationUnitFK.Value,
                    EffectiveDateFrom = monthlyEffectiveAccuredLeaveTypeDTO.EffectiveDateFrom,
                    EffectiveDateTo = monthlyEffectiveAccuredLeaveTypeDTO.EffectiveDateTo,
                    Month = monthlyEffectiveAccuredLeaveTypeDTO.Month,
                    Year = DateTime.Today.Year,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now,
                    UserFK = UserID
                };
                    
                Context.UserEntitlements.Add(newUserEntitlement);
                //}
            }

            if (newUserEntitlement != null)
            {
                UserEntitlementChange newUserEntitlementChange = new UserEntitlementChange()
                {
                    UserEntitlement = newUserEntitlement,
                    Comment = Comment,
                    CreationDate = DateTime.Now,
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
        //insert or update entitlement & entitlement change for monthly leave type(Excuse)
        private MonthlyEffectiveAccuredLeaveTypeDTO AddAndEditMonthlyEffectiveAccuredLeaveType(int LeaveTypeID)
        {
            MonthlyEffectiveAccuredLeaveType monthlyEffectiveAccuredLeaveType = new MonthlyEffectiveAccuredLeaveType();
            monthlyEffectiveAccuredLeaveType = Context.MonthlyEffectiveAccuredLeaveTypes.Where(x => x.LeaveTypeFK == LeaveTypeID && x.IsActive == true).FirstOrDefault();
            monthlyEffectiveAccuredLeaveType.IsActive = false;
            monthlyEffectiveAccuredLeaveType.ModificationDate = DateTime.Now;

            MonthlyEffectiveAccuredLeaveType newMonthlyEffectiveAccuredLeaveType = new MonthlyEffectiveAccuredLeaveType()
            {
                LeaveTypeFK = LeaveTypeID,
                EffectiveDateFrom = DateTime.Now.Date,
                EffectiveDateTo = DateTime.Now.AddDays(-1).AddMonths(1).Date,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                CreationDate = CreationDate,
                IsActive = true,
                IsDeleted = false
            };

            Context.MonthlyEffectiveAccuredLeaveTypes.Add(newMonthlyEffectiveAccuredLeaveType);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteToFile("Entitlement Error " + ex.Message);

            }

            MonthlyEffectiveAccuredLeaveTypeDTO monthlyEffectiveAccuredLeaveTypeDTO = new MonthlyEffectiveAccuredLeaveTypeDTO()
            {
                EffectiveDateFrom = newMonthlyEffectiveAccuredLeaveType.EffectiveDateFrom.Value,
                EffectiveDateTo = newMonthlyEffectiveAccuredLeaveType.EffectiveDateTo.Value,
                Month = newMonthlyEffectiveAccuredLeaveType.Month.Value,
                Year = newMonthlyEffectiveAccuredLeaveType.Year.Value
            };
            return monthlyEffectiveAccuredLeaveTypeDTO;
        }
        //add Annual & Casual leave type for new employee
        public void AddAccuralLeaveTypesForNewEmployees()
        {
            WriteToFile("Start Add Accural Leave Types For New Employees on " + DateTime.Now);
            string comment = "";
            List<string> filePaths = new List<string>();
            List<UserEntitlementDTO> userEntitlementDTO = new List<UserEntitlementDTO>();

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
                    if (EligiableDateForAccuredLeaves.Date == DateTime.Today.Date) //&& CheckUserHaveEntitlementForLeaveType(usersItems.UserID, accuralItems.LeaveTypeFK.Value))
                    {
                        if (accuralItems.GainingEligibilityMethodFK == (int)LeaveTypeGainingEligibilityMethod.AfterHire && accuralItems.AfterHireEligibilityMonths != null)
                        {
                            //DateTime HiringDateAfterAddingMonths = usersItems.HireDate.Value.AddMonths(AccuralItems.AfterHireEligibilityMonths.Value);
                            //To check if the experience of employee exceed 10 years 
                            if ((usersItems.SeniorityBeforeHireYears != null && usersItems.SeniorityBeforeHireYears.ToString() != " " && usersItems.SeniorityBeforeHireYears.ToString() != string.Empty) &&
                                (usersItems.SeniorityBeforeHireMonth != null && usersItems.SeniorityBeforeHireMonth.ToString() != " ") && usersItems.SeniorityBeforeHireMonth.ToString() != string.Empty)
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
                                AddOrEditEntitlementAndEntitlementChange(usersItems.UserID, accuralItems.LeaveTypeFK.Value, finalAmount, comment, null);

                                userEntitlementDTO.Add(new UserEntitlementDTO()
                                {
                                    CypressID = usersItems.UserID,
                                    AccessControlID = usersItems.AccessControlUserFK != null ? usersItems.AccessControlUserFK.Value.ToString() : "",
                                    EmployeeName = usersItems.UserName,
                                    ContractType = ((ContractTypes)Enum.Parse(typeof(ContractTypes), usersItems.ContractTypeFK.ToString())).ToString(),
                                    LeaveTypeName = leaveTypeName,
                                    Amount = finalAmount,
                                    HiringDate = usersItems.HireDate.Value.Date
                                });

                                WriteToFile(" Add " + leaveTypeName + " Leave Types For " + usersItems.UserName + " on " + DateTime.Now);

                                List<LeaveType> ChildLeaveTypes = Context.LeaveTypes.Where(x => x.ParentLeaveTypeFK == accuralItems.LeaveTypeFK && x.TakeMaxAmountFromParent != null && x.IsActive == true).ToList();
                                if (ChildLeaveTypes != null)
                                {
                                    foreach (var leaveTypeItem in ChildLeaveTypes)
                                    {
                                        finalAmount = CalculateEntitlementQuantity(leaveTypeItem.TakeMaxAmountFromParent.Value, workingDaysFromHire, accuralItems.ProrateCalculationModeFK.Value, accuralItems.ProrateMethodFK.Value, usersItems.HireDate.Value.Year);

                                        //Not add entitlement from last year
                                        if (finalAmount > leaveTypeItem.TakeMaxAmountFromParent)
                                            finalAmount = leaveTypeItem.TakeMaxAmountFromParent.Value;

                                        comment = leaveTypeItem.LeaveTypeName + " Entitlement After " + accuralItems.AfterHireEligibilityMonths + " Months";
                                        AddOrEditEntitlementAndEntitlementChange(usersItems.UserID, leaveTypeItem.LeaveTypeID, finalAmount, comment, null);

                                        userEntitlementDTO.Add(new UserEntitlementDTO() {
                                            CypressID = usersItems.UserID,
                                            AccessControlID = usersItems.AccessControlUserFK != null ? usersItems.AccessControlUserFK.Value.ToString() : "",
                                            EmployeeName = usersItems.UserName,
                                            ContractType = ((ContractTypes)Enum.Parse(typeof(ContractTypes), usersItems.ContractTypeFK.ToString())).ToString(),
                                            LeaveTypeName = leaveTypeItem.LeaveTypeName,
                                            Amount = finalAmount,
                                            HiringDate = usersItems.HireDate.Value.Date
                                        });

                                        WriteToFile(" Add " + leaveTypeItem.LeaveTypeName + " Leave Types For " + usersItems.UserName + " on " + DateTime.Now);
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
            if (userEntitlementDTO.Count > 0)
            {
                filePaths.Add(SaveToExcel(userEntitlementDTO, null,"Accural Leave Types For New Employees", "Entitlements"));
                try
                {
                    SendMail(filePaths, "New Accural Leave Types For New Employees", "Accural");
                }
                catch (Exception ex)
                {
                    WriteToFile("Error " + ex.Message);
                    throw;
                }
                
            }

            WriteToFile("End Add Accural Leave Types For New Employees on " + DateTime.Now);

        }
        //add Excuse monthly for employees
        public void AddAccuralLeaveTypesForEmployees()
        {
            WriteToFile("Start Add Monthly Leave Types For Employees on " + DateTime.Now);

            string comment = "Monthly Entitlement";
            List<string> filePaths = new List<string>();
            List<UserEntitlementDTO> userEntitlementDTO = new List<UserEntitlementDTO>();

            List<User> users = Context.Users.Where(x => x.IsActive == true).ToList();

            List<int> accuralLeaveTypes = Context.LeaveTypes.Where(x => x.ParentLeaveTypeFK == null && x.EntitlementTypeFK == (int)LeaveTypeEntitlementType.Accured && x.IsSuspended == false && x.IsActive == true).Select(x => x.LeaveTypeID).ToList();
            List<LeaveTypeAccuralRule> accuralRuleLeaveTypes = Context.LeaveTypeAccuralRules.Where(x => accuralLeaveTypes.Contains(x.LeaveTypeFK.Value) && x.GainingEligibilityMethodFK == (int)LeaveTypeGainingEligibilityMethod.FromTheFirstDayAtWork && x.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt && x.IsActive == true).ToList();

            foreach (var accuralItems in accuralRuleLeaveTypes)
            {
                string leaveTypeName = Context.LeaveTypes.Where(x => x.LeaveTypeID == accuralItems.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                if (accuralItems.EveryMonthAccuralDay == DateTime.Today.Day)
                {
                    List<ApprovedBySystemDTO> approvedBySystemDTO = RequestBLL.ApprovedBySystem(accuralItems.LeaveTypeFK.Value);
                    filePaths.Add(SaveToExcel(null, approvedBySystemDTO, "System Approved & Rejected Requests", "Requests"));

                    MonthlyEffectiveAccuredLeaveTypeDTO monthlyEffectiveAccuredLeaveTypeDTO = AddAndEditMonthlyEffectiveAccuredLeaveType(accuralItems.LeaveTypeFK.Value);

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

                            AddOrEditEntitlementAndEntitlementChange(usersItems.UserID, accuralItems.LeaveTypeFK.Value, finalAmount, comment, monthlyEffectiveAccuredLeaveTypeDTO);

                            userEntitlementDTO.Add(new UserEntitlementDTO()
                            {
                                CypressID = usersItems.UserID,
                                AccessControlID = usersItems.AccessControlUserFK != null ? usersItems.AccessControlUserFK.Value.ToString() : "",
                                EmployeeName = usersItems.UserName,
                                ContractType = ((ContractTypes)Enum.Parse(typeof(ContractTypes), usersItems.ContractTypeFK.ToString())).ToString(),
                                LeaveTypeName = leaveTypeName,
                                Amount = finalAmount,
                                HiringDate = usersItems.HireDate.Value.Date
                            });

                            WriteToFile(" Add Monthly Leave Types For " + usersItems.UserName + " on " + DateTime.Now);
                        }
                    }
                }

                else
                {
                    continue;
                }

            }
            if(userEntitlementDTO.Count > 0)
            {
                filePaths.Add(SaveToExcel(userEntitlementDTO, null,"Monthly Leave Types Entitlements", "Entitlements"));

                try { 
                    SendMail(filePaths, "New Monthly Leave Types For Employees", "Monthly");
                }
                    catch (Exception ex)
                {
                    WriteToFile("Error " + ex.Message);
                    throw;
                }
        }
            
            WriteToFile("End Add Monthly Leave Types For Employees on " + DateTime.Now);


        }

        private string SaveToExcel(List<UserEntitlementDTO> userEntitlementDTO, List<ApprovedBySystemDTO> approvedBySystemDTOs, string fileName, string content)
        {
            DataTable dtRequest = new DataTable();
            var workbook = new XLWorkbook();

            if (content == "Entitlements" && userEntitlementDTO != null)
            {
                dtRequest.Columns.Add("Cypress ID");
                dtRequest.Columns.Add("Access Control ID");
                dtRequest.Columns.Add("Employee Name");
                dtRequest.Columns.Add("Hire Date");
                dtRequest.Columns.Add("Contract Type");
                dtRequest.Columns.Add("Leave Type Name");
                dtRequest.Columns.Add("Total Amount");

                foreach (var item in userEntitlementDTO)
                {
                    dtRequest.Rows.Add(item.CypressID, item.AccessControlID, item.EmployeeName, item.HiringDate.Date.ToString("dd/MM/yyyy"), item.ContractType, item.LeaveTypeName, item.Amount);
                }

                workbook.AddWorksheet(dtRequest, "Entitlements");

            } else if (content == "Requests" && approvedBySystemDTOs != null)
            {
                dtRequest.Columns.Add("Cypress ID");
                dtRequest.Columns.Add("Access Control ID");
                dtRequest.Columns.Add("Employee Name");
                dtRequest.Columns.Add("Request ID");
                dtRequest.Columns.Add("Request Status"); 
                dtRequest.Columns.Add("Exception");

                foreach (var item in approvedBySystemDTOs)
                {
                    dtRequest.Rows.Add(item.EmployeeID, item.AccessControlID, item.EmployeeName, item.RequestID, item.RequestStatusName, item.Exception);
                }

                workbook.AddWorksheet(dtRequest, "Requests");
            }
            

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Excels";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = path + "\\" + fileName + " " + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".xltm";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                StreamWriter sw = File.CreateText(filepath);
                sw.Close();
            }
            workbook.SaveAs(filepath);
            return filepath;
        }

        private void SendMail(List<string> FilePaths, string Subject, string Type)
        {
            MailingDTO mailingDTO = new MailingDTO();
            Mailing mail = new Mailing();

            mailingDTO.To = new List<string>();
            mailingDTO.CC = new List<string>();

            mailingDTO.Subject = Subject;

            mailingDTO.To.Add(Context.Configurations.Where(x => x.ConfigurationKey == "HREmail").FirstOrDefault().ConfigurationValue);
            mail.PrepareMailToSent(mailingDTO, FilePaths, Type);
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
    }
}
