using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestaurantService
{
    [Serializable]
    [DataContract]
    public class Method
    {
        [DataMember] 
        public string Name { get; set; }
        [DataMember]
        public string Returns { get; set; }
    }
}