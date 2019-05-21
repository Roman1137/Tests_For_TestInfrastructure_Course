using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.config
{
    public static class TestSettings
    {
        public static Uri ToDoApplicationUrl { get; set; } = new Uri(TestContext.Parameters[nameof(ToDoApplicationUrl)]);
        public static Uri SeleniumGridUrl { get; set; } = new Uri(TestContext.Parameters[nameof(SeleniumGridUrl)]);
        public static bool IsLocalBrowser { get; set; } = Boolean.Parse(TestContext.Parameters[nameof(IsLocalBrowser)]);
        public static string Browser { get; set; } = TestContext.Parameters[nameof(Browser)];
        public static string Timeout { get; set; } = TestContext.Parameters[nameof(Timeout)];
        public static string IsHeadlessMode { get; set; } = TestContext.Parameters[nameof(IsHeadlessMode)];
    }
}
