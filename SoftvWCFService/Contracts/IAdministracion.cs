using Globals;
using Softv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace SoftvWCFService.Contracts
{
    [AuthenticatingHeader]
    [ServiceContract]
    public interface IAdministracion
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetCMTSLista", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<CMTSEntity> GetCMTSLista();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTipoCMTS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<TipoCMTSEntity> GetTipoCMTS();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetNuevoCMTS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int GetNuevoCMTS(string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetCMTSPorId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CMTSEntity GetCMTSPorId(int IdCMTS);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetEditaCMTS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int GetEditaCMTS(int IdCMTS, string Nombre, string IP, string Comunidad, string ComunidadCablemodem, int IdTipo, string interfaceS, string Usuario, string PasswordS, string Enable);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetEliminaCMTS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int GetEliminaCMTS(int IdCMTS);
    }
}