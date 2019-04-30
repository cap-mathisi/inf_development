using System;

namespace sspx.core.entities
{
    public class NoteComment
    {
        public int NoteCommentId = DefaultValue.Id;
        public int Namespace = DefaultValue.Namespace;
        public decimal NoteCommentCkey = DefaultValue.Ckey;
        public string Comment = String.Empty;
        public decimal UserCkey = DefaultValue.Ckey;
        //public string DateAdded { get; set; }
    }
}