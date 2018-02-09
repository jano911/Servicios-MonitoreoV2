using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Diagnostics.Eventing;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Configuration;
using System.ServiceModel.Web;
using System.Net;
using System.Text;
using Softv.BAL;
using Softv.Entities;

namespace SoftvWCFService
{
    public class MessageInspector : IDispatchMessageInspector, IServiceBehavior
    {
        #region IDispatchMessageInspector
        List<String> lstInvaliModules;
        List<String> lstInvaliAction;
        public MessageInspector()
        {
            lstInvaliModules = ConfigurationManager.AppSettings["NoRegisterInBitacoraModules"].Split(',').ToList();
            lstInvaliAction = ConfigurationManager.AppSettings["NoRegisterInBitacoraStartWith"].Split(',').ToList();
        }

        public static XmlDocument RemoveXmlns(String xml)
        {
            XDocument d = XDocument.Parse(xml);
            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            d.Root.Descendants().Attributes().Where(x => x.Name.Namespace != "").Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(d.CreateReader());

            return xmlDocument;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Methods");
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Origin");
            WebOperationContext.Current.OutgoingResponse.Headers.Remove("Access-Control-Allow-Headers");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Authorization,Content-Type");
        }


        private const string BasicAuth = "Basic";

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //
            List<String> lstUriAction = request.Headers.To.ToString().Split('/').ToList();
            String Action = lstUriAction.Last().ToUpper();
            String Module = lstUriAction[lstUriAction.Count() - 2].ToUpper();
            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                return null;
            }
            else
            {
                // Check to see if there is an Authorization in the header, otherwise throw a 401
                if (WebOperationContext.Current.IncomingRequest.Headers["Authorization"] == null)
                {
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                    throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                }

                else // Decode the header, check password
                {
                    string encodedUnamePwd = "";
                    if (Module == "USUARIO" && Action == "LOGON")
                    {
                        encodedUnamePwd = GetEncodedCredentialsFromHeader();
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
                                return false;
                            }

                            string credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);

                            // Validate User and Password
                            string[] authParts = credentials.Split(':');
                            Usuario objUsuario = new Usuario();
                            UsuarioEntity objUsr = Usuario.GetusuarioByUserAndPass(authParts[0], authParts[1]);
                            if (objUsr == null)
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                            }
                        }
                    }
                    else
                    {
                        encodedUnamePwd = GetTokenFromHeader();
                        if (!string.IsNullOrEmpty(encodedUnamePwd))
                        {
                            List<SessionEntity> objSessionEntity = Session.GetAll();
                            if (!objSessionEntity.Any(x => x.Token == encodedUnamePwd))
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                            }
                            else
                            {
                                SessionEntity objSession = objSessionEntity.Where(x => x.Token == encodedUnamePwd).FirstOrDefault();
                                UsuarioEntity objUsr = Usuario.GetOne(objSession.IdUsuario);
                                if (!(lstInvaliAction.Where(x => (Action.StartsWith(x) || lstInvaliAction.Contains(Action))).Any()))
                                {
                                    List<SecutityEntity> lstSecutityEntity = Secutity.GetPagedList(1, 99999, Globals.SerializeTool.Serialize<SecutityEntity>(new SecutityEntity() { Module = Module })).ToList();
                                    if (lstSecutityEntity.Any(x => x.Action == Action))
                                    {
                                        SecutityEntity objSecutity = lstSecutityEntity.Where(x => x.Action == Action).FirstOrDefault();
                                        if (objSecutity != null)
                                        {
                                            List<PermisoEntity> lstPermisos = Permiso.GetXml(Globals.SerializeTool.Serialize<PermisoEntity>(new PermisoEntity() { IdRol = objUsr.IdRol })).ToList();
                                            PermisoEntity objPermisos = lstPermisos.Where(x => x.Module.ModulePath.ToUpper() == Module.ToUpper()).ToList().FirstOrDefault();

                                            if (objPermisos != null)
                                            {
                                                switch (objSecutity.Permision)
                                                {
                                                    case "S":
                                                        {
                                                            return null;
                                                        }
                                                    case "A":
                                                        {
                                                            if (objPermisos.OptAdd == true)
                                                            {
                                                                return null;
                                                            }
                                                            else
                                                            {
                                                                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                                                            }
                                                        }
                                                    case "D":
                                                        {
                                                            if (objPermisos.OptDelete == true)
                                                            {
                                                                return null;
                                                            }
                                                            else
                                                            {
                                                                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                                                            }
                                                        }
                                                    case "U":
                                                        {

                                                            if (objPermisos.OptUpdate == true)
                                                            {
                                                                return null;
                                                            }
                                                            else
                                                            {
                                                                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                                                            }
                                                        }
                                                    default:
                                                        {
                                                            WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                                            throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                                                        }
                                                }
                                            }
                                            else
                                            {
                                                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                                throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
                                        throw new WebFaultException<string>("Acceso no autorizado, favor de validar autenticación", HttpStatusCode.Unauthorized);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Basic auth encodes uname and pwd pair. We take the credential string from the HTTP header.
        /// </summary>
        /// <returns></returns>
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

        private static string GetTokenFromHeader()
        {
            WebOperationContext ctx = WebOperationContext.Current;

            // credentials are in the Authorization Header
            string credsHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (credsHeader != null)
            {
                return credsHeader;
            }
            // no auth header was found
            return null;
        }


        #endregion

        #region IServiceBehavior

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new MessageInspector());
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}