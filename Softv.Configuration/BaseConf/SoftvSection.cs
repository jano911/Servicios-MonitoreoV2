using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftvConfiguration
{
    public class SoftvSection : ConfigurationSection
    {
        /// <summary>
        /// Gets default connection String. If it doesn't exist then
        /// returns global connection string
        /// </summary>
        [ConfigurationProperty("DefaultConnectionString")]
        public String ConnectionString
        {
            get
            {
                string connectionString = (string)base["DefaultConnectionString"];
                connectionString = String.IsNullOrEmpty(connectionString) ?
                    Globals.DataAccess.GlobalConectionString :
                    (string)base["DefaultConnectionString"];
                return connectionString;
            }
        }

        /// <summary>
        /// Gets default assembly name for TestSimulator data clases
        /// </summary>
        [ConfigurationProperty("DefaultAssembly", DefaultValue = "Softv.SQL")]
        public String Assembly
        {
            get { return (string)base["DefaultAssembly"]; }
        }
        /// <summary>
        /// Gets Usuario configuration data
        /// </summary>
        [ConfigurationProperty("Usuario")]
        public UsuarioElement Usuario
        {
            get { return (UsuarioElement)base["Usuario"]; }
        }
        /// <summary>
        /// Gets Role configuration data
        /// </summary>
        [ConfigurationProperty("Role")]
        public RoleElement Role
        {
            get { return (RoleElement)base["Role"]; }
        }

        /// <summary>
        /// Gets Permiso configuration data
        /// </summary>
        [ConfigurationProperty("Permiso")]
        public PermisoElement Permiso
        {
            get { return (PermisoElement)base["Permiso"]; }
        }
        /// <summary>
        /// Gets Module configuration data
        /// </summary>
        [ConfigurationProperty("Module")]
        public ModuleElement Module
        {
            get { return (ModuleElement)base["Module"]; }
        }

        /// <summary>
        /// Gets Secutity configuration data
        /// </summary>
        [ConfigurationProperty("Secutity")]
        public SecutityElement Secutity
        {
            get { return (SecutityElement)base["Secutity"]; }
        }

        /// <summary>
        /// Gets Session configuration data
        /// </summary>
        [ConfigurationProperty("Session")]
        public SessionElement Session
        {
            get { return (SessionElement)base["Session"]; }
        }

        /// <summary>
        /// Gets Session configuration data
        /// </summary>
        [ConfigurationProperty("Administracion")]
        public AdministracionElement Administracion
        {
            get { return (AdministracionElement)base["Administracion"]; }
        }

        /// <summary>
        /// Gets Session configuration data
        /// </summary>
        [ConfigurationProperty("Cablemodem")]
        public CablemodemElement Cablemodem
        {
            get { return (CablemodemElement)base["Cablemodem"]; }
        }











        /// <summary>
        /// Gets Terminal configuration data
        /// </summary>
        [ConfigurationProperty("Terminal")]
        public TerminalElement Terminal
        {
            get { return (TerminalElement)base["Terminal"]; }
        }
        

        /// <summary>
        /// Gets UsuarioSystem configuration data
        /// </summary>
        [ConfigurationProperty("UsuarioSystem")]
        public UsuarioSystemElement UsuarioSystem
        {
            get { return (UsuarioSystemElement)base["UsuarioSystem"]; }
        }

        /// <summary>
        /// Gets UsuarioSystem configuration data
        /// </summary>
        [ConfigurationProperty("CMTS")]
        public CMTSElement CMTS
        {
            get { return (CMTSElement)base["CMTS"]; }
        }




    }
}
