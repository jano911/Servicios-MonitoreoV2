using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Linq;
using Softv.Providers;
using Softv.Entities;
using Globals;

namespace Softv.BAL
{
    [DataObject]
    [Serializable]
    public class Cablemodem
    {
        #region Constructors
        public Cablemodem() { }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<CablemodemEntity> GetListaCablemodem(int IdCMTS)
        {
            List<CablemodemEntity> entities = new List<CablemodemEntity>();
            entities = ProviderSoftv.Cablemodem.GetListaCablemodem(IdCMTS);

            return entities ?? new List<CablemodemEntity>();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<ConsumoEntity>  GetHistorialConsumo(string MAC)
        {
            List<ConsumoEntity> entities = new List<ConsumoEntity>();
            entities = ProviderSoftv.Cablemodem.GetHistorialConsumo(MAC);

            return entities ?? new List<ConsumoEntity>();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ClienteEntity GetDatosCliente(string MAC)
        {
            ClienteEntity entities = new ClienteEntity();
            entities = ProviderSoftv.Cablemodem.GetDatosCliente(MAC);

            return entities ?? new ClienteEntity();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ConsumoEntity GetConsumoActual(string MAC)
        {
            ConsumoEntity entities = new ConsumoEntity();
            entities = ProviderSoftv.Cablemodem.GetConsumoActual(MAC);

            return entities ?? new ConsumoEntity();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ClienteEntity GetIPCliente(string MAC)
        {
            ClienteEntity entities = new ClienteEntity();
            entities = ProviderSoftv.Cablemodem.GetIPCliente(MAC);

            return entities ?? new ClienteEntity();
        }
    }
}
