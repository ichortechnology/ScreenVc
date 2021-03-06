﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngelList.JsonTypes
{

    public class UserRoles
    {

        [JsonProperty("startup_roles")]
        public StartupRole[] StartupRoles { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }
    }

}
