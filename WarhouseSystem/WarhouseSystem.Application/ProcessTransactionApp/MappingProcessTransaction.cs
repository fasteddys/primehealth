using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.ProcessTransactionApp
{
    public class MappingProcessTransaction
    {
        public ProcessTransaction MapModelToEntity(ProcessTransactionViewModel Model, ProcessTransaction Entity)
        {
            Entity.SubCategoryId = Model.SubCategoryId;
            Entity.AffectNumber = Model.AffectNumber;
            Entity.EmployeeId = Model.EmployeeId;
            Entity.IsDeleted = Model.IsDeleted;
            Entity.TransactionTypeId = Model.TransactionTypeId;
            Entity.CreationTime = Model.CreationDate;
            return Entity;
        }
        public ProcessTransaction MapModelToEntity(ProcessTransactionViewModel Model)
        {
            return new ProcessTransaction()
            {
                Id = Model.Id,
                SubCategoryId = Model.SubCategoryId,
                AffectNumber = Model.AffectNumber,
                EmployeeId = Model.EmployeeId,
                IsDeleted = false,
                TransactionTypeId = Model.TransactionTypeId,
                CreationTime = Model.CreationDate
            };
        }

        public ProcessTransactionViewModel MapEntityToModel(ProcessTransaction Entity)
        {
            return new ProcessTransactionViewModel()
            {
                Id = Entity.Id,
                SubCategoryId = Entity.SubCategoryId,
                SubCategoryName = Entity.SubCategory.Name,
                AffectNumber = Entity.AffectNumber,
                EmployeeId = Entity.EmployeeId,
                IsDeleted = Entity.IsDeleted,
                EmployeeName = Entity.Employee.Name,
                TransactionTypeId = Entity.TransactionTypeId,
                TransactionTypeName = Entity.TransactionType.Name,
                CreationDate = Entity.CreationTime,

            };
        }
    }
}
