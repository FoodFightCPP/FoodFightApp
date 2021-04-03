using FoodFight.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodFight.DAL.Services
{
    public class GooglePlacesAPI<T> : IGooglePlacesAPI<T>
    {

        string BASEURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
        HttpClient client = new HttpClient();


        public async Task<T> GetAllLocations(string lat, string lng, int distance = 1610)
        {
            var json = await client.GetStringAsync(BASEURL + lat + ", " + lng + "&radius=" + distance + "&type=restaurant&key=AIzaSyCPS0P4Zq_bJt6Fb8ZiUSH6TR230KulOrs");
            var getEntities = JsonConvert.DeserializeObject<T>(json);
            return getEntities;
        }
    }
}
