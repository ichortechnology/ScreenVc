﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngelList.JsonTypes
{

    public class Location
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tag_type")]
        public string TagType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("angellist_url")]
        public string AngellistUrl { get; set; }
    }

}
