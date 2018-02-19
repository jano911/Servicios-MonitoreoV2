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
    public abstract class CMTSProvider : Globals.DataAccess
    {
        private static CMTSProvider _Instance = null;

        private static ObjectHandle obj;
        /// <summary>
        /// Generates a Role instance
        /// </summary>
        public static CMTSProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    obj = Activator.CreateInstance(
                    SoftvSettings.Settings.CMTS.Assembly,
                    SoftvSettings.Settings.CMTS.DataClass);
                    _Instance = (CMTSProvider)obj.Unwrap();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Provider's default constructor
        /// </summary>
        public CMTSProvider()
        {
        }

        public abstract CMTSDataEntity GetCMTSDatos(int IdCMTS);

    }
}
