﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class BlockedRestaurant : DomainObject
    {
        [JsonProperty("blockedRestaurantId")]
        public int BlockedRestaurantId { get; set; }

        [JsonProperty("restaurantId")]
        public int RestaurantId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("restaurant")]
        public Restaurant Restaurant { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
