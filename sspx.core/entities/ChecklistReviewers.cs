using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class ChecklistReviewers
    {
        public int ReviewerId { get; set; }
        public string FirstName { get; set; }
        public string ReviewerName { get; set; }
        public bool Selected { get; set; }
        public string Qualifications { get; set; }
        public string Email { get; set; }
        public int ProtocolVersionUserRoleKey { get; set; }
        public string ProtocolName { get; set; }
    }

    public class LablelistAuthors
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string FirstName { get; set; }
        public string Qualifications { get; set; }
        public int ProtocolVersionUserRoleKey { get; set; }
        public string Email { get; set; }
        public string ProtocolName { get; set; }
    }
}
