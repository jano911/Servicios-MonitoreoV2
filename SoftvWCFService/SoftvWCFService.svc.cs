using Globals;
using Newtonsoft.Json;
using Softv.BAL;
using Softv.Entities;
using SoftvWCFService.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Xml.Linq;
namespace SoftvWCFService
{
    [ScriptService]
    public partial class SoftvWCFService : ISecutity, ISession, IUsuario, IModule, IRole, IPermiso, ITerminal, IUsuarioSystem
    {


        #region Secutity
        public SecutityEntity GetSecutity(int? IdSecutity)
        {
            return Secutity.GetOne(IdSecutity);
        }

        public SecutityEntity GetDeepSecutity(int? IdSecutity)
        {
            return Secutity.GetOneDeep(IdSecutity);
        }

        public IEnumerable<SecutityEntity> GetSecutityList()
        {
            return Secutity.GetAll();
        }

        public SoftvList<SecutityEntity> GetSecutityPagedList(int page, int pageSize)
        {
            return Secutity.GetPagedList(page, pageSize);
        }

        public SoftvList<SecutityEntity> GetSecutityPagedListXml(int page, int pageSize, String xml)
        {
            return Secutity.GetPagedList(page, pageSize, xml);
        }

        public int? AddSecutity(SecutityEntity objSecutity)
        {
            return Secutity.Add(objSecutity);
        }

        public int? UpdateSecutity(SecutityEntity objSecutity)
        {
            return Secutity.Edit(objSecutity);
        }

        public int? DeleteSecutity(String BaseRemoteIp, int BaseIdUser, int? IdSecutity)
        {
            return Secutity.Delete(IdSecutity);
        }

        #endregion

        #region Session
        public SessionEntity GetSession(long? IdSession)
        {
            return Session.GetOne(IdSession);
        }

        public SessionEntity GetDeepSession(long? IdSession)
        {
            return Session.GetOneDeep(IdSession);
        }

        public IEnumerable<SessionEntity> GetSessionList()
        {
            return Session.GetAll();
        }

        public SoftvList<SessionEntity> GetSessionPagedList(int page, int pageSize)
        {
            return Session.GetPagedList(page, pageSize);
        }

        public SoftvList<SessionEntity> GetSessionPagedListXml(int page, int pageSize, String xml)
        {
            return Session.GetPagedList(page, pageSize, xml);
        }

        public int? AddSession(SessionEntity objSession)
        {
            return Session.Add(objSession);
        }

        public int? UpdateSession(SessionEntity objSession)
        {
            return Session.Edit(objSession);
        }

        public int? DeleteSession(String BaseRemoteIp, int BaseIdUser, long? IdSession)
        {
            return Session.Delete(IdSession);
        }

        #endregion

        #region Usuario
        public UsuarioEntity GetusuarioByUserAndPass(string Usuariox, string Pass)
        {
            return Usuario.GetusuarioByUserAndPass(Usuariox, Pass);
        }


        private const string BasicAuth = "Basic";

        public UsuarioEntity LogOn()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            if (WebOperationContext.Current.IncomingRequest.Headers["Authorization"] == null)
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
            }
            else // Decode the header, check password
            {
                string encodedUnamePwd = GetEncodedCredentialsFromHeader();
                if (!string.IsNullOrEmpty(encodedUnamePwd))
                {
                    // Decode the credentials
                    byte[] decodedBytes = null;
                    try
                    {
                        decodedBytes = Convert.FromBase64String(encodedUnamePwd);
                    }
                    catch (FormatException)
                    {

                    }

                    string credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);

                    // Validate User and Password
                    string[] authParts = credentials.Split(':');
                    Usuario objUsuario = new Usuario();
                    UsuarioEntity objUsr = Usuario.GetusuarioByUserAndPass(authParts[0], authParts[1]);

                    if (objUsr != null)
                    {
                        List<SessionEntity> lstSessions = Session.GetAll();
                        if (lstSessions.Any(x => x.IdUsuario == objUsr.IdUsuario))
                        {
                            foreach (SessionEntity i in lstSessions.Where(x => x.IdUsuario == objUsr.IdUsuario))
                            {
                                Session.Delete(i.IdSession);
                            }
                        }
                        byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                        byte[] key = Guid.NewGuid().ToByteArray();
                        string token = Convert.ToBase64String(time.Concat(key).ToArray());
                        Session.Add(new SessionEntity() { IdUsuario = objUsr.IdUsuario.Value, Token = token });
                        objUsr.Token = token;
                        objUsr.Password = "";

                        var R = objUsr.IdRol.Value;
                        var U = objUsr.IdUsuario.Value;

                        List<UsuarioEntity> usua = Usuario.GetAll();
                        var usua2 = usua.Where(x => x.IdUsuario == U);
                        var total = usua2.Count();

                        List<PermisoEntity> per = Permiso.GetAll();
                        List<PermisoEntity> per2 = per.Where(x => x.IdRol == R).ToList();

                        objUsr.permiso2 = per2;

                        return objUsr;
                    }
                    else
                    {
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                        throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                    }
                }
            }

            return new UsuarioEntity();
        }

        private static string GetEncodedCredentialsFromHeader()
        {
            WebOperationContext ctx = WebOperationContext.Current;

            // credentials are in the Authorization Header
            string credsHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (credsHeader != null)
            {
                // make sure that we have 'Basic' auth header. Anything else can't be handled
                string creds = null;
                int credsPosition = credsHeader.IndexOf(BasicAuth, StringComparison.OrdinalIgnoreCase);
                if (credsPosition != -1)
                {
                    // 'Basic' creds were found
                    credsPosition += BasicAuth.Length + 1;
                    if (credsPosition < credsHeader.Length - 1)
                    {
                        creds = credsHeader.Substring(credsPosition, credsHeader.Length - credsPosition);
                        return creds;
                    }
                    return null;
                }
                else
                {
                    // we did not find Basic auth header but some other type of auth. We can't handle it. Return null.
                    return null;
                }
            }

            // no auth header was found
            return null;
        }

        public IEnumerable<UsuarioEntity> GetUsuarioList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.GetAll();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddUsuario(UsuarioEntity objUsuario)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.Add(objUsuario);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateUsuario(UsuarioEntity objUsuario)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.Edit(objUsuario);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<UsuarioEntity> GetUsuario2List(String Nombre, String Email, String Usuario2, int? Op, int? IdRol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.GetAll2(Nombre, Email, Usuario2, Op, IdRol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<UsuarioEntity> GetUserListbyIdUser(int? IdUsuario)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.GetUserListbyIdUser(IdUsuario);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public UsuarioEntity GetExisteUser(String Usuario2, int? Op)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Usuario.GetExisteUser(Usuario2, Op);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        #endregion

        #region Module



        public List<ModuleEntity> GetModulos_Permisos(int? idrol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetModulos_Permisos(idrol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }


        }

        public ModuleEntity GetModule(int? IdModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetOne(IdModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public ModuleEntity GetDeepModule(int? IdModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetOneDeep(IdModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        public IEnumerable<ModuleEntity> GetModuleList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetAll();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<ModuleEntity> GetModulePagedList(int page, int pageSize)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetPagedList(page, pageSize);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<ModuleEntity> GetModulePagedListXml(int page, int pageSize, String xml)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.GetPagedList(page, pageSize, xml);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddModule(ModuleEntity objModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.Add(objModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateModule(ModuleEntity objModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.Edit(objModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? DeleteModule(String BaseRemoteIp, int BaseIdUser, int? IdModule)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Module.Delete(IdModule);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        #endregion

        #region Role

        public RoleEntity GetRoleById(int? IdRol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.GetRoleById(IdRol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }


        }



        public IEnumerable<RoleEntity> GetRoleList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.GetAll();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddRole(RoleEntity objRole)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.Add(objRole);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateRole(RoleEntity objRole)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Role.Edit(objRole);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<RoleEntity> GetUpListPermisos(RoleEntity objRole, List<PermisoEntity> LstPer)
        {

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                objRole.LstPer = LstPer;

                XElement xe = XElement.Parse(Globals.SerializeTool.Serialize<RoleEntity>(objRole));

                XElement xml2 = XElement.Parse(Globals.SerializeTool.SerializeList<PermisoEntity>(objRole.LstPer, "objRole"));

                xe.Add(xml2);
                try
                {
                    return Role.GetUpListPermisos(xe.ToString());
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message + " " + xe.ToString(), HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<PermisoEntity> GetPermiRolList(int? IdRol)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Permiso.GetPermisoRolList(IdRol);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        


        #endregion

        #region Permiso
        public IEnumerable<PermisoEntity> GetPermisoList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Permiso.GetAll();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        #endregion

        
        #region Terminal

        //public string GetUploadFiles()
        //{
        //    if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
        //    {
        //        return null;


        //    }
        //    else
        //    {
        //        try
        //        {
        //            var request = HttpContext.Current.Request;

        //            if (request.Files.Count > 0)
        //            {
        //                var postedFile = request.Files[0];
        //                Stream stream = postedFile.InputStream;

        //                return Terminal.GetUploadFiles(stream);
        //            }
        //            else
        //            {

        //                return "";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
        //        }
        //    }

        //}

        public TerminalEntity GetTerminal(long? SAN)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetOne(SAN);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public TerminalEntity GetDeepTerminal(long? SAN)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetOneDeep(SAN);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        public IEnumerable<TerminalEntity> GetTerminalList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetAll();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<TerminalEntity> GetTerminalPagedList(int page, int pageSize)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetPagedList(page, pageSize);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<TerminalEntity> GetTerminalPagedListXml(int page, int pageSize, String xml)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetPagedList(page, pageSize, xml);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddTerminal(TerminalEntity objTerminal)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.Add(objTerminal);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateTerminal(TerminalEntity objTerminal)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.Edit(objTerminal);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? DeleteTerminal(String BaseRemoteIp, int BaseIdUser, long? SAN)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.Delete(SAN);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<TerminalEntity> GetFilterTerminalList(long? SAN, String Suscriptor, String Estatus, int? IdBeam, string ESN, string satelite, int? IdServicio, int? Op)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetFilterTerminalList(SAN, Suscriptor, Estatus, IdBeam, ESN, satelite, IdServicio, Op);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public IEnumerable<TerminalEntity> GetTerminaByIdSusList(long? IdSuscriptor)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetTerminaByIdSusList(IdSuscriptor);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public TerminalEntity GetByTerminal(long? SAN)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetByTerminal(SAN);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        public IEnumerable<TerminalEntity> GetDeepIdSuscriptor(long? IdSuscriptor)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.GetDeepIdSuscriptor(IdSuscriptor);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateTerminalInformacionAdicional(TerminalEntity objTerminal)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return Terminal.TerminalInformacionAdicional(objTerminal);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        

        #endregion
        
        #region UsuarioSystem
        public UsuarioSystemEntity GetUsuarioSystem(int? IdUsuario)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.GetOne(IdUsuario);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public UsuarioSystemEntity GetDeepUsuarioSystem(int? IdUsuario)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.GetOneDeep(IdUsuario);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }

            }
        }

        public IEnumerable<UsuarioSystemEntity> GetUsuarioSystemList()
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.GetAll();
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<UsuarioSystemEntity> GetUsuarioSystemPagedList(int page, int pageSize)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.GetPagedList(page, pageSize);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public SoftvList<UsuarioSystemEntity> GetUsuarioSystemPagedListXml(int page, int pageSize, String xml)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.GetPagedList(page, pageSize, xml);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? AddUsuarioSystem(UsuarioSystemEntity objUsuarioSystem)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.Add(objUsuarioSystem);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? UpdateUsuarioSystem(UsuarioSystemEntity objUsuarioSystem)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.Edit(objUsuarioSystem);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        public int? DeleteUsuarioSystem(String BaseRemoteIp, int BaseIdUser, int? IdUsuario)
        {
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                try
                {
                    return UsuarioSystem.Delete(IdUsuario);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message, HttpStatusCode.ExpectationFailed);
                }
            }
        }

        #endregion
        
    }
}
