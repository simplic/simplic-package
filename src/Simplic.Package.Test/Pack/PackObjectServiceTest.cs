using Moq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Simplic.Package.Test
{
    public class PackObjectServiceTest
    {
        // Potentially test various files with encoding special cases
        [Theory]
        [InlineData("sql")]
        [InlineData("repository", "$^@#*$(@#&$(!^#($KHJSF")]
        [InlineData("grid")]
        public async Task ReadAsync_ReadingFromFile_Test(string objectType, string fileText = "Select * from test")
        {
            var container = DependencyInjectionHelper.GetContainer();

            var objectListItem = new ObjectListItem
            {
                Source = "",
                Target = "Target"
            };

            var fileService = new Mock<IFileService>();
            var fileBytes = Encoding.Default.GetBytes(fileText);

            fileService.Setup(x => x.ReadAllBytesAsync(It.IsAny<string>())).Returns(Task.FromResult(fileBytes));
            container.RegisterInstance<IFileService>(fileService.Object);

            var packObjectService = container.Resolve<IPackObjectService>(objectType);
            var packObjectResult = await packObjectService.ReadAsync(objectListItem);

            Assert.Equal(fileBytes, packObjectResult.File);
            Assert.Equal(fileText, Encoding.Default.GetString(packObjectResult.File));
            Assert.Equal(objectListItem.Target, packObjectResult.Location);
        }
    }
}