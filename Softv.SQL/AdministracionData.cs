using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Softv.Entities;
using Softv.Providers;
using SoftvConfiguration;
using Globals;

namespace Softv.DAO
{
    public class AdministracionData : AdministracionProvider
    {
        public override List<CMTSEntity> GetCMTSLista()
        {
            List<CMTSEntity> result = new List<CMTSEntity>();
            DBHelper db = new DBHelper();
            SqlDataReader reader = db.consultaReader("ObtieneCMTS");
            result = db.MapDataToEntityCollection<CMTSEntity>(reader).ToList();
            reader.Close();
            db.cierraConexion();
            return result;
        }

        public override List<TipoCMTSEntity> GetTipoCMTS()
        {
            List<TipoCMTSEntity> result = new List<TipoCMTSEntity>();
            DBHelper db = new DBHelper();
            SqlDataReader reader = db.consultaReader("ObtieneTipoCMTS");
            result = db.MapDataToEntityCollection<TipoCMTSEntity>(reader).ToList();
            reader.Close();
            db.cierraConexion();
            return result;
        }

        public override int GetNuevoCMTS(string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable)
        {
            int result = 0;
            DBHelper db = new DBHelper();
            db.agregarParametro("@Nombre", SqlDbType.VarChar, Nombre);
            db.agregarParametro("@IP", SqlDbType.VarChar, IP);
            db.agregarParametro("@Comunidad", SqlDbType.VarChar, Comunidad);
            db.agregarParametro("@ComunidadCablemodem", SqlDbType.VarChar, ComunidadCablemodem);
            db.agregarParametro("@IdTipo", SqlDbType.Int, IdTipo);
            db.agregarParametro("@interface", SqlDbType.VarChar, interfaceS);
            db.agregarParametro("@Usuario", SqlDbType.VarChar, Usuario);
            db.agregarParametro("@Password", SqlDbType.VarChar, PasswordS);
            db.agregarParametro("@Enable", SqlDbType.VarChar, Enable);
            db.consultaSinRetorno("NuevoCMTS");
            return result;
        }

        public override CMTSEntity GetCMTSPorId(int IdCMTS)
        {
            CMTSEntity result = new CMTSEntity();
            DBHelper db = new DBHelper();
            db.agregarParametro("@IdCMTS", SqlDbType.Int, IdCMTS);
            SqlDataReader reader = db.consultaReader("ObtieneCMTSPorId");
            result = db.MapDataToEntityCollection<CMTSEntity>(reader).FirstOrDefault();
            reader.Close();
            db.cierraConexion();
            return result;
        }

        public override int GetEditaCMTS(int IdCMTS, string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable)
        {
            int result = 0;
            DBHelper db = new DBHelper();
            db.agregarParametro("@IdCMTS", SqlDbType.Int, IdCMTS);
            db.agregarParametro("@Nombre", SqlDbType.VarChar, Nombre);
            db.agregarParametro("@IP", SqlDbType.VarChar, IP);
            db.agregarParametro("@Comunidad", SqlDbType.VarChar, Comunidad);
            db.agregarParametro("@ComunidadCablemodem", SqlDbType.VarChar, ComunidadCablemodem);
            db.agregarParametro("@IdTipo", SqlDbType.Int, IdTipo);
            db.agregarParametro("@interface", SqlDbType.VarChar, interfaceS);
            db.agregarParametro("@Usuario", SqlDbType.VarChar, Usuario);
            db.agregarParametro("@Password", SqlDbType.VarChar, PasswordS);
            db.agregarParametro("@Enable", SqlDbType.VarChar, Enable);
            db.consultaSinRetorno("EditaCMTS");
            return result;
        }

        public override int GetEliminaCMTS(int IdCMTS)
        {
            int result = 0;
            DBHelper db = new DBHelper();
            db.agregarParametro("@IdCMTS", SqlDbType.Int, IdCMTS);
            db.consultaSinRetorno("EliminaCMTS");
            return result;
        }
    }
}
