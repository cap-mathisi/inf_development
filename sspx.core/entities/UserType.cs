using System;

namespace sspx.core.entities
{
    public class UserType
    {
        public int UserTypeKey = DefaultValue.Key;
        public int NamespaceKey = DefaultValue.Namespace;
        public string Type = String.Empty;
        public string Description = String.Empty;
        public decimal SortOrder = DefaultValue.Ckey;
        public bool Active = DefaultValue.Active;
    }
}
