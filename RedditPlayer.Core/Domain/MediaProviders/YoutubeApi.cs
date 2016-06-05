using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace RedditPlayer.Domain.MediaProviders
{
    //public class YoutubeApi
    //{
    //    const string BaseUrl = "https://www.googleapis.com/youtube/v3/";

    //    string apiKey;

    //    public YoutubeApi(string apiKey)
    //    {
    //        this.apiKey = apiKey;
    //    }

    //    public async Task<YoutubeTrack> GetTrackInfo (string videoId)
    //    {
    //        var response = await MakeRequest("videos", new Dictionary<string, string> { 
    //            ["videoId"] = videoId,
    //            ["part"] = "snippet,contentDetails"
    //        });

    //        return YoutubeTrack.FromJson(JObject.Parse(response));
    //    }

    //    public async Task<YoutubeTrack> SearchTracks (string query)
    //    {
    //        var response = await MakeRequest("search")
    //    }

    //    async Task<string> MakeRequest (string endpoint, Dictionary<string, string> queryParams)
    //    {
    //        queryParams.Add("key", apiKey);
    //        var queryString = string.Join("&", queryParams.Select(pair => $"{pair.Key}={pair.Value}"));

    //        var url = $"{BaseUrl}{endpoint}?{queryString}";

    //        var client = new HttpClient();
    //        return await client.GetStringAsync(url);
    //    }
    //}
}

