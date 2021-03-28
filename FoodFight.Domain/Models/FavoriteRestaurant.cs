using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class FavoriteRestaurant : DomainObject
    {
        [JsonProperty("favoriteRestaurantId")]
        public Guid FavoriteRestaurantId { get; set; }

        [JsonProperty("restaurantId")]
        public int RestaurantId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("restaurant")]
        public Restaurant Restaurant { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
