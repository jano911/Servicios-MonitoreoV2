﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Softv.Entities
{
    /// <summary>
    /// Class                   : Softv.Entities.SessionEntity.cs
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Session entity
    /// File                    : SessionEntity.cs
    /// Creation date           : 13/12/2015
    /// Creation time           : 10:46 a. m.
    ///</summary>
    [DataContract]
    [Serializable]
    public class SessionEntity : BaseEntity
    {
        #region Attributes

        /// <summary>
        /// Property IdSession
        /// </summary>
        [DataMember]
        public long? IdSession { get; set; }
        /// <summary>
        /// Property IdUsuario
        /// </summary>
        [DataMember]
        public int? IdUsuario { get; set; }
        /// <summary>
        /// Property Token
        /// </summary>
        [DataMember]
        public String Token { get; set; }
        [DataMember]
        public UsuarioEntity Usuario { get; set; }

        #endregion
    }
}

