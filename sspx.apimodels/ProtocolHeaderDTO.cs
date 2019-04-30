using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.apimodels
{
    public class ProtocolHeaderDTO
    {
        public decimal ProtocolHeaderCKey { get; set; }
        public decimal HeaderKey { get; set; }
        public int Namespace { get; set; }
        public string Title { get; set; }
        public string ProtocolName { get; set; }
        public string ProtocolGroup { get; set; }
        public string ProtocolVersion { get; set; }
        public string Subtitle { get; set; }
        public decimal ProtocolVersionCKey { get; set; }
        public DateTime? WebPostingDate { get; set; }
        public string BaseVersions { get; set; }
        public decimal ProtocolCKey { get; set; }
        public decimal ProtocolGroupCKey { get; set; }
        public decimal CreatedBy { get; set; }
        // NAA. Why?
        //public System.Data.Linq.Binary TS { get; set; }
        public decimal RecordCreateByCkey { get; set; }
    }
}
