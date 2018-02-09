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
    public class CablemodemData : CablemodemProvider
    {
        public override List<CablemodemEntity> GetListaCablemodem()
        {
            List<CablemodemEntity> result = new List<CablemodemEntity>();
            string contents = File.ReadAllText(@"C:\Users\Jano\Downloads\Monitoreo\salida.txt");
            //contents = contents.Replace(Environment.NewLine, "+");
            contents = Regex.Replace(contents, @"\r\n?|\n", "+");
            string[] lineas = contents.Split('+');
            int cont = 0;
            foreach(string linea in lineas)
            {
                string[] valores = linea.Split(' ');
                if (valores.Count() > 9)
                {
                    cont = 0;
                    CablemodemEntity cablemodemAux = new CablemodemEntity();
                    foreach (string valor in valores)
                    {
                        if (valor.Length > 0)
                        {
                            cont++;
                            switch (cont)
                            {
                                case 1:
                                    cablemodemAux.MAC = valor;
                                    break;
                                case 2:
                                    cablemodemAux.IP = valor;
                                    break;
                                case 4:
                                    cablemodemAux.Puerto = valor;
                                    break;
                                case 5:
                                    cablemodemAux.Status = valor;
                                    break;
                                case 7:
                                    cablemodemAux.RxPwr = valor;
                                    break;
                                case 10:
                                    cablemodemAux.Activo = valor;
                                    break;
                            }
                        }
                    }
                    result.Add(cablemodemAux);
                }
            }
            return result;
        }
    }
}
