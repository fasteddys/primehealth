using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WarhouseSystem.DB.Models
{
   public class StatusType: Base
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
