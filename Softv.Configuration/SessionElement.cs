
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class SessionElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Session class
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
        /// Gets class name for Session
        ///</summary>
        [ConfigurationProperty("DataClassSession", DefaultValue = "Softv.DAO.SessionData")]
        public String DataClass
        {
            get { return (string)base["DataClassSession"]; }
        }

        /// <summary>
        /// Gets connection string for database Session access
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

