﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Softv.Entities
{
    /// <summary>
    /// Class                   : Softv.Entities.UsuarioEntity.cs
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Usuario entity
    /// File                    : UsuarioEntity.cs
    /// Creation date           : 19/09/2015
    /// Creation time           : 03:46 p. m.
    ///</summary>
    [DataContract]
    [Serializable]
    public class UsuarioEntity : BaseEntity
    {
        #region Attributes

        /// <summary>
        /// Property IdUsuario
        /// </summary>
        [DataMember]
        public int? IdUsuario { get; set; }

        /// <summary>
        /// Property IdRol
        /// </summary>
        [DataMember]
        public int? IdRol { get; set; }

        /// <summary>
        /// Property Nombre
        /// </summary>
        [DataMember]
        public String Nombre { get; set; }

        /// <summary>
        /// Property Email
        /// </summary>
        [DataMember]
        public String Email { get; set; }

        /// <summary>
        /// Property Usuario
        /// </summary>
        [DataMember]
        public String Usuario { get; set; }

        /// <summary>
        /// Property Password
        /// </summary>
        [DataMember]
        public String Password { get; set; }

        /// <summary>
        /// Property Estado
        /// </summary>
        [DataMember]
        public bool? Estado { get; set; }


        [DataMember]
        public RoleEntity Role { get; set; }


        [DataMember]
        public String Token { get; set; }


        [DataMember]
        public PermisoEntity Permiso { get; set; }


        [DataMember]
        public List<PermisoEntity> permiso2 { get; set; }


        [DataMember]
        public List<Menu> Menu { get; set; }



        [DataMember]
        public int? Op { get; set; }


        [DataMember]
        public String Usuario2 { get; set; }

        [DataMember]
        public String NombreRol { get; set; }



        [DataMember]
        public int? Bnd { get; set; }

        [DataMember]
        public String Msg { get; set; }


        [DataMember]
        public int ? UsuarioSAC { get; set; }


        [DataMember]
        public bool? RecibeMensaje { get; set; }

        [DataMember]
        public bool ? CheckMemoria { get; set; }

        #endregion
    }
}

