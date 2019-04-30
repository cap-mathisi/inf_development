using System.Collections.Generic;

namespace sspx.web.Models
{
    public class ProtocolIndexModel
    {
        public int AllProtocolsCount { get; set; }
        public int PrimaryAuthorCount { get; set; }
        public int ReviewerPanelCount { get; set; }
        public int AuthorPanelCount { get; set; }
        public List<ProtocolIndexModelItem> Items { get; set; } = new List<ProtocolIndexModelItem>();
    }
}
