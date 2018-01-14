using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActor.Interfaces
{
    [System.Runtime.Serialization.DataContract]
    public class UserStatus
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int Age { get; set; }

    }
}
