
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class TerminalElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Terminal class
        /// </summary>
        [ConfigurationProperty( "Assembly")]
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
        /// Gets class name for Terminal
        ///</summary>
        [ConfigurationProperty("DataClassTerminal", DefaultValue = "Softv.DAO.TerminalData")]
        public String DataClass
        {
          get { return (string)base["DataClassTerminal"]; }
        }

        /// <summary>
        /// Gets connection string for database Terminal access
        ///</summary>
        [ConfigurationProperty("ConnectionString")]
        public String ConnectionString
        {
          get
          {
            string connectionString = (string)base["ConnectionString"];
            connectionString = String.IsNullOrEmpty(connectionString) ? SoftvSettings.Settings.ConnectionString :  (string)base["ConnectionString"];
            return connectionString;
          }
        }
      }
    }

  