﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngelList.JsonTypes
{

    public class Startup
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("community_profile")]
        public bool CommunityProfile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("angellist_url")]
        public string AngellistUrl { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        [JsonProperty("launch_date")]
        public string LaunchDate { get; set; }

        [JsonProperty("quality")]
        public int Quality { get; set; }

        [JsonProperty("product_desc")]
        public string ProductDesc { get; set; }

        [JsonProperty("high_concept")]
        public string HighConcept { get; set; }

        [JsonProperty("follower_count")]
        public int FollowerCount { get; set; }

        [JsonProperty("company_url")]
        public string CompanyUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("crunchbase_url")]
        public string CrunchbaseUrl { get; set; }

        [JsonProperty("twitter_url")]
        public string TwitterUrl { get; set; }

        [JsonProperty("blog_url")]
        public string BlogUrl { get; set; }

        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        [JsonProperty("markets")]
        public Market[] Markets { get; set; }

        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("company_type")]
        public CompanyType[] CompanyType { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("screenshots")]
        public Screenshot[] Screenshots { get; set; }
    }

}