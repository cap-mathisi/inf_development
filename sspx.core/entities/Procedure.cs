using System;

namespace sspx.core.entities { 
	
	public class Procedure {
        public decimal ProcedureCkey = DefaultValue.Ckey;
        public int Namespace = DefaultValue.Namespace;
        public decimal DraftVersionCkey = DefaultValue.Ckey;
        public decimal BaseVersionCkey = DefaultValue.Ckey;
        public string ProcedureDetails = String.Empty;
        public decimal ProcedureVersionCkey = DefaultValue.Ckey;
        // ...
        public bool Active = DefaultValue.Active;
    }

}