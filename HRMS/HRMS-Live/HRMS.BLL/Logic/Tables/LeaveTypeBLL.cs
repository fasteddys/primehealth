using HRMS.BLL.StaticData;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class LeaveTypeBLL : Repository<LeaveType>
    {
        DateTime creationDate;
        LeaveTypeRestrictedGenderBLL leaveTypeRestrictedGenderBLL;
        LeaveTypeRestrictedDepBLL leaveTypeRestrictedDepBLL;
        LeaveTypeRestrictedContractTypeBLL leaveTypeRestrictedContractTypeBLL;
        LeaveTypeRestrictedSubDepBLL leaveTypeRestrictedSubDepBLL;
        LeaveTypeRestrictedEmployeeBLL leaveTypeRestrictedEmployeeBLL;
        LeaveTypeCarryOverRuleBLL leaveTypeCarryOverRuleBLL;
        LeaveTypeAccuralRuleBLL leaveTypeAccuralRuleBLL;
        LeaveTypeRestrictionBLL leaveTypeRestrictionBLL;
        LeaveTypeFieldsBLL leaveTypeFieldsBLL;
        LeaveTypePartialDurationBLL partialDurationBLL;
        UserBLL userBLL;
        UserEntitlementBLL userentitlementBLL;

        public LeaveTypeBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            leaveTypeRestrictedGenderBLL = new LeaveTypeRestrictedGenderBLL(Context, CreationDate);
            leaveTypeRestrictedSubDepBLL = new LeaveTypeRestrictedSubDepBLL(Context, CreationDate);
            leaveTypeRestrictedDepBLL = new LeaveTypeRestrictedDepBLL(Context, CreationDate);
            leaveTypeRestrictedContractTypeBLL = new LeaveTypeRestrictedContractTypeBLL(Context, CreationDate);
            leaveTypeRestrictedEmployeeBLL = new LeaveTypeRestrictedEmployeeBLL(Context, CreationDate);
            leaveTypeCarryOverRuleBLL = new LeaveTypeCarryOverRuleBLL(Context, CreationDate);
            leaveTypeAccuralRuleBLL = new LeaveTypeAccuralRuleBLL(Context, CreationDate);
            leaveTypeRestrictionBLL = new LeaveTypeRestrictionBLL(Context, CreationDate);
            leaveTypeFieldsBLL = new LeaveTypeFieldsBLL(Context, CreationDate);
            partialDurationBLL = new LeaveTypePartialDurationBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            userentitlementBLL = new UserEntitlementBLL(Context, CreationDate);

        }
        public List<LeaveTypeDTO> GetAllLeavesType()
        {
            List<LeaveTypeDTO> ListLeaveType = new List<LeaveTypeDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true))
            {
                LeaveTypeAccuralRule leaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == item.LeaveTypeID).FirstOrDefault();

                ListLeaveType.Add(new LeaveTypeDTO
                {
                    LeaveTypeID = item.LeaveTypeID,
                    LeaveTypeName = item.LeaveTypeName,
                    EmployeeEarnsNumberOfUnits= leaveTypeAccuralRule?.EmployeeEarnsNumberOfUnits,
                    OverSeniorityEmployeeEarns= leaveTypeAccuralRule?.OverSeniorityEmployeeEarns,

                });
            }
            return ListLeaveType;
        }
        public List<LeaveTypeDTO> GetLeavesTypeForUser(int UserID)
        {
            List<LeaveTypeDTO> ListLeaveType = new List<LeaveTypeDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true && p.IsSuspended == false && p.IsDeleted == false))
            {
                LeaveTypeAccuralRule leaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == item.LeaveTypeID).FirstOrDefault();
                User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
                bool IsActiveLeaveType = true;
                if (leaveTypeRestrictedGenderBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.LeaveGenderRestrictionID == User.GenderFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;

                }

                if (leaveTypeRestrictedEmployeeBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.EmployeeFK == User.UserID && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }

                if (leaveTypeRestrictedContractTypeBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.ContractTypeFK == User.ContractTypeFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }

                if (leaveTypeRestrictedDepBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.DepartmentFK == User.DepartmentFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }

                if (leaveTypeRestrictedSubDepBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.SubDepartmentFK == User.SubDepartmentFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }
                if (IsActiveLeaveType)
                {
                    ListLeaveType.Add(new LeaveTypeDTO
                    {
                        LeaveTypeID = item.LeaveTypeID,
                        LeaveTypeName = item.LeaveTypeName,
                        EmployeeEarnsNumberOfUnits = leaveTypeAccuralRule?.EmployeeEarnsNumberOfUnits,
                        OverSeniorityEmployeeEarns = leaveTypeAccuralRule?.OverSeniorityEmployeeEarns,
                        Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.DurationUnitFK.ToString())).ToString()



                    });
                }
            }
            return ListLeaveType;
        }


        public List<LeaveTypeDTO> GetLeavesTypeForUserForEntitlmentChanges(int UserID)
        {
            List<LeaveTypeDTO> ListLeaveType = new List<LeaveTypeDTO>();
            foreach (var item in GetAll().Where(p => p.IsActive == true && p.IsSuspended == false && p.IsDeleted == false))
            {
                LeaveTypeAccuralRule leaveTypeAccuralRule = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK == item.LeaveTypeID).FirstOrDefault();
                User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
                bool IsActiveLeaveType = true;
                if (leaveTypeRestrictedGenderBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.LeaveGenderRestrictionID == User.GenderFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;

                }

                if (leaveTypeRestrictedEmployeeBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.EmployeeFK == User.UserID && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }

                if (leaveTypeRestrictedContractTypeBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.ContractTypeFK == User.ContractTypeFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }

                if (leaveTypeRestrictedDepBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.DepartmentFK == User.DepartmentFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;


                }

                if (leaveTypeRestrictedSubDepBLL.Find(x => x.IsActive == true && x.IsDeleted == false && x.SubDepartmentFK == User.SubDepartmentFK && x.LeaveTypeFK == item.LeaveTypeID).Count() > 0)
                {
                    IsActiveLeaveType = false;

                }

                if (item.ParentLeaveTypeFK == null && item.EntitlementTypeFK == (int)StaticEnums.LeaveTypeEntitlementType.Unlimited)
                {
                    IsActiveLeaveType = false;
                }

                if (IsActiveLeaveType)
                {
                    if (item.ParentLeaveTypeFK==null)
                    {
                        ListLeaveType.Add(new LeaveTypeDTO
                        {
                            LeaveTypeID = item.LeaveTypeID,
                            LeaveTypeName = item.LeaveTypeName,
                            EmployeeEarnsNumberOfUnits = leaveTypeAccuralRule?.EmployeeEarnsNumberOfUnits,
                            OverSeniorityEmployeeEarns = leaveTypeAccuralRule?.OverSeniorityEmployeeEarns,
                            Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.DurationUnitFK.ToString())).ToString()



                        });
                    }
                    else if (item.ParentLeaveTypeFK!=null)
                    {
                        if (item.TakeMaxAmountFromParent != null) //&& leaveTypeAccuralRuleBLL.Find(p => p.LeaveTypeFK == item.LeaveTypeID)?.FirstOrDefault() != null)
                        {
                            ListLeaveType.Add(new LeaveTypeDTO
                            {
                                LeaveTypeID = item.LeaveTypeID,
                                LeaveTypeName = item.LeaveTypeName,
                                EmployeeEarnsNumberOfUnits = leaveTypeAccuralRule?.EmployeeEarnsNumberOfUnits,
                                OverSeniorityEmployeeEarns = leaveTypeAccuralRule?.OverSeniorityEmployeeEarns,
                                Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.DurationUnitFK.ToString())).ToString()



                            });
                        }
                        
                    }
                }
            }
            return ListLeaveType;
        }

        public void GetLeaveTypeFields(int LeaveTypeID)
        {

        }
        public void LoadLeavesTypeConfiguration(int LeaveTypeID)
        {
            LeaveTypeAdditionContainerDTO LeaveTypeContainerDTO = new LeaveTypeAdditionContainerDTO();
            LeaveType leaveType = new LeaveType();
            leaveType = Find(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault();
            LeaveTypeContainerDTO.AllowNegativeentitlement = (bool)leaveType.AllowNegativeEntitlement;
            LeaveTypeContainerDTO.LeaveTypeConsiderationsID = leaveType.ConsiderAsFK;
            LeaveTypeContainerDTO.DurationUnitID = leaveType.DurationUnitFK;
            LeaveTypeContainerDTO.EnableAttachments = (bool)leaveType.HasAttachments;
            LeaveTypeContainerDTO.AttachmentsRequired = (bool)leaveType.IsAttachmentsRequired;
            LeaveTypeContainerDTO.TakeEntitlementFromID = leaveType.ParentLeaveTypeFK;
            LeaveTypeContainerDTO.TakemaxamountFromParentLeaveType = (decimal)leaveType.TakeMaxAmountFromParent;
            LeaveTypeContainerDTO.MaxmimAllowNegativeEntitlement = (decimal)leaveType.MaxAllowedNegativeEntitlement;
            LeaveTypeContainerDTO.EntitlementTypeID = leaveType.EntitlementSourceFK;
            LeaveTypeContainerDTO.AccuralID = leaveType.EntitlementTypeFK;
            LeaveTypeContainerDTO.MinLeaveDurationWithinOneDay = (int)leaveType.OneDayMinDurationFK;
            LeaveTypeContainerDTO.LeaveName = leaveType.LeaveTypeName;
            LeaveTypeContainerDTO.EnablePartailUnit = (bool)leaveType.AllowSameDayPartialEntitlement;

            LeaveTypeCarryOverRule LeaveTypeCarryOverRules = new LeaveTypeCarryOverRule();
            LeaveTypeContainerDTO.MaxNumberOfDaysToCarriedOver = LeaveTypeCarryOverRules.CarryOverMaxEntitlementsUnit;
            LeaveTypeContainerDTO.EnableCarryoverNegativeEntitlement = (bool)LeaveTypeCarryOverRules.CarryOverNegativeEntitlement;
            LeaveTypeContainerDTO.EnableCarryOverUnusedEntitlement = (bool)LeaveTypeCarryOverRules.CarryOverUnusedEntitlements;

            LeaveTypeContainerDTO.NumberOfMonthsToExpireCarriedOverLeave = (int)LeaveTypeCarryOverRules.ExpireCarriedOverEntitlementMonths;
            LeaveTypeContainerDTO.ExpireCarriedOverAfterTime = (bool)LeaveTypeCarryOverRules.ExpireCarriedOverEntitlements;
            LeaveTypeContainerDTO.LeaveTypeFK = leaveType.LeaveTypeID;





            LeaveTypeAccuralRule LeaveTypeAccuralRule = new LeaveTypeAccuralRule();

            LeaveTypeContainerDTO.AccuralPeriodID = (int)LeaveTypeAccuralRule.AccuralPeriodFK;
            LeaveTypeContainerDTO.EmployeeGainsEligibilityAfterID = LeaveTypeAccuralRule.AfterHireEligibilityMonths;
            LeaveTypeContainerDTO.EffectiveDate = LeaveTypeAccuralRule.EffectiveDate.ToString();
            LeaveTypeContainerDTO.EmployeeEarnsNumberOfUnits = LeaveTypeAccuralRule.EmployeeEarnsNumberOfUnits;
            LeaveTypeContainerDTO.MonthlyAaccuralDays = (int)LeaveTypeAccuralRule.EveryMonthAccuralDay;
            LeaveTypeContainerDTO.FirstAccuralMethodForNewEmployeesID = (int)LeaveTypeAccuralRule.FirstAccuralMethodFK;
            LeaveTypeContainerDTO.EmployeeGainsEligibilityAfterID = LeaveTypeAccuralRule.GainingEligibilityMethodFK;
            leaveType.LeaveTypeID = (int)LeaveTypeAccuralRule.LeaveTypeFK;
            LeaveTypeContainerDTO.OverSeniorityEmployeeEarns = LeaveTypeAccuralRule.OverSeniorityEmployeeEarns;
            LeaveTypeContainerDTO.OverSeniorityYears = LeaveTypeAccuralRule.OverSeniorityYears;
            LeaveTypeContainerDTO.ProrateCalculationModeID = LeaveTypeAccuralRule.ProrateCalculationModeFK;
            LeaveTypeContainerDTO.ProrateMethodID = LeaveTypeAccuralRule.ProrateMethodFK;

            List<Entities.LeaveTypeField> ListLeaveTypeField = new List<Entities.LeaveTypeField>();
            foreach (var item in ListLeaveTypeField)
            {
                LeaveTypeContainerDTO.LeaveTypeFields.Add(new LeaveTypeFieldDTO
                {
                    LeaveTypeFieldID = item.LeaveTypeFieldID,
                    LeaveTypeFieldVisibilityID = (int)item.LeaveTypeFieldsVisibilityFK,
                    LeaveTypeFieldName = ((StaticEnums.LeaveTypeField)Enum.Parse(typeof(StaticEnums.LeaveTypeField), item.LeaveTypeFieldID.ToString())).ToString()



                });
            }
            List<LeaveTypeRestrictedGender> LeaveTypeRestrictedGender = new List<LeaveTypeRestrictedGender>();


            foreach (var item in LeaveTypeRestrictedGender)
            {
                LeaveTypeContainerDTO.RestrictedEntitys.GenderIDS.Add(item.LeaveGenderRestrictionID);
            }
            List<LeaveTypeRestrictedSubDep> LeaveTypeRestrictedSubDep = new List<LeaveTypeRestrictedSubDep>();


            foreach (var item in LeaveTypeRestrictedSubDep)
            {
                LeaveTypeContainerDTO.RestrictedEntitys.SubDepartmentIDS.Add((int)item.SubDepartmentFK);
            }

            List<LeaveTypeRestrictedDep> LeaveTypeRestrictedDep = new List<LeaveTypeRestrictedDep>();
            foreach (var item in LeaveTypeRestrictedDep)
            {
                LeaveTypeContainerDTO.RestrictedEntitys.DepartmentIDS.Add((int)item.DepartmentFK);
            }


            List<LeaveTypeRestrictedEmployee> LeaveTypeRestrictedEmployee = new List<LeaveTypeRestrictedEmployee>();
            foreach (var item in LeaveTypeRestrictedEmployee)
            {
                LeaveTypeContainerDTO.RestrictedEntitys.UserIDS.Add((int)item.EmployeeFK);
            }

            List<LeaveTypeRestrictedContractType> LeaveTypeRestrictedContractTypes = new List<LeaveTypeRestrictedContractType>();
            foreach (var item in LeaveTypeRestrictedContractTypes)
            {
                LeaveTypeContainerDTO.RestrictedEntitys.UserIDS.Add((int)item.ContractTypeFK);
            }


            List<LeaveTypeRestriction> LeaveTypeRestriction = new List<LeaveTypeRestriction>();



            if (LeaveTypeContainerDTO.AbsenceNotLongerThan == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.AbsenceNotLongerThan,
                    RestrictionUnitTypeFK = LeaveTypeContainerDTO.DurationUnitID,
                    RestrictionUnit = LeaveTypeContainerDTO.NumberAbsenceNotLongerThan



                });

            }
            // Not Completed
            if (LeaveTypeContainerDTO.AbsenceNotShorterThan == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.AbsenceNotShorterThan,
                    RestrictionUnitTypeFK = LeaveTypeContainerDTO.DurationUnitID,
                    RestrictionUnit = LeaveTypeContainerDTO.NumberAbsenceNotShorterThan




                });

            }
            if (LeaveTypeContainerDTO.AbsenceRequestedUpTo == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.AbsenceRequestedUpTo,
                    RestrictionUnitTypeFK = (int)LeaveDurationUnit.Days,
                    RestrictionUnit = LeaveTypeContainerDTO.NumberDaysRequestedUpTo


                });

            }
            if (LeaveTypeContainerDTO.IfAbsenceLongerThan == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.IfAbsenceLongerThan,
                    RestrictionUnitTypeFK = LeaveTypeContainerDTO.DurationUnitID,
                    RestrictionUnit = LeaveTypeContainerDTO.NumberOfDayIfAbsenceLongerThan,
                    OtherRestrictionUnit = LeaveTypeContainerDTO.NumberOfDaysAllowedToMakeRequestInThePast,

                });

            }
            leaveTypeRestrictionBLL.AddRange(LeaveTypeRestriction);

        }
        public void AddLeavesType(LeaveTypeAdditionContainerDTO AddLeaveTypeContainerDTO)
        {

            LeaveType leaveType = new LeaveType
            {
                AllowNegativeEntitlement = AddLeaveTypeContainerDTO.AllowNegativeentitlement,
                ArName = "",
                ConsiderAsFK = AddLeaveTypeContainerDTO.LeaveTypeConsiderationsID,
                CreationDate = creationDate,
                DurationUnitFK = AddLeaveTypeContainerDTO.DurationUnitID,
                HasAttachments = AddLeaveTypeContainerDTO.EnableAttachments,
                IsActive = true,
                IsDeleted = false,
                IsAttachmentsRequired = AddLeaveTypeContainerDTO.AttachmentsRequired,
                ParentLeaveTypeFK = AddLeaveTypeContainerDTO.TakeEntitlementFromID,
                TakeMaxAmountFromParent = AddLeaveTypeContainerDTO.TakemaxamountFromParentLeaveType,
                MaxAllowedNegativeEntitlement = AddLeaveTypeContainerDTO.MaxmimAllowNegativeEntitlement,
                EntitlementSourceFK = AddLeaveTypeContainerDTO.EntitlementTypeID,
                EntitlementTypeFK = AddLeaveTypeContainerDTO.AccuralID,
                OneDayMinDurationFK = AddLeaveTypeContainerDTO.MinLeaveDurationWithinOneDay,
                LeaveTypeName = AddLeaveTypeContainerDTO.LeaveName,
                AllowSameDayPartialEntitlement = AddLeaveTypeContainerDTO.EnablePartailUnit,
                IsSuspended = AddLeaveTypeContainerDTO.IsSuspend

            };
            Add(leaveType);
           
            List<LeaveTypePartialDuration> ListPartialDuration = new List<LeaveTypePartialDuration>();

            foreach (var item in AddLeaveTypeContainerDTO.LeavePartailUnit)
            {
                LeaveTypePartialDuration LeaveTypePartialDuration = new LeaveTypePartialDuration
                {
                    CreationDate = creationDate,
                    PartialDurationUnitFK = item,
                    LeaveType = leaveType,
                    IsActive = true,
                    IsDeleted = false,
                     


                };

                partialDurationBLL.Add(LeaveTypePartialDuration);

            }




            foreach (var item in AddLeaveTypeContainerDTO.RestrictedEntitys.GenderIDS)
            {
                LeaveTypeRestrictedGender LeaveTypeRestrictedGender = new LeaveTypeRestrictedGender
                {
                    CreationDate = creationDate,
                    GenderFK = item,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,

                };

                leaveTypeRestrictedGenderBLL.Add(LeaveTypeRestrictedGender);

            }
            foreach (var item in AddLeaveTypeContainerDTO.RestrictedEntitys.SubDepartmentIDS)
            {
                LeaveTypeRestrictedSubDep LeaveTypeRestrictedSubDep = new LeaveTypeRestrictedSubDep
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    SubDepartmentFK = item
                };
                leaveTypeRestrictedSubDepBLL.Add(LeaveTypeRestrictedSubDep);

            }

            foreach (var item in AddLeaveTypeContainerDTO.RestrictedEntitys.DepartmentIDS)
            {
                LeaveTypeRestrictedDep LeaveTypeRestrictedDep = new LeaveTypeRestrictedDep
                {
                    CreationDate = creationDate,
                    DepartmentFK = item,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                };
                leaveTypeRestrictedDepBLL.Add(LeaveTypeRestrictedDep);


            }
            foreach (var item in AddLeaveTypeContainerDTO.RestrictedEntitys.UserIDS)
            {
                LeaveTypeRestrictedEmployee LeaveTypeRestrictedEmployee = new LeaveTypeRestrictedEmployee
                {
                    CreationDate = creationDate,
                    EmployeeFK = item,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,

                };
                leaveTypeRestrictedEmployeeBLL.Add(LeaveTypeRestrictedEmployee);

            }
            foreach (var item in AddLeaveTypeContainerDTO.RestrictedEntitys.ContractTypeIDS)
            {
                LeaveTypeRestrictedContractType LeaveTypeRestrictedContractTypes = new LeaveTypeRestrictedContractType
                {
                    CreationDate = creationDate,
                    ContractTypeFK = item,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,

                };
                leaveTypeRestrictedContractTypeBLL.Add(LeaveTypeRestrictedContractTypes);

            }

            List<Entities.LeaveTypeField> ListLeaveTypeField = new List<Entities.LeaveTypeField>();
            foreach (var item in AddLeaveTypeContainerDTO.LeaveTypeFields)
            {
                leaveTypeFieldsBLL.Add(new Entities.LeaveTypeField
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    LeaveTypeFieldFK = item.LeaveTypeFieldID,
                    LeaveTypeFieldsVisibilityFK = item.LeaveTypeFieldVisibilityID,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    
                });
            }

            // leaveTypeFieldsBLL.AddRange(ListLeaveTypeField);


            LeaveTypeCarryOverRule LeaveTypeCarryOverRules = new LeaveTypeCarryOverRule
            {
                CarryOverMaxEntitlementsUnit = AddLeaveTypeContainerDTO.MaxNumberOfDaysToCarriedOver,
                CarryOverNegativeEntitlement = AddLeaveTypeContainerDTO.EnableCarryoverNegativeEntitlement,
                CarryOverUnusedEntitlements = AddLeaveTypeContainerDTO.EnableCarryOverUnusedEntitlement,
                CreationDate = creationDate,
                LeaveTypeFK = leaveType.LeaveTypeID,
                ExpireCarriedOverEntitlementMonths = AddLeaveTypeContainerDTO.NumberOfMonthsToExpireCarriedOverLeave,
                ExpireCarriedOverEntitlements = AddLeaveTypeContainerDTO.ExpireCarriedOverAfterTime,
                IsActive = true,
                IsDeleted = false,
            };
            leaveTypeCarryOverRuleBLL.Add(LeaveTypeCarryOverRules);

            if (AddLeaveTypeContainerDTO.AccuralPeriodID!=0)
            {
                LeaveTypeAccuralRule LeaveTypeAccuralRule = new LeaveTypeAccuralRule
                {
                    AccuralPeriodFK = AddLeaveTypeContainerDTO.AccuralPeriodID,
                    AfterHireEligibilityMonths = AddLeaveTypeContainerDTO.NumberOfMonthsTogainseligibility,
                    CreationDate = creationDate,
                    EffectiveDate =DateTime.ParseExact(AddLeaveTypeContainerDTO.EffectiveDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    EmployeeEarnsNumberOfUnits = AddLeaveTypeContainerDTO.EmployeeEarnsNumberOfUnits,
                    EveryMonthAccuralDay = AddLeaveTypeContainerDTO.MonthlyAaccuralDays,
                    FirstAccuralMethodFK = AddLeaveTypeContainerDTO.FirstAccuralMethodForNewEmployeesID,
                    GainingEligibilityMethodFK = AddLeaveTypeContainerDTO.EmployeeGainsEligibilityAfterID,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    OverSeniorityEmployeeEarns = AddLeaveTypeContainerDTO.OverSeniorityEmployeeEarns,
                    OverSeniorityYears = AddLeaveTypeContainerDTO.OverSeniorityYears,
                    ProrateCalculationModeFK = AddLeaveTypeContainerDTO.ProrateCalculationModeID,
                    ProrateMethodFK = AddLeaveTypeContainerDTO.ProrateMethodID,

                };
                leaveTypeAccuralRuleBLL.Add(LeaveTypeAccuralRule);
            }
            List<LeaveTypeRestriction> LeaveTypeRestriction = new List<LeaveTypeRestriction>();
            if (AddLeaveTypeContainerDTO.AbsenceNotLongerThan == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.AbsenceNotLongerThan,
                    RestrictionUnitTypeFK = AddLeaveTypeContainerDTO.DurationUnitID,
                    RestrictionUnit = AddLeaveTypeContainerDTO.NumberAbsenceNotLongerThan



                });

            }
            if (AddLeaveTypeContainerDTO.AbsenceNotShorterThan == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.AbsenceNotShorterThan,
                    RestrictionUnitTypeFK = AddLeaveTypeContainerDTO.DurationUnitID,
                    RestrictionUnit = AddLeaveTypeContainerDTO.NumberAbsenceNotShorterThan




                });

            }

            if (AddLeaveTypeContainerDTO.AbsenceRequestedUpTo == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.AbsenceRequestedUpTo,
                    RestrictionUnitTypeFK = (int)LeaveDurationUnit.Days,
                    RestrictionUnit = AddLeaveTypeContainerDTO.NumberDaysRequestedUpTo


                });

            }


            if (AddLeaveTypeContainerDTO.IfAbsenceLongerThan == true)
            {
                LeaveTypeRestriction.Add(new LeaveTypeRestriction
                {
                    CreationDate = creationDate,
                    IsActive = true,
                    IsDeleted = false,
                    LeaveTypeFK = leaveType.LeaveTypeID,
                    LeaveTypeRestrictionTypeFK = (int)RestrictionType.IfAbsenceLongerThan,
                    RestrictionUnitTypeFK = AddLeaveTypeContainerDTO.DurationUnitID,
                    RestrictionUnit = AddLeaveTypeContainerDTO.NumberOfDayIfAbsenceLongerThan,
                    OtherRestrictionUnit = AddLeaveTypeContainerDTO.NumberOfDaysAllowedToMakeRequestInThePast,

                });

            }
            leaveTypeRestrictionBLL.AddRange(LeaveTypeRestriction);

        }

        public LeavesTypeFieldsDTO GetLeavesTypeFields(int LeaveTypeID)
        {
            LeaveType LeaveType = Find(x => x.LeaveTypeID == LeaveTypeID).FirstOrDefault();
            LeavesTypeFieldsDTO LeavesTypeFields = new LeavesTypeFieldsDTO();
            LeavesTypeFields.AttachemntIsRequired = LeaveType.IsAttachmentsRequired;
            LeavesTypeFields.HasAttachemnt = LeaveType.HasAttachments;
            List<Entities.LeaveTypeField> ListLeaveTypeFields = leaveTypeFieldsBLL.Find(x => x.LeaveTypeFK == LeaveType.LeaveTypeID).ToList();
            LeavesTypeFields.LeaveTypeField = new List<LeaveTypeFieldDTO>();
            foreach (var item in ListLeaveTypeFields)

                LeavesTypeFields.LeaveTypeField.Add(new LeaveTypeFieldDTO
                {
                    LeaveTypeFieldID = item.LeaveTypeFieldFK,
                    LeaveTypeFieldName = ((StaticEnums.LeaveTypeField)Enum.Parse(typeof(StaticEnums.LeaveTypeField), item.LeaveTypeFieldFK.ToString())).ToString(),
                    LeaveTypeFieldVisibilityName = ((StaticEnums.LeaveTypeFieldVisibility)Enum.Parse(typeof(StaticEnums.LeaveTypeFieldVisibility), item.LeaveTypeFieldsVisibilityFK.ToString())).ToString()
                });


            return LeavesTypeFields;

        }



        public void AdjustEntitlement(AdjustEntitlementDTO AdjustEntitlementDTO)
        {
            UserEntitlement userentitlement = userentitlementBLL.Find(x => x.LeaveTypeFK == AdjustEntitlementDTO.LeaveTypeFK).FirstOrDefault();
            userentitlement.EntitlementTotal =+ AdjustEntitlementDTO.EntitlementTotal;
            userentitlement.CreationDate = AdjustEntitlementDTO.CreationDate;


        }

        public bool IsMonthlyLeaveType(int LeaveTypeID)
        {
            LeaveTypeAccuralRule accuralRuleLeaveTypes = leaveTypeAccuralRuleBLL.Find(x => x.LeaveTypeFK.Value == LeaveTypeID && x.AccuralPeriodFK == (int)LeaveTypeAccuralPeriod.RepeatEveryMonthAt && x.IsActive == true).FirstOrDefault();

            if (accuralRuleLeaveTypes != null)
                return true;
            else
                return false;
        }

    }
}
