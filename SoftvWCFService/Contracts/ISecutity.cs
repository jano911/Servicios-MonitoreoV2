
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
    [ServiceContract]
    public interface ISecutity
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSecutity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SecutityEntity GetSecutity(int? IdSecutity);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetDeepSecutity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SecutityEntity GetDeepSecutity(int? IdSecutity);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSecutityList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SecutityEntity> GetSecutityList();
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSecutityPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SecutityEntity> GetSecutityPagedList(int page, int pageSize);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetSecutityPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<SecutityEntity> GetSecutityPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "AddSecutity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? AddSecutity(SecutityEntity objSecutity);
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "UpdateSecutity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? UpdateSecutity(SecutityEntity objSecutity);


    }
}

