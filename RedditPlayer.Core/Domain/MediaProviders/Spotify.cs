using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;
using System.Linq;

namespace RedditPlayer.Domain.MediaProviders
{
    public class Spotify : IMediaProvider
    {
        public Spotify (IPlayer player)
        {
            Player = player;
        }

        public IPlayer Player { get; protected set; }

        public async Task<IList<Track>> GetTrackForId (params string [] ids)
        {
            return new List<Track> ();
        }

        public async Task<IList<Artist>> SearchArtists (string query)
        {
            var results = await SpotifyWebAPI.Artist.Search (query);
            return results.Items.Select (FromSPArtist).ToList ();
        }

        public async Task<IList<Track>> SearchTracks (string query)
        {
            var results = await SpotifyWebAPI.Track.Search (query);
            return results.Items.Select (FromSPTrack).ToList ();
        }

        Artist FromSPArtist (SpotifyWebAPI.Artist artist)
        {
            string imageUrl = null;
            if (artist.Images.Count > 0) {
                imageUrl = artist.Images [0].Url;
            }

            return new Artist (artist.Id, artist.Name, imageUrl, this);
        }

        Track FromSPTrack (SpotifyWebAPI.Track track)
        {
            string title;
            if (track.Artists.Count > 0) {
                title = string.Format ("{0} - {1}", track.Artists [0].Name, track.Name);
            } else {
                title = track.Name;
            }

            string imageUrl = null;
            if (track.Album?.Images.Count > 0) {
                imageUrl = track.Album.Images [0].Url;
            }

            return new Track (track.Id, title, imageUrl, TimeSpan.FromMilliseconds (track.Duration), this);
        }
    }
}

