using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace FoodFight.Domain.Models
{
    public class UserSetting : DomainObject
    {
        [JsonProperty("userSettingsId")]
        public Guid UserSettingsId { get; set; }

        [JsonProperty("settingsId")]
        public Guid SettingsId { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("settings")]
        public Setting Settings { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
