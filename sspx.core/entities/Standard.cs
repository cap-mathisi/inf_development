namespace sspx.core.entities
{
    public class Standard
    {
        public decimal BasedOnCkey = DefaultValue.Ckey;
        public int BasedOnKey = DefaultValue.Key;
        public int Namespace = DefaultValue.Namespace;
        public string BasedOn = string.Empty;
        public string Description = string.Empty;
        public int SortOrder = DefaultValue.SortOrder;
        public decimal CreatedBy = DefaultValue.Ckey;
        public decimal LastUpdated = DefaultValue.Ckey;
        public bool Active = DefaultValue.Active;
    }
}
