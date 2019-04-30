using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{

    public class VersionComments
    {
        public string Version = string.Empty;
        public string ReviewerName = string.Empty;
        public int? ReviewerID = 0;
        public int ProtocolVersionsKey = 0;
        //public List<Reviewer> Reviewers;
        public int CommentsCount = 0;
    }

    public class Reviewer
    {
        public string ReviewerName = string.Empty;
        public int? ReviewerID = 0;
    }
}