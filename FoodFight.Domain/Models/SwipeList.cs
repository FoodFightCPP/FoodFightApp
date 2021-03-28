using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodFight.Domain.Models
{
    public class SwipeList : DomainObject
    {
        [JsonProperty("swipeListId")]
        public Guid SwipeListId { get; set; }

        [JsonProperty("restaurantId")]
        public int RestaurantId { get; set; }

        [JsonProperty("matchSessionId")]
        public Guid MatchSessionId { get; set; }

        [JsonProperty("matchSession")]
        public MatchSession MatchSession { get; set; }

        [JsonProperty("restaurant")]
        public Restaurant Restaurant { get; set; }
    }
}
