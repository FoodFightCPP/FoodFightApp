using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class AcceptedRestaurant : DomainObject
    {
        [JsonProperty("acceptedRestaurantId")]
        public int AcceptedRestaurantId { get; set; }

        [JsonProperty("swipeListId")]
        public int SwipeListId { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("swipeList")]
        public SwipeList SwipeList { get; set; }
    }
}
