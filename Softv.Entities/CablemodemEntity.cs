using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Softv.Entities
{
    [DataContract]
    [Serializable]
    public class CablemodemEntity : BaseEntity
    {
        [DataMember]
        public string MAC { get; set; }

        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public string Puerto { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string RxPwr { get; set; }

        [DataMember]
        public string Activo { get; set; }
    }
}
