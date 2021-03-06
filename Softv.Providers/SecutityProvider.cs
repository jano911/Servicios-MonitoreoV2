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
    /// Class                   : Softv.Providers.SecutityProvider
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Secutity Provider
    /// File                    : SecutityProvider.cs
    /// Creation date           : 14/12/2015
    /// Creation time           : 11:51 a. m.
    /// </summary>
    public abstract class SecutityProvider : Globals.DataAccess
    {

        /// <summary>
        /// Instance of Secutity from DB
        /// </summary>
        private static SecutityProvider _Instance = null;

        private static ObjectHandle obj;
        /// <summary>
        /// Generates a Secutity instance
        /// </summary>
        public static SecutityProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    obj = Activator.CreateInstance(
                    SoftvSettings.Settings.Secutity.Assembly,
                    SoftvSettings.Settings.Secutity.DataClass);
                    _Instance = (SecutityProvider)obj.Unwrap();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Provider's default constructor
        /// </summary>
        public SecutityProvider()
        {
        }
        /// <summary>
        /// Abstract method to add Secutity
        ///  /summary>
        /// <param name="Secutity"></param>
        /// <returns></returns>
        public abstract int AddSecutity(SecutityEntity entity_Secutity);

        /// <summary>
        /// Abstract method to delete Secutity
        /// </summary>
        public abstract int DeleteSecutity(int? IdSecutity);

        /// <summary>
        /// Abstract method to update Secutity
        /// </summary>
        public abstract int EditSecutity(SecutityEntity entity_Secutity);

        /// <summary>
        /// Abstract method to get all Secutity
        /// </summary>
        public abstract List<SecutityEntity> GetSecutity();

        /// <summary>
        /// Abstract method to get all Secutity List<int> lid
        /// </summary>
        public abstract List<SecutityEntity> GetSecutity(List<int> lid);

        /// <summary>
        /// Abstract method to get by id
        /// </summary>
        public abstract SecutityEntity GetSecutityById(int? IdSecutity);



        /// <summary>
        ///Get Secutity
        ///</summary>
        public abstract SoftvList<SecutityEntity> GetPagedList(int pageIndex, int pageSize);

        /// <summary>
        ///Get Secutity
        ///</summary>
        public abstract SoftvList<SecutityEntity> GetPagedList(int pageIndex, int pageSize, String xml);

        /// <summary>
        /// Converts data from reader to entity
        /// </summary>
        protected virtual SecutityEntity GetSecutityFromReader(IDataReader reader)
        {
            SecutityEntity entity_Secutity = null;
            try
            {
                entity_Secutity = new SecutityEntity();
                entity_Secutity.IdSecutity = (int?)(GetFromReader(reader, "IdSecutity"));
                entity_Secutity.Module = (String)(GetFromReader(reader, "Module", IsString: true));
                entity_Secutity.Action = (String)(GetFromReader(reader, "Action", IsString: true));
                entity_Secutity.Permision = (String)(GetFromReader(reader, "Permision", IsString: true));

            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Secutity data to entity", ex);
            }
            return entity_Secutity;
        }

    }

    #region Customs Methods

    #endregion
}

