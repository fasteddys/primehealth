﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DispatchingMVCVer1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DisPatchingDBEntities : DbContext
    {
        public DisPatchingDBEntities()
            : base("name=DisPatchingDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }
        public virtual DbSet<StockRequest> StockRequests { get; set; }
        public virtual DbSet<StockItemsDetail> StockItemsDetails { get; set; }
    
        public virtual ObjectResult<GetAllEmployees_Result> GetAllEmployees(Nullable<int> iD_Param, string name_param)
        {
            var iD_ParamParameter = iD_Param.HasValue ?
                new ObjectParameter("ID_Param", iD_Param) :
                new ObjectParameter("ID_Param", typeof(int));
    
            var name_paramParameter = name_param != null ?
                new ObjectParameter("Name_param", name_param) :
                new ObjectParameter("Name_param", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllEmployees_Result>("GetAllEmployees", iD_ParamParameter, name_paramParameter);
        }
    
        public virtual int InserEmpData(string name_param, string pass_param, string type_param)
        {
            var name_paramParameter = name_param != null ?
                new ObjectParameter("Name_param", name_param) :
                new ObjectParameter("Name_param", typeof(string));
    
            var pass_paramParameter = pass_param != null ?
                new ObjectParameter("Pass_param", pass_param) :
                new ObjectParameter("Pass_param", typeof(string));
    
            var type_paramParameter = type_param != null ?
                new ObjectParameter("type_param", type_param) :
                new ObjectParameter("type_param", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InserEmpData", name_paramParameter, pass_paramParameter, type_paramParameter);
        }
    }
}