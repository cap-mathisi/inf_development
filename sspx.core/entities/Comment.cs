using System;

namespace sspx.core.entities
{
    public class Comment
    {
        public int Id = DefaultValue.Id;
        public decimal ProtocolVersionCKey = DefaultValue.Ckey;
        public decimal UserCKey = DefaultValue.Ckey;
        public string UserId = String.Empty;
        public string UserComment = String.Empty;
        public string ReviewItem = String.Empty;
        public decimal ReviewItemCKey = DefaultValue.Ckey;
        // NAA. Need to standardize on .NET DateTime format <-> smalldatetime
        //public string DateAdded { get; set; }
        public string DraftVersion = String.Empty;
    }
}
