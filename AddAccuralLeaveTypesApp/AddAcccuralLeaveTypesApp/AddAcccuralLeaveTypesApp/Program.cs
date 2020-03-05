using AddAcccuralLeaveTypesApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LeaveTypesBLL leaveTypesBLL = new LeaveTypesBLL();

            try
            {
                leaveTypesBLL.WriteToFile("App starts on " + DateTime.Now);
                leaveTypesBLL.AddAccuralLeaveTypesForEmployees();
            }
            catch (Exception ex)
            {
                leaveTypesBLL.WriteToFile("AddAccuralLeaveTypesForEmployees Error " + ex.Message);
                throw;
            }
            try
            {
                leaveTypesBLL.AddAccuralLeaveTypesForNewEmployees();
                leaveTypesBLL.WriteToFile("App ends on " + DateTime.Now);
            }
            catch (Exception ex)
            {
                leaveTypesBLL.WriteToFile("AddAccuralLeaveTypesForNewEmployees Error " + ex.Message);
                throw;
            }
            
        }
    }
}
