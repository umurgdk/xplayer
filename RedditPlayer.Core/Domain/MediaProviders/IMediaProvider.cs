using System;
using System.Linq;
using System.Text.RegularExpressions;
using RedditPlayer.Domain.Media;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using System.Collections.Generic;

namespace RedditPlayer.Domain.MediaProviders
{
    public interface IMediaProvider
    {
        IPlayer Player { get; }

        Task<IList<Track>> SearchTracks(string query);
        Task<IList<Artist>> SearchArtists(string query);
        Task<IList<Album>> GetAlbums(Artist artist);
        Task<IList<Track>> GetPopularTracks(Artist artist);
    }
}

