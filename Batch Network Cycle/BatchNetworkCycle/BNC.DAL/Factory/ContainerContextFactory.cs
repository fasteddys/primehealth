using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.Entities;

namespace BNC.DAL.Factory
{
   public class ContainerContextFactory : IFactory<BNCEntities> ,IDisposable
    {
        protected BNCEntities context;

        public ContainerContextFactory()
        {
            context = Create();
            context.Configuration.LazyLoadingEnabled = false;

        }

        public BNCEntities Create() => context != null ? context : new BNCEntities();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
