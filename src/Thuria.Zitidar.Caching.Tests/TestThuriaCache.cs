using System;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;
using FluentAssertions;
using NSubstitute;
using Thuria.Calot.TestUtilities;
using Thuria.Zitidar.Core.Cache;

namespace Thuria.Zitidar.Caching.Tests
{
  [TestFixture]
  public class TestThuriaCache
  {
    [Test]
    public void Constructor()
    {
      //---------------Set up test pack-------------------
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var thuriaCache = new ThuriaCache<string>();
      //---------------Test Result -----------------------
      thuriaCache.Should().NotBeNull();
    }

    [Ignore("Need to fix RandomValueGenerator")] //TODO Johan Dercksen 26 Dec 2022: Ignored Test - Need to fix RandomValueGenerator
    [Test]
    public void Constructor_GivenZeroOrNegativeExpiryInSeconds_ShouldThrowArgumentException()
    {
      //---------------Set up test pack-------------------
      var expiryInSeconds = RandomValueGenerator.CreateRandomInt(-100, -1);
      Console.WriteLine($"Expiry in Seconds: {expiryInSeconds}");
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var argumentException = Assert.Throws<ArgumentException>(() => new ThuriaCache<string>(expiryInSeconds));
      //---------------Test Result -----------------------
      argumentException.ParamName.Should().Be("expiryInSeconds");
    }

    [Test]
    public void Constructor_GivenValue_ShouldSetSetPropertyValue()
    {
      //---------------Set up test pack-------------------
      var expiryInSeconds = RandomValueGenerator.CreateRandomInt(1, 10000);
      var thuriaCache     = new ThuriaCache<string>(expiryInSeconds);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var objectExpiry = thuriaCache.ExpiryInSeconds;
      //---------------Test Result -----------------------
      objectExpiry.Should().Be(expiryInSeconds);
    }

    [Test]
    public void ExistsAsync_GivenNullCacheKey_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      var thuriaCache = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.ThrowsAsync<ArgumentNullException>(() => thuriaCache.ExistsAsync(null));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("cacheKey");
    }

    [Test]
    public async Task ExistsAsync_GivenDataDoesNotExist_ShouldReturnFalse()
    {
      //---------------Set up test pack-------------------
      var cacheKey    = RandomValueGenerator.CreateRandomString(5, 10);
      var thuriaCache = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataExist = await thuriaCache.ExistsAsync(cacheKey);
      //---------------Test Result -----------------------
      dataExist.Should().BeFalse();
    }

    [Test]
    public async Task ExistsAsync_GivenDataExists_And_NotExpired_ShouldReturnTrue()
    {
      //---------------Set up test pack-------------------
      var cacheKey    = RandomValueGenerator.CreateRandomString(5, 10);
      var cacheData   = RandomValueGenerator.CreateRandomString(20, 50);
      var thuriaCache = new ThuriaCache<string>();

      await thuriaCache.UpsertAsync(cacheKey, new ThuriaCacheData<string>(cacheData));
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataExist = await thuriaCache.ExistsAsync(cacheKey);
      //---------------Test Result -----------------------
      dataExist.Should().BeTrue();
    }

    [Test]
    public async Task ExistsAsync_GivenDataExists_And_Expired_ShouldReturnFalse()
    {
      //---------------Set up test pack-------------------
      var cacheKey        = RandomValueGenerator.CreateRandomString(5, 10);
      var cacheData       = RandomValueGenerator.CreateRandomString(20, 50);
      var thuriaCache     = new ThuriaCache<string>();
      var thuriaCacheData = new ThuriaCacheData<string>(cacheData, DateTime.UtcNow.AddMilliseconds(10));

      await thuriaCache.UpsertAsync(cacheKey, thuriaCacheData, false);
      Thread.Sleep(200);
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var dataExist = await thuriaCache.ExistsAsync(cacheKey);
      //---------------Test Result -----------------------
      dataExist.Should().BeFalse();
    }

    [Test]
    public void Upsert_GivenNullCacheKey_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      var thuriaCacheData = new ThuriaCacheData<string>("Test");
      var thuriaCache     = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.ThrowsAsync<ArgumentNullException>(() => thuriaCache.UpsertAsync(null, thuriaCacheData));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("cacheKey");
    }

    [Test]
    public void Upsert_GivenNullCacheValue_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      var thuriaCache = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.ThrowsAsync<ArgumentNullException>(() => thuriaCache.UpsertAsync("Test", null));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("cacheValue");
    }

    [Test]
    public async Task Upsert_GivenValidData_And_CacheDataDoesNotExist_ShouldAddCacheDataToCache_And_ReturnTrue()
    {
      //---------------Set up test pack-------------------
      var cacheKey        = RandomValueGenerator.CreateRandomString(5, 10);
      var cacheData       = RandomValueGenerator.CreateRandomString(20, 50);
      var thuriaCacheData = new ThuriaCacheData<string>(cacheData);
      var thuriaCache     = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var upsertResult = await thuriaCache.UpsertAsync(cacheKey, thuriaCacheData);
      //---------------Test Result -----------------------
      upsertResult.Should().BeTrue();

      var returnedData = await thuriaCache.GetAsync(cacheKey);
      returnedData.Should().Be(cacheData);
    }

    [Test]
    public async Task Upsert_GivenValidData_And_CacheDataExists_ShouldUpdateCacheDataInCache_And_ReturnTrue()
    {
      //---------------Set up test pack-------------------
      var cacheKey         = RandomValueGenerator.CreateRandomString(5, 10);
      var cacheData1       = RandomValueGenerator.CreateRandomString(20, 50);
      var cacheData2       = RandomValueGenerator.CreateRandomString(20, 50);
      var thuriaCacheData1 = new ThuriaCacheData<string>(cacheData1);
      var thuriaCacheData2 = new ThuriaCacheData<string>(cacheData2);
      var thuriaCache      = new ThuriaCache<string>();
      var upsertResult1    = await thuriaCache.UpsertAsync(cacheKey, thuriaCacheData1);
      //---------------Assert Precondition----------------
      upsertResult1.Should().BeTrue();
      //---------------Execute Test ----------------------
      var upsertResult2 = await thuriaCache.UpsertAsync(cacheKey, thuriaCacheData2);
      //---------------Test Result -----------------------
      upsertResult2.Should().BeTrue();

      var returnedData = await thuriaCache.GetAsync(cacheKey);
      returnedData.Should().Be(cacheData2);
    }

    [Test]
    public async Task Upsert_GivenValidData_And_SetCacheExpiryIsFalse_ShouldNotUpdateCacheExpiry()
    {
      //---------------Set up test pack-------------------
      DateTime? dataExpiry   = null;
      var cacheKey           = RandomValueGenerator.CreateRandomString(5, 10);
      var thuriaCacheData    = Substitute.For<IThuriaCacheData<string>>();
      thuriaCacheData.Expiry = Arg.Do<DateTime>(time => dataExpiry = time);
      var thuriaCache        = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var upsertResult = await thuriaCache.UpsertAsync(cacheKey, thuriaCacheData, false);
      //---------------Test Result -----------------------
      upsertResult.Should().BeTrue();
      dataExpiry.Should().BeNull();
    }

    [Test]
    public void GetAsync_GivenNullCacheKey_ShouldThrowArgumentNullException()
    {
      //---------------Set up test pack-------------------
      var thuriaCache = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var exception = Assert.ThrowsAsync<ArgumentNullException>(() => thuriaCache.GetAsync(null));
      //---------------Test Result -----------------------
      exception.ParamName.Should().Be("cacheKey");
    }

    [Test]
    public async Task GetAsync_GivenDataDoesNotExist_ShouldReturnNull()
    {
      //---------------Set up test pack-------------------
      var cacheKey    = RandomValueGenerator.CreateRandomString(5, 10);
      var thuriaCache = new ThuriaCache<string>();
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var cacheDataReturned = await thuriaCache.GetAsync(cacheKey);
      //---------------Test Result -----------------------
      cacheDataReturned.Should().BeNull();
    }

    [Test]
    public async Task GetAsync_GivenDataExists_ShouldReturnData()
    {
      //---------------Set up test pack-------------------
      var cacheKey    = RandomValueGenerator.CreateRandomString(5, 10);
      var cacheData   = RandomValueGenerator.CreateRandomString(20, 50);
      var thuriaCache = new ThuriaCache<string>();

      await thuriaCache.UpsertAsync(cacheKey, new ThuriaCacheData<string>(cacheData));
      //---------------Assert Precondition----------------
      //---------------Execute Test ----------------------
      var cacheDataReturned = await thuriaCache.GetAsync(cacheKey);
      //---------------Test Result -----------------------
      cacheDataReturned.Should().NotBeNullOrWhiteSpace();
      cacheDataReturned.Should().Be(cacheData);
    }
  }
}
