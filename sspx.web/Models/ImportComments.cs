using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sspx.web.Models
{
    public class ImportComments
    {
        public int ProtocolVersionsKey { get; set; }
        public string Version { get; set; }
        public List<Reviewer> Reviewers { get; set; }
        public int Comments { get; set; }
    }

    public class Reviewer {
        public string ReviewerName { get; set; }
        public int? ReviewerID { get; set; }
        public int ReviewerComments { get; set; }
    }

}