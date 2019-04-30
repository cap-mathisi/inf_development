using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class Checklist
    {
        public decimal ChecklistCKey = DefaultValue.Ckey;
        public decimal ProtocolCKey = DefaultValue.Ckey;
        public decimal ProtocolVersionCKey = DefaultValue.Ckey;
        public string Name = String.Empty;
        public string VisibleText = String.Empty;
    }
}