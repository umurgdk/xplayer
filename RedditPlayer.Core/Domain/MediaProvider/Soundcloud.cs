using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;
using RedditPlayer.Domain.Reddit;

namespace RedditPlayer.Domain.MediaProvider
{
    public class Soundcloud : IMediaProvider
    {
        public bool IsSupported => true;

        private readonly ISoundcloudApi Api;

        public Soundcloud (ISoundcloudApi api)
        {
            Api = api;
        }

        public bool IsValidUrl (string url)
        {
            return Regex.IsMatch (url, "soundcloud");
        }

        public Task<ITrack> GetTrakForUrl (string url)
        {
            return null;
        }
    }
}