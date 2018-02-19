using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnmpSharpNet;
using Softv.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Net;
namespace Softv.DAO
{
    public class SNMPHelper
    {
        private CMTSEntity cmts;
        private Dictionary<string, string> oids;

        public SNMPHelper(int IdCMTS)
        {
            cmts = new CMTSEntity();
            DBHelper db = new DBHelper();
            db.agregarParametro("@IdCMTS", SqlDbType.Int, IdCMTS);
            SqlDataReader reader = db.consultaReader("ObtieneCMTSPorId");
            cmts = db.MapDataToEntityCollection<CMTSEntity>(reader).FirstOrDefault();
            reader.Close();
            db.cierraConexion();
            //Llenamos las oids
            oids = new Dictionary<string, string>();
            oids.Add("SNR", "1.2.3");
        }

        public string EjecutaComando(string valor, bool CMTS, string IP)
        {
            string oid = oids[valor];
            var salida = "";
            //Si el valor que se desea obtener es de un CMTS o es un cablemodem
            if (CMTS)
            {
                // SNMP community name
                OctetString community = new OctetString(cmts.Comunidad);

                // Define agent parameters class
                AgentParameters param = new AgentParameters(community);
                // Set SNMP version to 1 (or 2)
                param.Version = SnmpVersion.Ver1;
                // Construct the agent address object
                // IpAddress class is easy to use here because
                //  it will try to resolve constructor parameter if it doesn't
                //  parse to an IP address
                IpAddress agent = new IpAddress(cmts.IP);

                // Construct target
                UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

                // Pdu class used for all requests
                Pdu pdu = new Pdu(PduType.Get);
                pdu.VbList.Add(oid); 

                // Make SNMP request
                SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);

                // If result is null then agent didn't reply or we couldn't parse the reply.
                if (result != null)
                {
                    // ErrorStatus other then 0 is an error returned by 
                    // the Agent - see SnmpConstants for error definitions
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        // agent reported an error with the request
                        //Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        //    result.Pdu.ErrorStatus,
                        //    result.Pdu.ErrorIndex);
                        salida = result.Pdu.ErrorStatus.ToString();
                    }
                    else
                    {
                        // Reply variables are returned in the same order as they were added
                        //  to the VbList
                        //Console.WriteLine("sysDescr({0}) ({1}): {2}",
                        //    result.Pdu.VbList[0].Oid.ToString(),
                        //    SnmpConstants.GetTypeName(result.Pdu.VbList[0].Value.Type),
                        //    result.Pdu.VbList[0].Value.ToString());
                        salida = result.Pdu.VbList[0].Value.ToString();
                    }
                }
                else
                {
                    //Console.WriteLine("No response received from SNMP agent.");
                    salida = "---";
                }
                target.Close();
            }
            else
            {
                // SNMP community name
                OctetString community = new OctetString(cmts.ComunidadCablemodem);

                // Define agent parameters class
                AgentParameters param = new AgentParameters(community);
                // Set SNMP version to 1 (or 2)
                param.Version = SnmpVersion.Ver1;
                // Construct the agent address object
                // IpAddress class is easy to use here because
                //  it will try to resolve constructor parameter if it doesn't
                //  parse to an IP address
                IpAddress agent = new IpAddress(IP);

                // Construct target
                UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

                // Pdu class used for all requests
                Pdu pdu = new Pdu(PduType.Get);
                pdu.VbList.Add(oid);

                // Make SNMP request
                SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);

                // If result is null then agent didn't reply or we couldn't parse the reply.
                if (result != null)
                {
                    // ErrorStatus other then 0 is an error returned by 
                    // the Agent - see SnmpConstants for error definitions
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        // agent reported an error with the request
                        //Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        //    result.Pdu.ErrorStatus,
                        //    result.Pdu.ErrorIndex);
                        salida = result.Pdu.ErrorStatus.ToString();
                    }
                    else
                    {
                        // Reply variables are returned in the same order as they were added
                        //  to the VbList
                        //Console.WriteLine("sysDescr({0}) ({1}): {2}",
                        //    result.Pdu.VbList[0].Oid.ToString(),
                        //    SnmpConstants.GetTypeName(result.Pdu.VbList[0].Value.Type),
                        //    result.Pdu.VbList[0].Value.ToString());
                        salida = result.Pdu.VbList[0].Value.ToString();
                    }
                }
                else
                {
                    //Console.WriteLine("No response received from SNMP agent.");
                    salida = "---";
                }
                target.Close();
            }
            return salida;
        }
    }
}
