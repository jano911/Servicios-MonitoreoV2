﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Linq;
using Softv.Providers;
using Softv.Entities;
using Globals;

/// <summary>
/// Class                   : Softv.BAL.Client.cs
/// Generated by            : Class Generator (c) 2015
/// Description             : PermisoBussines
/// File                    : PermisoBussines.cs
/// Creation date           : 19/09/2015
/// Creation time           : 04:34 p. m.
///</summary>
namespace Softv.BAL
{

    [DataObject]
    [Serializable]
    public class Permiso
    {

        #region Constructors
        public Permiso() { }
        #endregion

        /// <summary>
        ///Adds Permiso
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static int MargePermiso(String xml)
        {
            int result = ProviderSoftv.Permiso.MargePermiso(xml);
            return result;
        }

        /// <summary>
        ///Get Permiso
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static SoftvList<PermisoEntity> GetXml(String xml)
        {
            SoftvList<PermisoEntity> entities = new SoftvList<PermisoEntity>();
            entities = ProviderSoftv.Permiso.GetXml(xml);

            List<ModuleEntity> lstModule = new List<ModuleEntity>();
            foreach (int i in entities.Where(x => x.IdModule.HasValue).Select(x => x.IdModule.Value).Distinct().ToList())
                lstModule.Add(ProviderSoftv.Module.GetModuleById(i));

            lstModule.ForEach(XModule => entities.Where(x => x.IdModule.HasValue).Where(x => x.IdModule == XModule.IdModule).ToList().ForEach(y => y.Module = XModule));

            return entities ?? new SoftvList<PermisoEntity>();
        }


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<PermisoEntity> GetPermisoRolList(int? IdRol)
        {

            List<PermisoEntity> entities = new List<PermisoEntity>();
            entities = ProviderSoftv.Permiso.GetPermisoRolList(IdRol);

            //List<RoleEntity> lRole = ProviderSoftv.Role.GetRole(entities.Where(x => x.IdRol.HasValue).Select(x => x.IdRol.Value).ToList());
            //lRole.ForEach(xRole => entities.Where(x => x.IdRol.HasValue).Where(x => x.IdRol == xRole.IdRol).ToList().ForEach(y => y.Role = xRole));

            return entities ?? new List<PermisoEntity>();

        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<PermisoEntity> GetAll()
        {

            List<PermisoEntity> entities = new List<PermisoEntity>();
            entities = ProviderSoftv.Permiso.GetPermiso();

            List<RoleEntity> lRole = ProviderSoftv.Role.GetRole(entities.Where(x => x.IdRol.HasValue).Select(x => x.IdRol.Value).ToList());
            lRole.ForEach(xRole => entities.Where(x => x.IdRol.HasValue).Where(x => x.IdRol == xRole.IdRol).ToList().ForEach(y => y.Role = xRole));

            return entities ?? new List<PermisoEntity>();

        }



    }
}
