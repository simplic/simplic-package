using Moq;
using Simplic.Package.Service;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Simplic.Package.Test
{
    public class PackSqlServiceTest
    {
        // Potentially test various sqlFiles with encoding special cases
        [Fact]
        public async Task ReadAsync_ReadingFromFile_Test()
        {
            var container = new UnityContainer();
            container.RegisterType<IPackObjectService, PackSqlService>("sql");

            var objectListItem = new ObjectListItem
            {
                Source = "",
                Target = "Target"
            };

            var fileService = new Mock<IFileService>();
            var sqlFile = "Select * from test";
            var sqlFileBytes = Encoding.Default.GetBytes(sqlFile);

            fileService.Setup(x => x.ReadAllBytesAsync(It.IsAny<string>())).Returns(Task.FromResult(sqlFileBytes));

            container.RegisterInstance<IFileService>(fileService.Object);

            var packSqlService = container.Resolve<IPackObjectService>("sql");

            var packObjectResult = await packSqlService.ReadAsync(objectListItem);

            Assert.Equal(sqlFileBytes, packObjectResult.File);
            Assert.Equal(sqlFile, Encoding.Default.GetString(packObjectResult.File));
            Assert.Equal($"sql/{objectListItem.Target}", packObjectResult.Location);
        }
    }
}