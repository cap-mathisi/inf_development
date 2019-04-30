using System;

namespace sspx.web.Helpers
{
    public static class InMemoryDataToggle
    {
        public static bool UseInMemory(string MasterToggle, string SecondaryToggle)
        {
            if(string.Equals(MasterToggle, "true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (string.Equals(MasterToggle, "false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return string.Equals(SecondaryToggle, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
