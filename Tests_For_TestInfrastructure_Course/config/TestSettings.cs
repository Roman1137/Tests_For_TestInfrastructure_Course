using System;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.config
{
    public static class TestSettings
    {
        public static Uri ToDoApplicationUrl
        {
            get
            {
                var url = TestContext.Parameters[nameof(ToDoApplicationUrl)]
                          ??
                          Environment.GetEnvironmentVariable(nameof(ToDoApplicationUrl));
                return new Uri(url);
            }
        }

        public static Uri SeleniumClusterUrl
        {
            get
            {
                var url = TestContext.Parameters[nameof(SeleniumClusterUrl)]
                          ??
                          Environment.GetEnvironmentVariable(nameof(SeleniumClusterUrl));
                return new Uri(url);
            }
        }

        public static bool IsLocalBrowser => Boolean.Parse(TestContext.Parameters[nameof(IsLocalBrowser)]);
        public static string Browser => TestContext.Parameters[nameof(Browser)];
        public static string Timeout => TestContext.Parameters[nameof(Timeout)];
        public static string IsHeadlessMode => TestContext.Parameters[nameof(IsHeadlessMode)];
    }
}
