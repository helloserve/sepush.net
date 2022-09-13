using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.SePush.Tests
{
    [TestClass]
    public class SePushClientTests
    {
        private SePushClient client;
        private SePushOptions options = new SePushOptions();
        private Mock<IOptionsMonitor<SePushOptions>> optionsMock = new Mock<IOptionsMonitor<SePushOptions>>();
        private Mock<ILogger<SePushClient>> loggerMock = new Mock<ILogger<SePushClient>>();

        [TestInitialize]
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .AddUserSecrets("8fe41d60-01bc-4460-bf18-74b5b06e9850")
                .Build();

            options.Token = config["SePushOptions:Token"];

            optionsMock.SetupGet(x => x.CurrentValue).Returns(options);

            client = new SePushClient(optionsMock.Object, loggerMock.Object);
        }

        [TestMethod]
        public async Task ShoulsGetStatus()
        {
            //act
            var result = await client.StatusAsync();

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ShouldGetAreaInformation()
        {
            //assert
            string id = "eskde-10-fourwaysext10cityofjohannesburggauteng";

            //act
            var result = await client.AreaInformationAsync(id, testMode: "current");

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Schedule.Days[0].Stages.Count > 0);
        }

        [TestMethod]
        public async Task ShouldFindNearbyAreas()
        {
            //arrange
            double latitude = 26.0269658;
            double longitude = 28.0137339;

            //act
            var result = await client.AreasNearbyAsync(latitude, longitude);

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ShouldSearchArea()
        {
            //arrange
            string searchText = "cape town";

            //act
            var result = await client.AreasSearchAsync(searchText);

            //assert
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public async Task ShouldFindNearbyTopics()
        {
            //arrange
            double latitude = 26.0269658;
            double longitude = 28.0137339;

            //act
            var result = await client.TopicsNearbyAsync(latitude, longitude);

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ShouldCheckAllowance()
        {
            //act
            var result = await client.CheckAllowanceAsync();

            //assert
            Assert.IsNotNull(result);
        }
    }
}
