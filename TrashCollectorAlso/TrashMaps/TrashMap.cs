using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrashCollectorAlso.TrashMaps
{
    public class TrashMap
    {
        //member variables
        HttpClient client = new HttpClient();
        private string coordinates;
        public string Coordinates { get; set; }

        //constructor
        public TrashMap(string customerAddress)
        {
            GetAddressCoordinates(customerAddress);
            GetTrashLocation(coordinates);
        }

        private async Task GetAddressCoordinates(string customerAddressIn)
        {
            string requestUrl = "https://api.opencagedata.com/geocode/v1/json?q=";
            string customerAddress = System.Web.HttpUtility.UrlEncode(customerAddressIn);
            string authenticationString = "&key=ca30af6d79bc423fa030b9916f599acf&language=en&pretty=1";
            string response = await client.GetStringAsync(requestUrl + customerAddress + authenticationString);
            this.coordinates = response;

            //address param string
            //"313 n. plankinton ave, milwaukee, wi 53203"
            //313 %20 n. %20 plankinton %20 ave %2C %20 milwaukee %2C %20 wi %20 53203 
            //313%20n.%20plankinton%20ave%2C%20milwaukee%2C%20wi%2053203&key=ca30af6d79bc423fa030b9916f599acf&language=en&pretty=1

            Console.WriteLine(response);
            Console.ReadLine();
        }

        private async Task GetTrashLocation(string customerCoordinates)
        {
            string requestUrl = "https://maps.googleapis.com/maps/api/js?key=";
            string authenticationString = "AIzaSyC0Jey32q_0fW8xGijS8Gg7HMvVofnNLdU";
            string response = await client.GetStringAsync(requestUrl + customerCoordinates + authenticationString + "&callback=initMap");
//            this.coordinates = response;
           
            Console.WriteLine(response);
            Console.ReadLine();
        }
    }
}