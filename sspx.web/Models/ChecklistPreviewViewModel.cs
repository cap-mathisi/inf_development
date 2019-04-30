using System;
using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Models
{
    public class ChecklistPreviewViewModel
    {
        public String nodeCkey = String.Empty;
        public String versionCkey = String.Empty;
        public String view = String.Empty;
        public ChecklistVersion version = null;
        public List<ChecklistItemNode> items = new List<ChecklistItemNode>();
        public List<ExplanatoryNote> notes = new List<ExplanatoryNote>();
        public List<ItemReference> references = new List<ItemReference>();
    }
}