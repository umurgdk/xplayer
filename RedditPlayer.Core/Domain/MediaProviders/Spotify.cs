using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;
using System.Linq;
using Splat;
using RedditPlayer.Services;

namespace RedditPlayer.Domain.MediaProviders
{
    public class Spotify : IMediaProvider
    {
        public Spotify (IPlayer player)
        {
            Player = player;
        }

        public IPlayer Player { get; protected set; }

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

        public async Task<IList<Album>> GetAlbums (Artist artist)
        {
            var spArtist = await SpotifyWebAPI.Artist.GetArtist (artist.UniqueId);
            var albumPage = await spArtist.GetAlbums ();
            var spAlbums = await albumPage.ToList ();

            spAlbums = spAlbums.Aggregate (new Dictionary<string, SpotifyWebAPI.Album> (), (albumsDictionary, album) => {
                albumsDictionary [album.Name] = album;
                return albumsDictionary;
            }).Values.ToList ();

            var albums = await Task.WhenAll (spAlbums.Select (FromSPAlbum));

            return albums.ToList ();
        }

        public async Task<IList<Track>> GetPopularTracks (Artist artist)
        {
            var spArtist = await SpotifyWebAPI.Artist.GetArtist (artist.UniqueId);
            var popularSongs = await spArtist.GetTopTracks ();

            var numberOfPopularSongs = Locator.CurrentMutable.GetService<ISettings> ().NumberOfPopularSongs;

            return popularSongs.Select (FromSPTrack).ToList ();
        }

        public async Task<IList<Playlist>> GetPlaylists (Artist artist)
        {
            return new List<Playlist> ();
        }

        Artist FromSPArtist (SpotifyWebAPI.Artist spArtist)
        {
            string imageUrl = null;
            if (spArtist.Images.Count > 0) {
                imageUrl = spArtist.Images [0].Url;
            }

            return new Artist (spArtist.Id, spArtist.Name, imageUrl, this);
        }

        Track FromSPTrack (SpotifyWebAPI.Track spTrack)
        {
            string title;
            if (spTrack.Artists.Count > 0) {
                title = string.Format ("{0} - {1}", spTrack.Artists [0].Name, spTrack.Name);
            } else {
                title = spTrack.Name;
            }

            string imageUrl = null;
            if (spTrack.Album?.Images.Count > 0) {
                imageUrl = spTrack.Album.Images [0].Url;
            }

            var track = new Track (spTrack.Id, title, imageUrl, TimeSpan.FromMilliseconds (spTrack.Duration), this);
            track.ArtistId = spTrack.Artists [0].Id;
            //track.AlbumId = spTrack.Album.Id;
            track.OrderInAlbum = spTrack.TrackNumber;

            return track;
        }

        async Task<Album> FromSPAlbum (SpotifyWebAPI.Album spAlbum)
        {
            string imageUrl = null;
            if (spAlbum.Images.Count > 0) {
                imageUrl = spAlbum.Images [0].Url;
            }

            var tracksPage = await spAlbum.GetAlbumTracks ();
            var tracks = await tracksPage.ToList ();

            var album = new Album (spAlbum.Name, spAlbum.Id, imageUrl, this);
            album.Tracks = tracks.Select (FromSPTrack).ToList ();
            album.ReleaseDate = spAlbum.ReleaseDate;

            return album;
        }
    }
}

