using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
namespace RedditPlayer.Domain.MediaProvider
{
    public interface ISoundcloudApi
    {
        Task<SoundcloudTrack> GetTrack (string trackId);
    }

    public class SoundcloudApi : ISoundcloudApi
    {
        const string BaseUrl = "http://api.soundcloud.com/";

        readonly string AppClientId;

        public SoundcloudApi (string clientId)
        {
            AppClientId = clientId;
        }

        public async Task<SoundcloudTrack> GetTrack (string trackId)
        {
            var client = new HttpClient ();
            var url = new Uri ($"{BaseUrl}tracks/{trackId}?clientId={AppClientId}");
            var response = await client.GetStringAsync (url);

            var json = JObject.Parse (response);
            return SoundcloudTrack.FromJson (json);
        }
    }
}

