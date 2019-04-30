using System;
using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Models
{
    public class ChecklistNoteViewModel
    {
        public String protocolCkey = String.Empty;
        public String nodeCkey = String.Empty;
        public List<ExplanatoryNote> notes = new List<ExplanatoryNote>();
        public List<ItemComment> comments = new List<ItemComment>();
        public List<ItemReference> references = new List<ItemReference>();
        public List<UserSimple> allUsers = new List<UserSimple>();
        public List<KeyValuePair<String, String>> standards = new List<KeyValuePair<String, String>>();
        public String[,] dataTypes = null;
        public String[,] dataUnits = null;
        public Int32 protocolVersionKey = 0;
        public Int32 commentsCount = 0; //SSP-136
    }
}