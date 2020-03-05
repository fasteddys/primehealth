using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TasksMonitoringSystem
{
    [MetadataType(typeof(TaskMetaData))]
    public partial class Task
    {

    }
    public class TaskMetaData
    {
        [Required(ErrorMessage = "Task Name  is required")]
        public string TaskName { get; set; }
        [Required(ErrorMessage = "Task Description  is required")]
        public string Description { get; set; }
  

    

    }
}