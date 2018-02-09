using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class AdministracionElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Secutity class
        /// </summary>
        [ConfigurationProperty("Assembly")]
        public String Assembly
        {
            get
            {
                string assembly = (string)base["Assembly"];
                assembly = String.IsNullOrEmpty(assembly) ?
                SoftvSettings.Settings.Assembly :
                (string)base["Assembly"];
                return assembly;
            }
        }

        /// <summary>
        /// Gets class name for Secutity
        ///</summary>
        [ConfigurationProperty("DataClassSecutity", DefaultValue = "Softv.DAO.AdministracionData")]
        public String DataClass
        {
            get { return (string)base["DataClassSecutity"]; }
        }

        /// <summary>
        /// Gets connection string for database Secutity access
        ///</summary>
        [ConfigurationProperty("ConnectionString")]
        public String ConnectionString
        {
            get
            {
                string connectionString = (string)base["ConnectionString"];
                connectionString = String.IsNullOrEmpty(connectionString) ? SoftvSettings.Settings.ConnectionString : (string)base["ConnectionString"];
                return connectionString;
            }
        }
    }
}
