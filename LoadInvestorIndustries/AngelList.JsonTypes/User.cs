﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngelList.JsonTypes
{

    public class User
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("follower_count")]
        public int FollowerCount { get; set; }

        [JsonProperty("angellist_url")]
        public string AngellistUrl { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("blog_url")]
        public string BlogUrl { get; set; }

        [JsonProperty("online_bio_url")]
        public string OnlineBioUrl { get; set; }

        [JsonProperty("twitter_url")]
        public string TwitterUrl { get; set; }

        [JsonProperty("facebook_url")]
        public string FacebookUrl { get; set; }

        [JsonProperty("linkedin_url")]
        public string LinkedinUrl { get; set; }

        [JsonProperty("aboutme_url")]
        public string AboutmeUrl { get; set; }

        [JsonProperty("github_url")]
        public string GithubUrl { get; set; }

        [JsonProperty("dribbble_url")]
        public string DribbbleUrl { get; set; }

        [JsonProperty("behance_url")]
        public string BehanceUrl { get; set; }

        [JsonProperty("what_ive_built")]
        public string WhatIveBuilt { get; set; }

        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("roles")]
        public Role[] Roles { get; set; }

        [JsonProperty("skills")]
        public Skill[] Skills { get; set; }

        [JsonProperty("investor")]
        public bool Investor { get; set; }
    }

}
