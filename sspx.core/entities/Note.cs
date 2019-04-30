using System;

namespace sspx.core.entities
{
    // NAA. aka Explanatory Note
    public class Note
    {
        public decimal NoteCkey = DefaultValue.Ckey;
        public int NoteKey = DefaultValue.Key;
        public int Namespace = DefaultValue.Namespace;
        public decimal DraftVersion = DefaultValue.Ckey;
        public decimal BaseVersion = DefaultValue.Ckey;
        public string NoteNumber = String.Empty;
        public string NoteTitle = String.Empty;
        //public string NoteDetails = String.Empty; // NAA.  This is binary?
        // public string NoteDetails_html
        // public string NoteDetails_OpenXML
        // public string NoteDetails_Doc
        public decimal ProtocolVersionCkey = DefaultValue.Ckey;
        public bool? Active = DefaultValue.Active;
    }
}