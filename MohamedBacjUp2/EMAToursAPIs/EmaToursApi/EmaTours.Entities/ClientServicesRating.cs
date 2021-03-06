//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmaTours.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientServicesRating
    {
        public int ClientServiceRatingID { get; set; }
        public Nullable<int> ClientFK { get; set; }
        public Nullable<int> ClientVisitFK { get; set; }
        public Nullable<int> ServiceFK { get; set; }
        public Nullable<decimal> RatingScale { get; set; }
        public string ClientCommentPerService { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ClientVisit ClientVisit { get; set; }
        public virtual EMAService EMAService { get; set; }
    }
}
