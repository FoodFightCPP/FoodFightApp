﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class ConnectedUser : DomainObject
    {
        [JsonProperty("connectedUserId")]
        public int ConnectedUserId { get; set; }

        [JsonProperty("baseUserId")]
        public int BaseUserId { get; set; }

        [JsonProperty("friendUserId")]
        public int FriendUserId { get; set; }

        [JsonProperty("baseUser")]
        public User BaseUser { get; set; }

        [JsonProperty("friendUser")]
        public User FriendUser { get; set; }
    }
}
