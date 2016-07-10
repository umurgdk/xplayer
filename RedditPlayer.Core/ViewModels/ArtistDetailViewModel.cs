using System;
using ReactiveUI;
using RedditPlayer.Domain.Media;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Reactive.Linq;
namespace RedditPlayer.ViewModels
{
    public class ArtistDetailViewModel : ReactiveObject
    {
        [Reactive]
        public Artist Artist { get; protected set; }

        public ReactiveList<Track> PopularSongs { get; protected set; }
        public ReactiveList<Album> Albums { get; protected set; }
        public ReactiveList<Playlist> Playlists { get; protected set; }

        public ReactiveCommand<IList<Track>> LoadPopularSongs { get; protected set; }
        public ReactiveCommand<IList<Album>> LoadAlbums { get; protected set; }
        public ReactiveCommand<IList<Playlist>> LoadPlaylists { get; protected set; }

        public ArtistDetailViewModel (Artist artist)
        {
            Artist = artist;

            PopularSongs = new ReactiveList<Track> ();
            Albums = new ReactiveList<Album> ();
            Playlists = new ReactiveList<Playlist> ();

            var artistAvailable = this.WhenAnyValue (vm => vm.Artist)
                                      .Select (val => val != null);

            LoadPopularSongs = ReactiveCommand.CreateAsyncTask (artistAvailable, _ => Artist.MediaProvider.GetPopularTracks (Artist));
            LoadAlbums = ReactiveCommand.CreateAsyncTask (artistAvailable, _ => Artist.MediaProvider.GetAlbums (Artist));
            LoadPlaylists = ReactiveCommand.CreateAsyncTask (artistAvailable, _ => Artist.MediaProvider.GetPlaylists (Artist));

            LoadPopularSongs.Subscribe (tracks => {
                using (PopularSongs.SuppressChangeNotifications ()) {
                    PopularSongs.Clear ();
                    PopularSongs.AddRange (tracks);
                }
            });

            LoadAlbums.Subscribe (albums => {
                using (Albums.SuppressChangeNotifications ()) {
                    Albums.Clear ();
                    Albums.AddRange (albums);
                }
            });

            LoadPlaylists.Subscribe (playlists => {
                using (Playlists.SuppressChangeNotifications ()) {
                    Playlists.Clear ();
                    Playlists.AddRange (playlists);
                }
            });
        }
    }
}

