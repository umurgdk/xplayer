using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using RedditPlayer.Domain.Media;
using System.Linq;

namespace RedditPlayer.Domain.MediaProviders
{
    public interface ISoundcloudApi
    {
        Task<SoundcloudTrack> GetTrack(string trackId);
    }

    public class SoundcloudApi : ISoundcloudApi
    {
        const string BaseUrl = "http://api.soundcloud.com/";

        readonly string clientId;

        public SoundcloudApi(string clientId)
        {
            this.clientId = clientId;
        }

        public async Task<List<Track>> SearchTracks(Soundcloud mediaProvider, string query)
        {
            var client = new HttpClient();
            var url = new Uri($"{BaseUrl}tracks?q={query}&types=original,recording&client_id={clientId}&limit=20");
            var response = await client.GetStringAsync(url);
            var json = JArray.Parse(response);

            return json.Children()
                       .Where(token => token["stream_url"].Value<string>() != null)
                       .Select(token => JsonToTrack(mediaProvider, token))
                       .ToList();
        }

        public async Task<SoundcloudTrack> GetTrack(string trackId)
        {
            var client = new HttpClient();
            var url = new Uri($"{BaseUrl}tracks/{trackId}?client_id={clientId}");
            var response = await client.GetStringAsync(url);

            var json = JObject.Parse(response);
            return SoundcloudTrack.FromJson(json);
        }

        Track JsonToTrack(Soundcloud mediaProvider, JToken json)
        {
            var track = new Track(
                json["id"].Value<string>(),
                json["title"].Value<string>(),
                json["artwork_url"].Value<string>()?.Replace("large", "t300x300"),
                TimeSpan.FromMilliseconds(json["duration"].Value<int>()),
                mediaProvider
            );

            track.ArtistId = json["user_id"].Value<string>();
            track.AudioUrl = $"{json["stream_url"]}?client_id={clientId}";

            return track;
        }
    }
}

