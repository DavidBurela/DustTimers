using System;
using System.IO;
using System.Threading.Tasks;
using DustTimers.LegacyApi.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DustTimers.LegacyApi.Tests.Resources
{
    [TestClass]
    public class CorporationTests
    {
        [TestMethod]
        public void CanParseXml()
        {
            // Arrange
            var a = new FileStream(@"SampleData\CorporationSheet_TSOLE.xml", FileMode.Open);
            var filereader = new System.IO.StreamReader(a);
            var str = filereader.ReadToEnd();


            // Act
            var corporation = CorporationResource.ParseCorporation(str);

            // Assert
            Assert.AreEqual("TSOLE", corporation.Ticker);
        }


        [TestMethod]
        public async Task CanDownloadAndParse()
        {
            // Arrange
            
            // Act
            var corporation = await CorporationResource.GetCorporation(98145683); // TSOLE

            // Assert
            Assert.AreEqual("TSOLE", corporation.Ticker);
        }
    }
}
