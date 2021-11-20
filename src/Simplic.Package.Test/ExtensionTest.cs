using Moq;
using Simplic.Package.Service;
using System;
using Unity;
using Xunit;

namespace Simplic.Package.Test
{
    /// <summary>
    /// Class to test the extension system.
    /// </summary>
    public class ExtensionTest
    {
        /// <summary>
        /// Checks wherther the extension loader executes and throws no errors.
        /// </summary>
        [Fact]
        public void LoadExtension_LaodsExtension()
        {
            var errorCounter = 0;

            // Setup log service to increase the error counter each time a error is logged.
            var logService = new Mock<ILogService>();
            logService.Setup(x => x.WriteAsync(
                    It.IsAny<string>(),
                    It.Is<LogLevel>(y => y == LogLevel.Error),
                    It.IsAny<Exception>()))
                .Callback(() =>
                {
                    errorCounter++;
                });

            var container = new UnityContainer();

            var service = new ExtensionService(logService.Object, container);
            service.LoadExtensions(new Package
            {
                Extensions = new[]
                {
                    "Simplic.Package.Test.Extension.dll"
                }
            });

            Assert.Equal(0, errorCounter);
        }
    }
}
