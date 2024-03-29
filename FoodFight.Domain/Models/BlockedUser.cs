﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class BlockedUser : DomainObject
    {
        [JsonProperty("blockUserId")]
        public int BlockUserId { get; set; }

        [JsonProperty("baseUserId")]
        public int BaseUserId { get; set; }

        [JsonProperty("blockedUserId")]
        public int BlockedUserId { get; set; }

        [JsonProperty("baseUser")]
        public User BaseUser { get; set; }

        [JsonProperty("blockedUserNavigation")]
        public User BlockedUserNavigation { get; set; }
    }
}
