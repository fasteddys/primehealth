using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;
namespace WarhouseSystem.Application.TransactionTypeApp
{
   public class MappingTransactionType
    {
        public TransactionType MapModelToEntity(LockupViewModel Model, TransactionType Entity)
        {
            Entity.Name = Model.Name;
            return Entity;
        }
        public TransactionType MapModelToEntity(LockupViewModel Model)
        {
            return new TransactionType()
            {
                Id = Model.Id,
                Name = Model.Name
            };
        }

        public LockupViewModel MapEntityToModel(TransactionType Entity)
        {
            return new LockupViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name
            };
        }
    }
}
