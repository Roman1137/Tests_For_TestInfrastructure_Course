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

        public static string RunType => TestContext.Parameters[nameof(RunType)];
        public static int Timeout => Int32.Parse(TestContext.Parameters[nameof(Timeout)]);
        public static string IsHeadlessMode => TestContext.Parameters[nameof(IsHeadlessMode)];
        public static bool EnableVnc => Boolean.Parse(TestContext.Parameters[nameof(EnableVnc)]);
    }
}
