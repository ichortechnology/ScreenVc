﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AngelList.JsonTypes.UserJsonTypes;

namespace AngelList.JsonTypes
{
    [DataContract]
    public class User
    {

        [JsonProperty("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [JsonProperty("bio")]
        [DataMember(Name = "bio")]
        public string Bio { get; set; }

        [JsonProperty("follower_count")]
        [DataMember(Name = "follower_count")]
        public int FollowerCount { get; set; }

        [JsonProperty("angellist_url")]
        [DataMember(Name = "angellist_url")]
        public string AngellistUrl { get; set; }

        [JsonProperty("image")]
        [DataMember(Name = "image")]
        public string Image { get; set; }

        [JsonProperty("blog_url")]
        [DataMember(Name = "blog_url")]
        public string BlogUrl { get; set; }

        [JsonProperty("online_bio_url")]
        [DataMember(Name = "online_bio_url")]
        public string OnlineBioUrl { get; set; }

        [JsonProperty("twitter_url")]
        [DataMember(Name = "twitter_url")]
        public string TwitterUrl { get; set; }

        [JsonProperty("facebook_url")]
        [DataMember(Name = "facebook_url")]
        public string FacebookUrl { get; set; }

        [JsonProperty("linkedin_url")]
        [DataMember(Name = "linkedin_url")]
        public string LinkedinUrl { get; set; }

        [JsonProperty("aboutme_url")]
        [DataMember(Name = "aboutme_url")]
        public string AboutmeUrl { get; set; }

        [JsonProperty("github_url")]
        [DataMember(Name = "github_url")]
        public string GithubUrl { get; set; }

        [JsonProperty("dribbble_url")]
        [DataMember(Name = "dribbble_url")]
        public string DribbbleUrl { get; set; }

        [JsonProperty("behance_url")]
        [DataMember(Name = "behance_url")]
        public string BehanceUrl { get; set; }

        [JsonProperty("what_ive_built")]
        [DataMember(Name = "what_ive_built")]
        public string WhatIveBuilt { get; set; }

        [JsonProperty("locations")]
        [DataMember(Name = "locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("roles")]
        [DataMember(Name = "roles")]
        public Role[] Roles { get; set; }

        [JsonProperty("skills")]
        [DataMember(Name = "skills")]
        public Skill[] Skills { get; set; }

        [JsonProperty("investor")]
        [DataMember(Name = "investor")]
        public bool Investor { get; set; }
    }

}
