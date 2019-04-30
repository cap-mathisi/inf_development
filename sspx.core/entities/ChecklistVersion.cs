using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class ChecklistVersion
    {
        public String key = String.Empty;
        public String protocolName = String.Empty;
        public String title = String.Empty;
        public String version = String.Empty;
        public DateTime webPostingDate;
        public String groupKey = String.Empty;
        public String groupName = String.Empty;
        public List<Int32> basedOnKey = new List<Int32>();
        public String cover = String.Empty;
        public String webPostingDateString = String.Empty;
        public List<EditorUser> users = new List<EditorUser>();
    }

    public class ChecklistVersionCommentsSummary
    {
        public Int32 Protocolkey = 0;
        public Int32 ProtocolVersionKey = 0;
        public String Commenttype = String.Empty;
        public Int32? CommentCount = 0;
    }
}
