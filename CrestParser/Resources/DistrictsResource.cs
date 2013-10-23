using CrestParser.Helpers;
using CrestParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrestParser.Resources
{
    public class DistrictsResource
    {
        public static async Task<List<District>> GetDistricts()
        {
            var httpClient = HttpClientExtensions.CreateGzipEnabledClient();
            // TODO: this needs to be an app key
            var districtsJson = await httpClient.GetStringAsync("http://public-crest.eveonline.com/districts/");
            //var districtsJson = await httpClient.GetStringAsync("http://localhost:41197/SampleData/Districts-201310172200.txt");
            
            return ParseDistricts(districtsJson);
        }

        public static List<District> ParseDistricts(string districtsJson)
        {
            var rootObject = JsonConvert.DeserializeObject<DistrictRootObject>(districtsJson);

            return rootObject.Items;
        }
    }
}
