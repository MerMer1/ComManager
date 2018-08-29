using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComManager
{
    abstract class Observer
    {
        public abstract void Update(byte[] msg);
    }
}
