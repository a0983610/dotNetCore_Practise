using NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.ViewModels
{
    public class CURDViewModel
    {
        public Guid ID { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string Tel { set; get; }
        public string Gender { set; get; }
        public DateTime Birthday { set; get; }

        public List<Member2> DataList { set; get; }

        public string Url { set; get; }
    }
}
