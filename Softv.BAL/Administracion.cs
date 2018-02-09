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
    public class Administracion
    {
        #region Constructors
        public Administracion() { }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<CMTSEntity> GetCMTSLista()
        {
            List<CMTSEntity> entities = new List<CMTSEntity>();
            entities = ProviderSoftv.Administracion.GetCMTSLista();

            return entities ?? new List<CMTSEntity>();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<TipoCMTSEntity> GetTipoCMTS()
        {
            List<TipoCMTSEntity> entities = new List<TipoCMTSEntity>();
            entities = ProviderSoftv.Administracion.GetTipoCMTS();

            return entities ?? new List<TipoCMTSEntity>();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int GetNuevoCMTS(string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable)
        {
            int entities = 0;
            entities = ProviderSoftv.Administracion.GetNuevoCMTS(Nombre, IP, Comunidad, ComunidadCablemodem, IdTipo, interfaceS, Usuario, PasswordS, Enable);

            return entities;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CMTSEntity GetCMTSPorId(int IdCMTS)
        {
            CMTSEntity entities = new CMTSEntity();
            entities = ProviderSoftv.Administracion.GetCMTSPorId(IdCMTS);

            return entities ?? new CMTSEntity();

        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int GetEditaCMTS(int IdCMTS, string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable)
        {
            int entities = 0;
            entities = ProviderSoftv.Administracion.GetEditaCMTS(IdCMTS, Nombre, IP, Comunidad, ComunidadCablemodem, IdTipo, interfaceS, Usuario, PasswordS, Enable);

            return entities;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int GetEliminaCMTS(int IdCMTS)
        {
            int entities = 0;
            entities = ProviderSoftv.Administracion.GetEliminaCMTS(IdCMTS);

            return entities;
        }
    }
}
