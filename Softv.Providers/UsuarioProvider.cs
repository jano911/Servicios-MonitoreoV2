﻿
using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting;
using Softv.Entities;
using SoftvConfiguration;
using Globals;

namespace Softv.Providers
{
    /// <summary>
    /// Class                   : Softv.Providers.UsuarioProvider
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Usuario Provider
    /// File                    : UsuarioProvider.cs
    /// Creation date           : 19/09/2015
    /// Creation time           : 03:46 p. m.
    /// </summary>
    public abstract class UsuarioProvider : Globals.DataAccess
    {

        /// <summary>
        /// Instance of Usuario from DB
        /// </summary>
        private static UsuarioProvider _Instance = null;

        private static ObjectHandle obj;
        /// <summary>
        /// Generates a Usuario instance
        /// </summary>
        public static UsuarioProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    obj = Activator.CreateInstance(
                    SoftvSettings.Settings.Usuario.Assembly,
                    SoftvSettings.Settings.Usuario.DataClass);
                    _Instance = (UsuarioProvider)obj.Unwrap();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Provider's default constructor
        /// </summary>
        public UsuarioProvider()
        {
        }
        /// <summary>
        /// Abstract method to add Usuario
        ///  /summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public abstract int AddUsuario(UsuarioEntity entity_Usuario);

        /// <summary>
        /// Abstract method to delete Usuario
        /// </summary>
        public abstract int DeleteUsuario(int? IdUsuario);

        /// <summary>
        /// Abstract method to update Usuario
        /// </summary>
        public abstract int EditUsuario(UsuarioEntity entity_Usuario);

        /// <summary>
        /// Abstract method to get all Usuario
        /// </summary>
        public abstract List<UsuarioEntity> GetUsuario();

        public abstract List<UsuarioEntity> GetUsuario2(String Nombre, String Email, String Usuario2, int? Op, int? IdRol);

        public abstract List<UsuarioEntity> GetUserListbyIdUser(int? IdUsuario);

        public abstract UsuarioEntity GetExisteUser(String Usuario2, int? Op);









        /// <summary>
        /// Abstract method to get all Usuario List<int> lid
        /// </summary>
        public abstract List<UsuarioEntity> GetUsuario(List<int> lid);

        /// <summary>
        /// Abstract method to get by id
        /// </summary>
        public abstract UsuarioEntity GetUsuarioById(int? IdUsuario);


        /// <summary>
        /// Abstract method to Change State Usuario
        /// </summary>
        public abstract int ChangeStateUsuario(int? IdUsuario, bool State);


        /// <summary>
        ///Get Usuario
        ///</summary>
        public abstract SoftvList<UsuarioEntity> GetPagedList(int pageIndex, int pageSize);

        /// <summary>
        ///Get Usuario
        ///</summary>
        public abstract SoftvList<UsuarioEntity> GetPagedList(int pageIndex, int pageSize, String xml);

        public abstract UsuarioEntity GetusuarioByUserAndPass(string Usuariox, string Pass);

        /// <summary>
        /// Converts data from reader to entity
        /// </summary>
        protected virtual UsuarioEntity GetUsuarioFromReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.IdUsuario = (int?)(GetFromReader(reader, "IdUsuario"));
                entity_Usuario.IdRol = (int?)(GetFromReader(reader, "IdRol"));
                entity_Usuario.Nombre = (String)(GetFromReader(reader, "Nombre", IsString: true));
                entity_Usuario.Email = (String)(GetFromReader(reader, "Email", IsString: true));
                entity_Usuario.Usuario = (String)(GetFromReader(reader, "Usuario", IsString: true));
                entity_Usuario.Password = (String)(GetFromReader(reader, "Password", IsString: true));
                entity_Usuario.Estado = (bool?)(GetFromReader(reader, "Estado"));               


            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }

        protected virtual UsuarioEntity GetUsuarioRolFromReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.IdUsuario = (int?)(GetFromReader(reader, "IdUsuario"));
                entity_Usuario.IdRol = (int?)(GetFromReader(reader, "IdRol"));
                entity_Usuario.Nombre = (String)(GetFromReader(reader, "Nombre", IsString: true));
                entity_Usuario.Email = (String)(GetFromReader(reader, "Email", IsString: true));
                entity_Usuario.Usuario = (String)(GetFromReader(reader, "Usuario", IsString: true));
                entity_Usuario.Password = (String)(GetFromReader(reader, "Password", IsString: true));
                entity_Usuario.Estado = (bool?)(GetFromReader(reader, "Estado"));
                entity_Usuario.NombreRol = (String)(GetFromReader(reader, "NombreRol", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }





        protected virtual UsuarioEntity GetExisteUserFromReader(IDataReader reader)
        {
            UsuarioEntity entity_Usuario = null;
            try
            {
                entity_Usuario = new UsuarioEntity();
                entity_Usuario.Bnd = (int?)(GetFromReader(reader, "Bnd"));
                entity_Usuario.Msg = (String)(GetFromReader(reader, "Msg", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Usuario data to entity", ex);
            }
            return entity_Usuario;
        }

























    }

    #region Customs Methods

    #endregion
}

