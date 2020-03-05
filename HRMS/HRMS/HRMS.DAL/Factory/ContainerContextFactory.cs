using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;

namespace HRMS.DAL.Factory
{
   public class ContainerContextFactory : IFactory<HRMSEntities> ,IDisposable
    {
        protected HRMSEntities context;

        public ContainerContextFactory()
        {
            context = Create();
        }
 
        public HRMSEntities Create() => context != null ? context : new HRMSEntities();


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
