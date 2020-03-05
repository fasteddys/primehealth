using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterSystemReports.DAL.Factory
{
    interface IFactory<T>
    {
        T Create();
    }
}
