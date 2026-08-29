using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdAdapterLibrary.dto
{
    public class UserPropertiesDto
    {
        public string cn { get; set; }
        public string givenname { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string passwordOld { get; set; }
        public string pricipalname { get; set; }
    }

    public class GroupPropertiesDto
    {
        public string cn { get; set; }

    }
}
