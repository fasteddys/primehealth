using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EmaTours.DAL.Factory;
using EmaTours.Entities;

namespace EmaTours.BLL.DbObjects
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
