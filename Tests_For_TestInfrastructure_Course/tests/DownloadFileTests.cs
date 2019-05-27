using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    [AllureNUnit]
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class DownloadFileTests:BaseTest
    {
        [Test]
        public void Selenoid_Should_Donwload_File()
        {
            App.FiledownloaderPage.Open();
            App.FiledownloaderPage.DownloadFirstItem();
        }
    }
}
