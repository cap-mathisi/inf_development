using System;

namespace sspx.core.entities
{
    // <Aggregate>Author/AuthorRoles
    public class Author
    {
        public decimal AuthorCKey = DefaultValue.Ckey;
        public string Name = String.Empty;
        public string Role = String.Empty;
        public decimal RoleCKey = DefaultValue.Ckey;
        public decimal ProtocolAuthorRoleCKey = DefaultValue.Ckey;
    }
}
