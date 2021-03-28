using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class RejectedRestaurant : DomainObject
    {
        [JsonProperty("rejectedRestaurantId")]
        public Guid RejectedRestaurantId { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("swipeListId")]
        public Guid SwipeListId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("swipeList")]
        public SwipeList SwipeList { get; set; }
    }
}
