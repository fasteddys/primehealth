using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.DAL.Factory
{
   public class ContainerContextFactory : IFactory<EMAToursEntities> ,IDisposable
    {
        protected EMAToursEntities context;

        public ContainerContextFactory()
        {
            context = Create();
        }
 
        public EMAToursEntities Create() => context != null ? context : new EMAToursEntities();


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
