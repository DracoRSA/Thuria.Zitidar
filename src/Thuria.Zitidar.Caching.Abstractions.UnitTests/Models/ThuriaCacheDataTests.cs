using NUnit.Framework;
using FluentAssertions;

using Thuria.Zitidar.Caching.Abstractions.Models;
using Thuria.Zitidar.Caching.Abstractions.UnitTests.TestData;

namespace Thuria.Zitidar.Caching.Abstractions.UnitTests.Models
{
    [TestFixture]
    public class ThuriaCacheDataTests
    {
        [Test]
        public void Constructor()
        {
            // Arrange
            

            // Act
            var thuriaCacheData = new ThuriaCacheData<TestFakeData>(DateTime.Now, new TestFakeData());

            // Assert
            thuriaCacheData.Should().NotBeNull();
        }
    }
}
