using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BNC.DAL.Factory;
using BNC.Entities;

namespace BNC.BLL.DbObjects
{
    public sealed class StoredFunctions : ContainerContextFactory, IDisposable
    {
        public int LanguageId { get; set; }

        #region Constructors

        public StoredFunctions()
        {

        }

        public StoredFunctions(int lang)
        {
            LanguageId = lang;
        }

      
      

        #endregion
    }
}
