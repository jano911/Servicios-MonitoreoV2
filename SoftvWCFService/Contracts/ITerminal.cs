
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
    public interface ITerminal
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTerminal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        TerminalEntity GetTerminal(long? SAN);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepTerminal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        TerminalEntity GetDeepTerminal(long? SAN);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTerminalList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<TerminalEntity> GetTerminalList();

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTerminalPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<TerminalEntity> GetTerminalPagedList(int page, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTerminalPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<TerminalEntity> GetTerminalPagedListXml(int page, int pageSize, String xml);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddTerminal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddTerminal(TerminalEntity objTerminal);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateTerminal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateTerminal(TerminalEntity objTerminal);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DeleteTerminal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? DeleteTerminal(String BaseRemoteIp, int BaseIdUser, long? SAN);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetFilterTerminalList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<TerminalEntity> GetFilterTerminalList(long? SAN, String Suscriptor, String Estatus, int? IdBeam, string ESN, string satelite, int? IdServicio, int? Op);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetTerminaByIdSusList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<TerminalEntity> GetTerminaByIdSusList(long? IdSuscriptor);


        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetByTerminal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        TerminalEntity GetByTerminal(long? SAN);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepIdSuscriptor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<TerminalEntity> GetDeepIdSuscriptor(long? IdSuscriptor);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateTerminalInformacionAdicional", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateTerminalInformacionAdicional(TerminalEntity objTerminal);

    }
}

