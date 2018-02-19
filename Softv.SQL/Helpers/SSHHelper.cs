using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Softv.Entities;
using System.Data.SqlClient;
using System.Data;
using Renci.SshNet;
namespace Softv.DAO
{
    public class SSHHelper
    {
        private ConnectionInfo ConnNfo;

        public SSHHelper(int IdCMTS)
        {
            CMTSEntity cmts = new CMTSEntity();
            DBHelper db = new DBHelper();
            db.agregarParametro("@IdCMTS", SqlDbType.Int, IdCMTS);
            SqlDataReader reader = db.consultaReader("ObtieneCMTSPorId");
            cmts = db.MapDataToEntityCollection<CMTSEntity>(reader).FirstOrDefault();
            reader.Close();
            db.cierraConexion();

            ConnNfo = new ConnectionInfo(cmts.IP, 22, cmts.Usuario,
                new AuthenticationMethod[]{
                // Pasword based Authentication
                new PasswordAuthenticationMethod(cmts.Usuario, cmts.Password),
            });
        }

        public string EjecutaComando(string Comando)
        {
            string salida = "";
            using (var sshclient = new SshClient(ConnNfo))
            {
                sshclient.Connect();
                // quick way to use ist, but not best practice - SshCommand is not Disposed, ExitStatus not checked...
                salida = sshclient.CreateCommand(Comando).Execute();

                sshclient.Disconnect();
            }
            return salida;
        }
    }
}
