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

namespace Softv.DAO
{
    public class CMTSData : CMTSProvider
    {
        public override CMTSDataEntity GetCMTSDatos(int IdCMTS)
        {
            CMTSDataEntity result = new CMTSDataEntity();

            result.Apagados = 13;
            result.CargaCPU = "15 %";
            result.ConsumoDatos = "Bajada 399.91 Mbps / Subida 29.32 Mbps";
            result.Descripcion = "Cisco IOS Software, 1000000 Software Version 2.1";
            result.EnLinea = 50;
            result.EnProceso = 5;
            result.Suspendidos = 16;
            result.TiempoActividad = "5 dias 14 horas";
            result.TotalModems = 84;
            
            return result;
        }
    }
}
