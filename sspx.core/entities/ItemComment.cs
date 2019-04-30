using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class ItemComment
    {
        public Int32 key = 0;
        public Int32 commentId = 0;
        public string comment = String.Empty;
        public string title = String.Empty;
        public string firstName = String.Empty;
        public string lastName = String.Empty;
        public DateTime dateAdded = DateTime.MinValue;
        public Int32 commentType = 8;
        public string dateString = String.Empty;
        public int roleKey = 0; //SSP-136
    }

    public enum ProtocolCommentKey
    {
        BASE = 0,
        TITLE = 1,
        SUBTITLE = 2,
        COVER = 3,
        AUTHORS = 4,
        CASESUMMARY = 5,
        EXPLANATORYNOTE = 6,
        REFERENCES = 7,
        UNKNOWN = 8
    }
}
