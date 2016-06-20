using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;
using RedditPlayer.Domain.Reddit;
using System.Diagnostics;

namespace RedditPlayer.Domain.MediaProviders
{
    public class Soundcloud : IMediaProvider
    {
        public bool IsSupported => true;

        public IPlayer Player { get; protected set; }

        readonly SoundcloudApi Api;

        public Soundcloud(SoundcloudApi api, IPlayer player)
        {
            Player = player;
            Api = api;
        }

        public bool IsValidUrl(string url)
        {
            return Regex.IsMatch(url, "soundcloud");
        }

        public async Task<IList<Track>> SearchTracks(string query)
        {
            return await Api.SearchTracks(this, query);
        }

        public async Task<IList<Artist>> SearchArtists(string query)
        {
            return await Api.SearchUsers(this, query);
        }

        // TODO: Implement Soundcloud#GetAlbums(Artist artist)
        public async Task<IList<Album>> GetAlbums(Artist artist)
        {
            Debug.WriteLine("Soundcloud#GetAlbums is not implemented yet!");
            return new List<Album>();
        }

        // TODO: Implement Soundcloud#GetPopularTracks(Artist artist)
        public async Task<IList<Track>> GetPopularTracks(Artist artist)
        {
            Debug.WriteLine("Soundcloud#GetPopularTracks is not implemented yet!");
            return new List<Track>();
        }
    }
}