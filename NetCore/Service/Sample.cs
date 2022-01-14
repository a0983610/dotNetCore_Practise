using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Service
{
    public class Sample : ISingle, IScoped, ITransient
    {
        private static int _count;
        private int _id;
        public Sample()
        {
            this._id = ++_count;
        }
        public int Id => _id;
    }
}
