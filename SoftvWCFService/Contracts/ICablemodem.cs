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
    public interface ICablemodem
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetListaCablemodem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<CablemodemEntity> GetListaCablemodem(int IdCMTS);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetHistorialConsumo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ConsumoEntity> GetHistorialConsumo(string MAC);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDatosCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ClienteEntity GetDatosCliente(string MAC);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetConsumoActual", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ConsumoEntity GetConsumoActual(string MAC);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetIPCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ClienteEntity GetIPCliente(string MAC);
    }
}