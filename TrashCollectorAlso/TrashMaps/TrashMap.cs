using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrashCollectorAlso.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrashCollectorAlso.TrashMaps
{
    public class TrashMap
    {
        //member variables
        HttpClient client = new HttpClient();
        ApplicationDbContext db;
        public double Lat;
        public double Lng;

        //constructor
        public TrashMap(/*string customerAddress*/)
        {
            //GetAddressCoordinates(customerAddress);
            //GetTrashLocation(coordinates);
            db = new ApplicationDbContext();
        }

        //private async Task GetAddressCoordinates(string customerAddressIn)
        //public async Task GetAddressCoordinates(string customerAddressIn)
        //public async Task GetAddressCoordinates(Customer customerIn)
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
            //string response = await client.GetStringAsync(requestUrl + customerAddress + authenticationString);
            var response = await client.GetStringAsync(requestUrl + customerAddress + authenticationString);

            JObject mapInfo = JObject.Parse(response);

            //this.Lat = (double)mapInfo["results"][0]["geometry"][0]["lat"];
            //this.Lng = (double)mapInfo["results"][0]["geometry"][0]["long"];
            //var coordinates = mapInfo["results"][0]["geometry"][0]["long"].Children();
            //this.Lat = (double)mapInfo["results"][0]["geometry"]["lat"];
            //this.Lng = (double)mapInfo["results"][0]["geometry"]["lng"];

            customerIn.lat = (double)mapInfo["results"][0]["geometry"]["lat"];
            customerIn.lng = (double)mapInfo["results"][0]["geometry"]["lng"];

            db.SaveChanges();

            //var coordinatesResponse = JsonConvert.DeserializeObject<>(response);

            //GetTrashLocation();
            //this.coordinates = response;


            //Console.WriteLine(response);
            //Console.ReadLine();
            return "success";
        }

        private async Task GetTrashLocation(/*string customerCoordinates*/)
        {
            string requestUrl = "https://maps.googleapis.com/maps/api/js?key=";
            string authenticationString = "AIzaSyC0Jey32q_0fW8xGijS8Gg7HMvVofnNLdU";
            //string response = await client.GetStringAsync(requestUrl + customerCoordinates + authenticationString + "&callback=initMap");

            //Console.WriteLine(response);
            //Console.ReadLine();
        }
    }




}