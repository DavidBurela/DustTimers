using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DustTimers.LegacyApi.Helpers;
using DustTimers.LegacyApi.Models;

namespace DustTimers.LegacyApi.Resources
{
    public class CorporationResource
    {
        public static async Task<Corporation> GetCorporation(int Id)
        {
            const string corporationApiUrl = "https://api.eveonline.com/corp/CorporationSheet.xml.aspx?corporationID={0}";
            var corporationUrl = string.Format(corporationApiUrl, Id);

            var httpClient = HttpClientExtensions.CreateGzipEnabledClient();
            var corporationXml = await httpClient.GetStringAsync(corporationUrl);

            return ParseCorporation(corporationXml);
        }

        public static Corporation ParseCorporation(string corporationXml)
        {
            var corporation = new Corporation();
            var xml = XElement.Parse(corporationXml);
            var resultNode = xml.Element("result");

            if (resultNode != null)
            {
                var idNode = resultNode.Element("corporationID");
                if (idNode == null)
                    throw new NullReferenceException();
                corporation.Id = Convert.ToInt32(resultNode.Element("corporationID").Value);

                var corpNameNode = resultNode.Element("corporationName");
                corporation.CorporationName = corpNameNode == null ? string.Empty : corpNameNode.Value;

                var ceoIdNode = resultNode.Element("ceoID");
                corporation.CeoId = ceoIdNode == null ? 0 : Convert.ToInt32(ceoIdNode.Value);

                var ceoNameNode = resultNode.Element("ceoName");
                corporation.CeoName = ceoNameNode == null ? string.Empty : ceoNameNode.Value;

                var tickerNode = resultNode.Element("ticker");
                corporation.Ticker = tickerNode == null ? string.Empty : tickerNode.Value;

                var memberCountNode = resultNode.Element("memberCount");
                corporation.MemberCount = memberCountNode == null ? 0 : Convert.ToInt32(memberCountNode.Value);

                var allianceIdNode = resultNode.Element("allianceID");
                corporation.AllianceId = allianceIdNode == null ? 0 : Convert.ToInt32(allianceIdNode.Value);

                var allianceNameNode = resultNode.Element("allianceName");
                corporation.AllianceName = allianceNameNode == null ? string.Empty : allianceNameNode.Value;

            }
            return corporation;
        }
    }
}
