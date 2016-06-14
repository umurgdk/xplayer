using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;
using RedditPlayer.Domain.Reddit;

namespace RedditPlayer.Domain.MediaProviders
{
    public class Soundcloud : IMediaProvider
    {
        public bool IsSupported => true;

        public IPlayer Player { get; protected set; }

        readonly SoundcloudApi Api;

        public Soundcloud (SoundcloudApi api, IPlayer player)
        {
            Player = player;
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

        public Task<IList<Track>> GetTrackForId (params string [] ids)
        {
            throw new NotImplementedException ();
        }

        public async Task<IList<Track>> SearchTracks (string query)
        {
            return await Api.SearchTracks (this, query);
        }
    }
}