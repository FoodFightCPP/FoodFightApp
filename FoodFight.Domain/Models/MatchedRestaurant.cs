using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodFight.Domain.Models
{
    public class MatchedRestaurant : DomainObject
    {
        [JsonProperty("matchRestaurantId")]
        public Guid MatchRestaurantId { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("acceptedRestaurantId")]
        public Guid AcceptedRestaurantId { get; set; }

        [JsonProperty("acceptedRestaurant")]
        public AcceptedRestaurant AcceptedRestaurant { get; set; }
    }
}
