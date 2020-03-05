using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineApprovals.Entities;

namespace OnlineApprovals.DAL.Factory
{
   public class ContainerContextFactory : IFactory<PhNetworkEntities> ,IDisposable
    {
        protected PhNetworkEntities context;

        public ContainerContextFactory()
        {
            context = Create();
        }
 
        public PhNetworkEntities Create() => context != null ? context : new PhNetworkEntities();


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
