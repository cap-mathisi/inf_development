using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.apimodels
{
    public class ProtocolGroupDTO
    {
        public decimal ProtocolCKey { get; set; }
        public string ProtocolName { get; set; }
        public string ProtocolShortName { get; set; }
        public decimal CreatedBy { get; set; }
        // NAA. Why?
        //public System.Data.Linq.Binary TS { get; set; }
    }
}
