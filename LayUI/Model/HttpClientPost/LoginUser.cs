using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class LoginUser
    {
        [DataMember(Order = 0)]
        public string loginId { get; set; }
        [DataMember(Order = 1)]
        public string password { get; set; }
    }
}
