﻿using System;
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
    public class Cablemodem
    {
        #region Constructors
        public Cablemodem() { }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<CablemodemEntity> GetListaCablemodem(int IdCMTS)
        {
            List<CablemodemEntity> entities = new List<CablemodemEntity>();
            entities = ProviderSoftv.Cablemodem.GetListaCablemodem(IdCMTS);

            return entities ?? new List<CablemodemEntity>();
        }
    }
}
