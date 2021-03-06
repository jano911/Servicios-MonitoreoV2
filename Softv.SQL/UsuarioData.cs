﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Softv.Entities;
using Softv.Providers;
using SoftvConfiguration;
using Globals;

namespace Softv.DAO
{
    /// <summary>
    /// Class                   : Softv.DAO.UsuarioData
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Usuario Data Access Object
    /// File                    : UsuarioDAO.cs
    /// Creation date           : 19/09/2015
    /// Creation time           : 03:46 p. m.
    ///</summary>
    public class UsuarioData : UsuarioProvider
    {
        /// <summary>
        ///</summary>
        /// <param name="Usuario"> Object Usuario added to List</param>
        public override int AddUsuario(UsuarioEntity entity_Usuario)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioAdd", connection);

                AssingParameter(comandoSql, "@IdUsuario", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@IdRol", entity_Usuario.IdRol);

                AssingParameter(comandoSql, "@Nombre", entity_Usuario.Nombre);

                AssingParameter(comandoSql, "@Email", entity_Usuario.Email);

                AssingParameter(comandoSql, "@Usuario", entity_Usuario.Usuario);

                AssingParameter(comandoSql, "@Password", entity_Usuario.Password);

                AssingParameter(comandoSql, "@RecibeMensaje", entity_Usuario.RecibeMensaje);

                AssingParameter(comandoSql, "@CheckMemoria", entity_Usuario.CheckMemoria);



                //AssingParameter(comandoSql, "@Estado", entity_Usuario.Estado);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding Usuario " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
               // result = (int)comandoSql.Parameters["@IdUsuario"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a Usuario
        ///</summary>
        /// <param name="">  IdUsuario to delete </param>
        public override int DeleteUsuario(int? IdUsuario)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioDelete", connection);

                AssingParameter(comandoSql, "@IdUsuario", IdUsuario);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Edits a Usuario
        ///</summary>
        /// <param name="Usuario"> Objeto Usuario a editar </param>
        public override int EditUsuario(UsuarioEntity entity_Usuario)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioEdit", connection);

                AssingParameter(comandoSql, "@IdUsuario", entity_Usuario.IdUsuario);

                AssingParameter(comandoSql, "@IdRol", entity_Usuario.IdRol);

                AssingParameter(comandoSql, "@Nombre", entity_Usuario.Nombre);

                AssingParameter(comandoSql, "@Email", entity_Usuario.Email);

                AssingParameter(comandoSql, "@Usuario", entity_Usuario.Usuario);

                AssingParameter(comandoSql, "@Password", entity_Usuario.Password);

                AssingParameter(comandoSql, "@RecibeMensaje", entity_Usuario.RecibeMensaje);

                AssingParameter(comandoSql, "@CheckMemoria", entity_Usuario.CheckMemoria);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Gets all Usuario
        ///</summary>
        public override List<UsuarioEntity> GetUsuario()
        {
            List<UsuarioEntity> UsuarioList = new List<UsuarioEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        UsuarioList.Add(GetUsuarioRolFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return UsuarioList;
        }



        public override List<UsuarioEntity> GetUsuario2(String Nombre, String Email, String Usuario2, int? Op, int? IdRol)
        {
            List<UsuarioEntity> UsuarioList = new List<UsuarioEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("FiltrosUsuarios", connection);

                AssingParameter(comandoSql, "@Nombre", Nombre);
                AssingParameter(comandoSql, "@Email", Email);
                AssingParameter(comandoSql, "@Usuario", Usuario2); 
                AssingParameter(comandoSql, "@Op", Op);
                AssingParameter(comandoSql, "@IdRol", IdRol);
                
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        UsuarioList.Add(GetUsuarioRolFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return UsuarioList;
        }



        public override List<UsuarioEntity> GetUserListbyIdUser(int? IdUsuario)
        {
            List<UsuarioEntity> UsuarioList = new List<UsuarioEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetByIdUser", connection);

                AssingParameter(comandoSql, "@IdUsuario", IdUsuario);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        UsuarioList.Add(GetUsuarioFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return UsuarioList;
        }


        public override UsuarioEntity GetExisteUser(String Usuario2, int? Op)
       {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("SiExisteUsuario", connection);
                UsuarioEntity entity_Usuario = null;


                AssingParameter(comandoSql, "@Usuario", Usuario2);
                AssingParameter(comandoSql, "@Op", Op);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Usuario = GetExisteUserFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Usuario;
            }

        }


        

                /// <summary>
        /// Gets all Usuario by List<int>
        ///</summary>
        public override List<UsuarioEntity> GetUsuario(List<int> lid)
        {
            List<UsuarioEntity> UsuarioList = new List<UsuarioEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        UsuarioList.Add(GetUsuarioFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return UsuarioList;
        }

        /// <summary>
        /// Gets Usuario by
        ///</summary>
        public override UsuarioEntity GetUsuarioById(int? IdUsuario)
        {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetById", connection);
                UsuarioEntity entity_Usuario = null;


                AssingParameter(comandoSql, "@IdUsuario", IdUsuario);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Usuario = GetUsuarioFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Usuario;
            }

        }


        /// <summary>
        /// Change State a Usuario
        ///</summary>
        /// <param name="">  IdUsuario to delete </param>
        public override int ChangeStateUsuario(int? IdUsuario, bool State)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioChangeState", connection);

                AssingParameter(comandoSql, "@IdUsuario", IdUsuario);

                AssingParameter(comandoSql, "@Estado", State);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }


        /// <summary>
        ///Get Usuario
        ///</summary>
        public override SoftvList<UsuarioEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<UsuarioEntity> entities = new SoftvList<UsuarioEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetPaged", connection);

                AssingParameter(comandoSql, "@pageIndex", pageIndex);
                AssingParameter(comandoSql, "@pageSize", pageSize);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetUsuarioFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetUsuarioCount();
                return entities ?? new SoftvList<UsuarioEntity>();
            }
        }

        /// <summary>
        ///Get Usuario
        ///</summary>
        public override SoftvList<UsuarioEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<UsuarioEntity> entities = new SoftvList<UsuarioEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetPagedXml", connection);

                AssingParameter(comandoSql, "@pageSize", pageSize);
                AssingParameter(comandoSql, "@pageIndex", pageIndex);
                AssingParameter(comandoSql, "@xml", xml);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetUsuarioFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetUsuarioCount(xml);
                return entities ?? new SoftvList<UsuarioEntity>();
            }
        }

        /// <summary>
        ///Get Count Usuario
        ///</summary>
        public int GetUsuarioCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }


        /// <summary>
        ///Get Count Usuario
        ///</summary>
        public int GetUsuarioCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Usuario " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        //public override UsuarioEntity GetusuarioByUserAndPass(string Usuariox, string Pass)
        //{
        //    using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
        //    {

        //        SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetusuarioByUserAndPass", connection);
        //        UsuarioEntity entity_Usuario = null;
        //        AssingParameter(comandoSql, "@Usuario", Usuariox);
        //        AssingParameter(comandoSql, "@Password", Pass);

        //        IDataReader rd = null;
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();
        //            rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
        //            if (rd.Read())
        //                entity_Usuario = GetUsuarioFromReader(rd);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
        //        }
        //        finally
        //        {
        //            if (connection != null)
        //                connection.Close();
        //            if (rd != null)
        //                rd.Close();
        //        }
        //        return entity_Usuario;
        //    }

        //}




        public override UsuarioEntity GetusuarioByUserAndPass(string Usuariox, string Pass)
        {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Usuario.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_UsuarioGetusuarioByUserAndPass", connection);
                UsuarioEntity entity_Usuario = null;
                AssingParameter(comandoSql, "@Usuario", Usuariox);
                AssingParameter(comandoSql, "@Password", Pass);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Usuario = GetUsuarioFromReader(rd);

                    rd.Close();
                    List<Menu> lmenu = new List<Menu>();
                    List<Menu> menusuario = new List<Menu>();
                    List<Menu> menusprincipales = new List<Menu>();
                    SqlCommand comandoSql2 = CreateCommand("GetUserMenu", connection);
                    AssingParameter(comandoSql2, "@Rol", entity_Usuario.IdRol);
                    rd = ExecuteReader(comandoSql2);
                    while (rd.Read())
                    {
                        Menu m = new Menu();
                        m.IdModule = Int32.Parse(rd[0].ToString());
                        m.OptAdd = bool.Parse(rd[2].ToString());
                        m.OptDelete = bool.Parse(rd[3].ToString());
                        m.OptSelect = bool.Parse(rd[4].ToString());
                        m.OptUpdate = bool.Parse(rd[5].ToString());
                        m.Title = rd[6].ToString();
                        m.Class = rd[7].ToString();
                        try
                        {
                            m.ParentId = Int32.Parse(rd[8].ToString());
                        }
                        catch
                        {
                            m.ParentId = 0;
                        }

                        m.Icon = rd[9].ToString();
                        try
                        {
                            m.SortOrder = Int32.Parse(rd[10].ToString());
                        }
                        catch
                        {
                        }

                        lmenu.Add(m);
                        
                    }
                    rd.Close();
                    menusprincipales = lmenu.Where(xx => xx.ParentId == 0).ToList();
                    menusprincipales.ForEach(y =>
                    {
                        menusuario = lmenu.Where(p => p.ParentId.Value == y.IdModule ).ToList();
                        y.MenuChild = menusuario;

                        y.MenuChild.ForEach(t =>
                        {
                            t.MenuChild = lmenu.Where(n => n.ParentId.Value == t.IdModule).ToList();
                        });
                    });

                    entity_Usuario.Menu = menusprincipales;
                    

                }
                catch (Exception ex)
                {
                    throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Usuario;
            }

        }















        #region Customs Methods

        #endregion
    }
}
