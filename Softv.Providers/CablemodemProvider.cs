using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting;
using Softv.Entities;
using SoftvConfiguration;
using Globals;

namespace Softv.Providers
{
    public abstract class CablemodemProvider : Globals.DataAccess
    {
        private static CablemodemProvider _Instance = null;

        private static ObjectHandle obj;
        /// <summary>
        /// Generates a Role instance
        /// </summary>
        public static CablemodemProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    obj = Activator.CreateInstance(
                    SoftvSettings.Settings.Cablemodem.Assembly,
                    SoftvSettings.Settings.Cablemodem.DataClass);
                    _Instance = (CablemodemProvider)obj.Unwrap();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Provider's default constructor
        /// </summary>
        public CablemodemProvider()
        {
        }

        public abstract List<CablemodemEntity> GetListaCablemodem(int IdCMTS);

        public abstract List<ConsumoEntity> GetHistorialConsumo(string MAC);

        public abstract ClienteEntity GetDatosCliente(string MAC);

        public abstract ConsumoEntity GetConsumoActual(string MAC);

        public abstract ClienteEntity GetIPCliente(string MAC);

    }
}
