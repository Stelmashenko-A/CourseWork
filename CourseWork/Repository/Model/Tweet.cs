using System.Collections.Generic;

namespace Repository.Model
{
    public class Description
    {
        public List<object> urls { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public string location { get; set; }
        public bool follow_request_sent { get; set; }
        public bool is_translator { get; set; }
        public string id_str { get; set; }
        public TwitterEntities entities { get; set; }
        public bool default_profile { get; set; }
        public bool contributors_enabled { get; set; }
        public int favourites_count { get; set; }
        public string url { get; set; }
        public string profile_image_url_https { get; set; }
        public int utc_offset { get; set; }
        public int id { get; set; }
        public int listed_count { get; set; }
        public string lang { get; set; }
        public int followers_count { get; set; }
        public bool @protected { get; set; }
        public object notifications { get; set; }
        public bool verified { get; set; }
        public bool geo_enabled { get; set; }
        public string time_zone { get; set; }
        public string description { get; set; }
        public bool default_profile_image { get; set; }
        public string profile_background_image_url { get; set; }
        public int statuses_count { get; set; }
        public int friends_count { get; set; }
        public bool show_all_inline_media { get; set; }
        public string screen_name { get; set; }
    }
}
