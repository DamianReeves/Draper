using FluentAssertions;
using Xunit;

namespace Draper.Internals
{
    public class DefaultAssemblyTypeScannerTests
    {
        [Fact]
        public void Should_All_Types_Should_Include_Public_And_Internal_Types()
        {
            var results = AssemblyTypeScanners.AllTypesScanner(typeof (AssemblyTypeScanners).Assembly);
            results.Should()
                .Contain(typeof(AssemblyTypeScanners))
                .And.Contain(t=>t.Name == "Draper.Internals.ActionDisposable");

        }
    }
}
