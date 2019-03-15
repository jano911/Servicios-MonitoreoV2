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
        public string Interface { get; set; }

        [DataMember]
        public string Contrato { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Servicio { get; set; }

    }

    [DataContract]
    [Serializable]
    public class ConsumoEntity 
    {
        [DataMember]
        public string Fecha { get; set; }

        [DataMember]
        public string Rx { get; set; }

        [DataMember]
        public string tx { get; set; }

    }

    [DataContract]
    [Serializable]
    public class ClienteEntity
    {

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int ultimo_mes { get; set; }


        [DataMember]
        public int ultimo_anio { get; set; }

        [DataMember]
        public string Contrato { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Servicio { get; set; }

        [DataMember]
        public string LimiteSubida { get; set; }

        [DataMember]
        public string LimiteBajada { get; set; }

        [DataMember]
        public string IP { get; set; }

    }
}
