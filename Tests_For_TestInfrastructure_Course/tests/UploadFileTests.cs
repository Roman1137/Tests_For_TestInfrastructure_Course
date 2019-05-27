using NUnit.Allure.Core;
using NUnit.Framework;

namespace Tests_For_TestInfrastructure_Course.tests
{
    [TestFixture]
    [AllureNUnit]
    [TestFixture, Parallelizable(ParallelScope.Fixtures)]
    public class UploadFileTests: BaseTest
    {
        [Test]
        public void Should_Upload_File()
        {
            App.FileuploadPage.Open();
            App.FileuploadPage.SelectPicture();
            App.FileuploadPage.Upload();
            App.FileuploadPage.WaitUntilDownloaded();
        }
    }
}
