using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class ProtocolVersionsStates
    {
        public decimal ProtocolversionKey = DefaultValue.Ckey;
        public decimal ProtocolversionStatusKey = DefaultValue.Ckey;
        public decimal ReleaseStatesKey = DefaultValue.Ckey;
        public DateTime? CreatedDate;
        public string CratedBy = String.Empty;
        public DateTime? ModifiedDate;
        public string ModifiedBy = String.Empty;
        public string ProtocolVersion = String.Empty;
        public string ReleaseStatus = String.Empty;
        
    }
}
