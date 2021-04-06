﻿using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FoodFight.Domain.Models
{
    public partial class Restaurant : DomainObject
    {
        [JsonProperty("html_attributions")]
        public List<object> HtmlAttributions { get; set; }

        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("business_status")]
        public string BusinessStatus { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("plus_code")]
        public PlusCode PlusCode { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        [JsonProperty("user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public partial class Viewport
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }

    public partial class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
    }

    public partial class Photo
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributions { get; set; }

        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public partial class PlusCode
    {
        [JsonProperty("compound_code")]
        public string CompoundCode { get; set; }

        [JsonProperty("global_code")]
        public string GlobalCode { get; set; }
    }

    public partial class Restaurant
    {
        public static Restaurant FromJson(string json) => JsonConvert.DeserializeObject<Restaurant>(json, FoodFight.Domain.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Restaurant self) => JsonConvert.SerializeObject(self, FoodFight.Domain.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
