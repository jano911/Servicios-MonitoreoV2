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
using System.IO;
using System.Text.RegularExpressions;
using Softv.SQL.Helpers;
namespace Softv.DAO
{
    public class CablemodemData : CablemodemProvider
    {
        public override List<CablemodemEntity> GetListaCablemodem(int IdCMTS)
        {
            List<CablemodemEntity> result = new List<CablemodemEntity>();
            try
            {
                DBHelper db = new DBHelper();
                SqlDataReader reader = db.consultaReader("DameListadoMac");
                result = db.MapDataToEntityCollection<CablemodemEntity>(reader).ToList();
                reader.Close();
                db.cierraConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetListaCablemodem " + ex.Message, ex);
            }
            return result;
        }

        public override List<ConsumoEntity> GetHistorialConsumo(string MAC)
        {
            List<ConsumoEntity> result = new List<ConsumoEntity>();
            try
            {
                DBHelper db = new DBHelper();
                db.agregarParametro("@mac", SqlDbType.VarChar, MAC);
                SqlDataReader reader = db.consultaReader("DameDatosAnchoBandaHistoricos");
                result = db.MapDataToEntityCollection<ConsumoEntity>(reader).ToList();
                reader.Close();
                db.cierraConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetHistorialConsumo " + ex.Message, ex);
            }
            return result;
        }

        public override ClienteEntity GetDatosCliente(string MAC)
        {
            ClienteEntity result = new ClienteEntity();
            try
            {
                DBHelper db = new DBHelper();
                db.agregarParametro("@mac", SqlDbType.VarChar, MAC);
                SqlDataReader reader = db.consultaReader("DameDatosCliente");
                result = db.MapDataToEntityCollection<ClienteEntity>(reader).FirstOrDefault();
                reader.Close();
                db.cierraConexion();

            }
            catch (Exception ex)
            {
                throw new Exception("Error GetHistorialConsumo " + ex.Message, ex);
            }
            return result;
        }

        public override ConsumoEntity GetConsumoActual(string MAC)
        {
            ConsumoEntity result = new ConsumoEntity();
            try
            {
                //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                result.Fecha = "5";// unixTimestamp.ToString();
                Random r = new Random();
                result.Rx = r.Next(0, 1000).ToString();
                result.tx = r.Next(0, 1000).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetConsumoActual " + ex.Message, ex);
            }
            return result;
        }

        public override ClienteEntity GetIPCliente(string MAC)
        {
            ClienteEntity result = new ClienteEntity();
            try
            {
                result.IP = "132";
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetIPCliente " + ex.Message, ex);
            }
            return result;
        }
    }
}
