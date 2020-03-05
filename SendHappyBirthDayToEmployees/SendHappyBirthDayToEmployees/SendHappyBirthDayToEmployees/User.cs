//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SendHappyBirthDayToEmployees
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<bool> IsOnline { get; set; }
        public string LDAPUserName { get; set; }
        public Nullable<int> SubDepartmentFK { get; set; }
        public Nullable<int> DepartmentFK { get; set; }
        public Nullable<int> AccessControlUserFK { get; set; }
        public Nullable<int> DirectManagerFK { get; set; }
        public Nullable<int> PositionFK { get; set; }
        public Nullable<int> ApprovalFlowFK { get; set; }
        public Nullable<int> ContractTypeFK { get; set; }
        public Nullable<int> LevelFK { get; set; }
        public string ProfilePictureURL { get; set; }
        public Nullable<System.DateTime> ProbationDate { get; set; }
        public Nullable<System.DateTime> TerminationDate { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> SeniorityBeforeHireYears { get; set; }
        public Nullable<int> GenderFK { get; set; }
        public Nullable<int> SeniorityBeforeHireMonth { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomNote { get; set; }
        public string ArName { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> IsHR { get; set; }
        public Nullable<int> WorkingWeekFK { get; set; }
        public string PrivatePhoneNumber { get; set; }
        public string NationalID { get; set; }
        public string HomeAddress { get; set; }
        public string Address { get; set; }
        public string MedicalID { get; set; }
        public Nullable<bool> IsFirstLogin { get; set; }
        public Nullable<bool> HasCompletedUserProfileData { get; set; }
        public string UserPersonalEmail { get; set; }
        public Nullable<int> CompanyFK { get; set; }
        public string ExtNumber { get; set; }
        public Nullable<bool> IsOutDomainUser { get; set; }
        public string OutDomainPassword { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual User Users1 { get; set; }
        public virtual User User1 { get; set; }
    }
}
