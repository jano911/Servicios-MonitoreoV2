using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Softv.Entities
{
    [DataContract]
    [Serializable]
    public class CMTSEntity : BaseEntity
    {
        [DataMember]
        public int IdCMTS { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string Comunidad { get; set; }
        [DataMember]
        public string ComunidadCablemodem { get; set; }
        [DataMember]
        public int IdTipo { get; set; }
        [DataMember]
        public string Interface { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Enable { get; set; }
    }

    [DataContract]
    [Serializable]
    public class TipoCMTSEntity
    {
        [DataMember]
        public int IdTipo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
    }
}
