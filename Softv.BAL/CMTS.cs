using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Linq;
using Softv.Providers;
using Softv.Entities;
using Globals;

namespace Softv.BAL
{
    [DataObject]
    [Serializable]
    public class CMTS
    {
        #region Constructors
        public CMTS() { }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CMTSDataEntity GetCMTSDatos(int IdCMTS)
        {
            CMTSDataEntity entities = new CMTSDataEntity();
            entities = ProviderSoftv.CMTS.GetCMTSDatos(IdCMTS);

            return entities ?? new CMTSDataEntity();
        }
    }
}
