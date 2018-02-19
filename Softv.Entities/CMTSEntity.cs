using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Softv.Entities
{
    [DataContract]
    [Serializable]
    public class CMTSDataEntity : BaseEntity
    {
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string TiempoActividad { get; set; }

        [DataMember]
        public int TotalModems { get; set; }

        [DataMember]
        public int EnLinea { get; set; }

        [DataMember]
        public int Apagados { get; set; }

        [DataMember]
        public int Suspendidos { get; set; }

        [DataMember]
        public int EnProceso { get; set; }

        [DataMember]
        public string ConsumoDatos { get; set; }

        [DataMember]
        public string CargaCPU { get; set; }
    }
}
