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
    public interface ICMTS
    {
        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "GetCMTSDatos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CMTSDataEntity GetCMTSDatos(int IdCMTS);
    }
}