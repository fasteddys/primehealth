namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WarhouseSystem.Common.Enums;
    using WarhouseSystem.DB.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WarhouseSystem.DB.Models.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WarhouseSystem.DB.Models.DB context)
        {
            LoadStatusTypes(context);

            LoadEmployeeTypes(context);

            LoadTransactionTypes(context);

            LoadCompanies(context);
        }

        public void LoadStatusTypes(WarhouseSystem.DB.Models.DB context)
        {
            foreach (StatusTypeEnum statusType in Enum.GetValues(typeof(StatusTypeEnum)))
            {
                if (!context.StatusTypes.Any(x => x.Name == statusType.ToString()))
                {
                    int keyEnum = (int)statusType;
                    context.StatusTypes.AddOrUpdate(
                  new StatusType() { Name = statusType.ToString(), Id = keyEnum, DeletionTime=null, CreationTime=DateTime.Now, IsDeleted=false, IsActive=true }
                       );
                }

                context.SaveChanges();
            }

            base.Seed(context);
        }

        public void LoadCompanies(WarhouseSystem.DB.Models.DB context)
        {
            foreach (CompanyEnum company in Enum.GetValues(typeof(CompanyEnum)))
            {
                if (!context.Companies.Any(x => x.Name == company.ToString()))
                {
                    int keyEnum = (int)company;
                    context.Companies.AddOrUpdate(
                  new Company() { Name = company.ToString(), Id = keyEnum, DeletionTime = null, CreationTime = DateTime.Now, IsDeleted = false, IsActive = true }
                       );
                }

                context.SaveChanges();
            }

            base.Seed(context);
        }

        public void LoadTransactionTypes(WarhouseSystem.DB.Models.DB context)
        {
            foreach (TransactionTypeEnum statusType in Enum.GetValues(typeof(TransactionTypeEnum)))
            {
                if (!context.TransactionTypes.Any(x => x.Name == statusType.ToString()))
                {
                    int keyEnum = (int)statusType;
                    context.TransactionTypes.AddOrUpdate(
                  new TransactionType() { Name = statusType.ToString(), Id = keyEnum, DeletionTime = null, CreationTime = DateTime.Now, IsDeleted = false, IsActive = true }
                       );
                }

                context.SaveChanges();
            }

            base.Seed(context);
        }

        public void LoadEmployeeTypes(WarhouseSystem.DB.Models.DB context)
        {
            foreach (EmployeeRoleEnum employeeRole in Enum.GetValues(typeof(EmployeeRoleEnum)))
            {
                if (!context.EmployeeRoles.Any(x => x.Name == employeeRole.ToString()))
                {
                    int keyEnum = (int)employeeRole;
                    context.EmployeeRoles.AddOrUpdate(
                  new EmployeeRole() { Name = employeeRole.ToString(), Id = keyEnum, DeletionTime = null, CreationTime = DateTime.Now, IsDeleted = false, IsActive = true }
                       );
                }

                context.SaveChanges();
            }

            base.Seed(context);
        }
    }
}
