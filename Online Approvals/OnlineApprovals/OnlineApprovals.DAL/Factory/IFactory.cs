using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DAL.Factory
{
    interface IFactory<T>
    {
        T Create();
    }
}
