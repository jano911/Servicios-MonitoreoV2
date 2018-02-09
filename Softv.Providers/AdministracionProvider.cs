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
    public abstract class AdministracionProvider : Globals.DataAccess
    {
        private static AdministracionProvider _Instance = null;

        private static ObjectHandle obj;
        /// <summary>
        /// Generates a Role instance
        /// </summary>
        public static AdministracionProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    obj = Activator.CreateInstance(
                    SoftvSettings.Settings.Administracion.Assembly,
                    SoftvSettings.Settings.Administracion.DataClass);
                    _Instance = (AdministracionProvider)obj.Unwrap();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Provider's default constructor
        /// </summary>
        public AdministracionProvider()
        {
        }

        public abstract List<CMTSEntity> GetCMTSLista();

        public abstract List<TipoCMTSEntity> GetTipoCMTS();

        public abstract int GetNuevoCMTS(string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable);

        public abstract CMTSEntity GetCMTSPorId(int IdCMTS);

        public abstract int GetEditaCMTS(int IdCMTS, string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable);

        public abstract int GetEliminaCMTS(int IdCMTS);
    }
}
