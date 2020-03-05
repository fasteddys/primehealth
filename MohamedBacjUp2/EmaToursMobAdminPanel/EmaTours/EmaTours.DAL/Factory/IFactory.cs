﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DAL.Factory
{
    interface IFactory<T>
    {
        T Create();
    }
}
