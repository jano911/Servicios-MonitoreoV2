
using Globals;
using Softv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface IUsuario
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetusuarioByUserAndPass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UsuarioEntity GetusuarioByUserAndPass(string Usuario, string Pass);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "LogOn", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        UsuarioEntity LogOn();


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUsuarioList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<UsuarioEntity> GetUsuarioList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddUsuario(UsuarioEntity objUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateUsuario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateUsuario(UsuarioEntity objUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUsuario2List", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<UsuarioEntity> GetUsuario2List(String Nombre, String Email, String Usuario2, int? Op, int? IdRol);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetUserListbyIdUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<UsuarioEntity> GetUserListbyIdUser(int? IdUsuario);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetExisteUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UsuarioEntity GetExisteUser(String Usuario2, int? Op);


    }
}

