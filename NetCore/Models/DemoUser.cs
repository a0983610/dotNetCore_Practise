using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Models
{
    public class DemoUser
    {
        public string Name { set; get; }
        public PhoneNumber PhoneNumber { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public int Age { set; get; }
        public bool IsActive { set; get; }

        
    }

    public class PhoneNumber
    {
        public string Tel { set; get; }
        public string Phone { set; get; }
    }
}
