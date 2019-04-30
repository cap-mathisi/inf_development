namespace sspx.core.entities
{
    public class Qualification
    {
        public int QualificationKey = DefaultValue.Key;
        public string QualificationTxt = string.Empty;
        public string Description = string.Empty;
        public decimal CreatedBy = DefaultValue.Ckey;
        public decimal LastUpdated = DefaultValue.Ckey;
        public bool Active = DefaultValue.Active;
    }
}
