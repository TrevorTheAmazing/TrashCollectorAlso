using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrashCollectorAlso.Models;
using Newtonsoft.Json.Linq;

namespace TrashCollectorAlso.TrashMaps
{
    public class TrashMap
    {
        //member variables
        HttpClient client = new HttpClient();
        ApplicationDbContext db;

        //constructor
        public TrashMap()
        {
            db = new ApplicationDbContext();
        }

        public async Task<string> GetAddressCoordinates(Customer customerIn)
        {
            string requestUrl = "https://api.opencagedata.com/geocode/v1/json?q=";
            string customerAddress = System.Web.HttpUtility.UrlEncode(
                customerIn.address1 + " " +
                customerIn.address2 + " " +
                customerIn.city + " " +
                customerIn.state + " " +
                customerIn.zip);
            string authenticationString = "&key=ca30af6d79bc423fa030b9916f599acf&language=en&pretty=1&no_annotations=1&limit=1";
            var response = await client.GetStringAsync(requestUrl + customerAddress + authenticationString);

            JObject mapInfo = JObject.Parse(response);

            customerIn.lat = (double)mapInfo["results"][0]["geometry"]["lat"];
            customerIn.lng = (double)mapInfo["results"][0]["geometry"]["lng"];

            db.SaveChanges();

            return "success";
        }
    }
}