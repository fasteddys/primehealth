//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddAcccuralLeaveTypesForNewEmployees_WS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkingWeekDetail
    {
        public int WorkingWeekDetailsID { get; set; }
        public int WorkingWeekFK { get; set; }
        public int WeekDaysFK { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public decimal WorkingDuration { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
    
        public virtual WeekDaysDIM WeekDaysDIM { get; set; }
        public virtual WorkingWeek WorkingWeek { get; set; }
    }
}
