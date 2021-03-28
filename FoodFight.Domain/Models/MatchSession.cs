using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class MatchSession : DomainObject 
    {
        [JsonProperty("matchSessionId")]
        public Guid MatchSessionId { get; set; }

        [JsonProperty("connectedUserId")]
        public Guid ConnectedUserId { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }

        [JsonProperty("connectedUser")]
        public ConnectedUser ConnectedUser { get; set; }
    }
}
