using Moq;
using Simplic.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Unity;
using Simplic.Package.Service.Install;
using System.Dynamic;

namespace Simplic.Package.Test
{
    public class CheckDependencyServiceTest
    {
        public static IEnumerable<object[]> CheckDependencyTestData
        {
            get
            {
                yield return new object[] {
                    new Dependency {
                        Version = new Version(1, 2, 3, 4),
                        GreaterAllowed = false,
                        PackageName = "SDF"},
                    new List<Version> {
                        new Version(1,2,3,4),
                        new Version(3,42,24),
                        new Version(1,2,34,5) 
                        },
                    true,
                    new Version(3,42,24),
                    "SDF"
                };
                yield return new object[] {
                    new Dependency {
                        Version = new Version(1, 2, 3, 5),
                        GreaterAllowed = false,
                        PackageName = "SDF"},
                    new List<Version> {
                        new Version(1,2,3,4),
                        new Version(3,42,24),
                        new Version(1,2,34,5)
                        },
                    false,
                    new Version(3,42,24),
                    "SDF"
                };
                yield return new object[] {
                    new Dependency {
                        Version = new Version(1, 2, 3, 5),
                        GreaterAllowed = true,
                        PackageName = "Test"},
                    new List<Version> {
                        new Version(1,2,3,4),
                        new Version(3,42,24),
                        new Version(1,2,34,5)
                        },
                    true,
                    new Version(3,42,24),
                    "Test"
                };
                yield return new object[] {
                    new Dependency {
                        Version = new Version(4, 2, 3, 4),
                        GreaterAllowed = true,
                        PackageName = "SDF"},
                    new List<Version>(),
                    false,
                    null,
                    "SDF"
                };
                yield return new object[] {
                    new Dependency {
                        Version = new Version(0, 2, 3, 4),
                        GreaterAllowed = true,
                        PackageName = "SDF"},
                    new List<Version>(),
                    false,
                    null,
                    "SDF"
                };
            }
        }

        [Theory]
        [MemberData(nameof(CheckDependencyTestData))]
        public async void Check_VersionNumber_Test(Dependency dependency, IEnumerable<Version> exisingVersionsList, bool expected, Version expectedVersion, string expectedName)
        {
            var container = DependencyInjectionHelper.GetContainer();

            var versionServiceMock = new Mock<IVersionRepository>();
            versionServiceMock.Setup(x => x.GetVersionsAsync(It.IsAny<string>())).Returns(Task.FromResult(exisingVersionsList));
            container.RegisterInstance<IVersionRepository>(versionServiceMock.Object);

            var service = container.Resolve<CheckDependencyService>();
            var result = await service.Check(dependency);

            Assert.Equal(expected, result.Exists);
            Assert.Equal(expectedName, result.Dependency.PackageName);
            Assert.Equal(expectedVersion, result.LatestExistingVersion);
        }
    }
}
