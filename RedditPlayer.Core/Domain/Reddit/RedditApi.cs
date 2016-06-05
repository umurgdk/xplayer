using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Domain.Reddit
{
    public static class RedditApi
    {
        const string API_URL = "http://reddit.com";
        const string SUBREDDIT_URL = API_URL + "/r/";

        public static async Task<SubReddit> GetSubReddit (string name)
        {
            var client = new HttpClient ();
            var url = SUBREDDIT_URL + name + "/about.json";
            var json = await client.GetStringAsync (url);
            var jobject = JObject.Parse (json);                                                                                       

            return SubReddit.FromJson (jobject);
        }

        public static async Task<List<Track>> GetSubRedditMedias (SubReddit subReddit)
        {
            return await GetSubRedditMedias(subReddit.Name);
        }

        public static async Task<List<Track>> GetSubRedditMedias (string subredditName)
        {
            var client = new HttpClient ();
            var url = SUBREDDIT_URL + subredditName + "/hot.json";
            var json = await client.GetStringAsync (url);

            var jObject = JObject.Parse (json);
            var children = jObject ["data"] ["children"].Values<JObject> ();

            return children
                .Select (child => RedditMedia.FromJson (child))
                .Where (media => media.Provider.IsSupported)
                .Select (media => media.AsTrack())
                .ToList ();
        }
    }
}

