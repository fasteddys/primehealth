using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasksMonitoringSystem.StaticData
{
    public class StaticEnums
    {
        public enum Company
        {
            PrimeHealth = 1,
            MedGulf = 2

        }
        public enum TaskStatus
        {
            Pending = 1,
            Assign = 2,
            Completed=3
               
        }
        public enum PriorityType
        {
            Normal = 1,
            Medium = 2,
            High = 3,

        }
     
    }
}