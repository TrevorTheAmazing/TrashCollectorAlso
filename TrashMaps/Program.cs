using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrashMaps
{
    class Program
    {
        HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.GetTodoItems();
        }

        private async Task GetTodoItems()
        {
            string response = await client.GetStringAsync(
                "https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initMap");

            Console.WriteLine(response);
        }
    }
}
