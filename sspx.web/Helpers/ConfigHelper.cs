using System;

namespace sspx.web.Helpers
{
    public class ConfigHelper
    {
        public static int GetLockoutMaxFailedAttempts(string maxAttempts)
        {
            const int DEFAULT_MAX_FAILED_ATTEMPTS = 3;

            if(maxAttempts == null || Convert.ToInt32(maxAttempts) < 1)
            {
                return DEFAULT_MAX_FAILED_ATTEMPTS;
            }

            return Convert.ToInt32(maxAttempts);
        }

        public static TimeSpan GetIdentityTimeout(string timeout)
        {
            const int DEFAULT_TIMEOUT = 20;

            if (timeout == null || Convert.ToInt32(timeout) < 1)
            {
                return TimeSpan.FromMinutes(DEFAULT_TIMEOUT);
            }

            return TimeSpan.FromMinutes(Convert.ToInt32(timeout));
        }

        public static TimeSpan GetSessionTimeout(string timeout)
        {
            // pad session timeout with a little extra past the Identity expiration, in case it helps reduce need to null check
            return GetIdentityTimeout(timeout)
                .Add(TimeSpan.FromMinutes(5));
        }
    }
}
