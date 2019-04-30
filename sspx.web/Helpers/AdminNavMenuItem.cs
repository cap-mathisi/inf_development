using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Helpers
{
    // CS2 based on https://ardalis.com/enum-alternatives-in-c
    public class AdminNavMenuItem
    {
        public static AdminNavMenuItem Select { get; } = new AdminNavMenuItem("Select", "/Index");
        public static AdminNavMenuItem Protocol { get; } = new AdminNavMenuItem("Protocol", "/Protocol");
        public static AdminNavMenuItem Protocol_CaseSummary { get; } = new AdminNavMenuItem("Protocol", "/ProtocolCaseSummary");
        public static AdminNavMenuItem ProtocolGroup { get; } = new AdminNavMenuItem("Protocol Group", "/ProtocolGroup");
        public static AdminNavMenuItem Qualification { get; } = new AdminNavMenuItem("Qualification", "/Qualification");
        public static AdminNavMenuItem Standard { get; } = new AdminNavMenuItem("Standard", "/Standard");

        public string DisplayText { get; private set; }
        public string PagePath { get; private set; }

        private AdminNavMenuItem(string displayText, string pagePath)
        {
            DisplayText = displayText;
            PagePath = pagePath;
        }

        private static IEnumerable<AdminNavMenuItem> List()
        {
            return new[] { Select, Protocol, Protocol_CaseSummary, ProtocolGroup, Qualification, Standard };
        }

        public static AdminNavMenuItem FromPagePath(string pagePath)
        {
            return List().Single( m =>
                string.Equals(m.PagePath, pagePath, System.StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
