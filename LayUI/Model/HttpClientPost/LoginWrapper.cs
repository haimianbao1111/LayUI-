using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class LoginWrapper
    {
        [DataMember(Order = 0)]
        public LoginUser user { get; set; }
    }
}
