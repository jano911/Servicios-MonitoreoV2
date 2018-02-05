using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Softv.Entities
{
    [DataContract]
    [Serializable]
    public class SecutityEntity : BaseEntity
    {
        #region Attributes

        /// <summary>
        /// Property IdSecutity
        /// </summary>
        [DataMember]
        public int? IdSecutity { get; set; }
        /// <summary>
        /// Property Module
        /// </summary>
        [DataMember]
        public String Module { get; set; }
        /// <summary>
        /// Property Action
        /// </summary>
        [DataMember]
        public String Action { get; set; }
        /// <summary>
        /// Property Permision
        /// </summary>
        [DataMember]
        public String Permision { get; set; }
        #endregion
    }
}

