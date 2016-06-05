using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RedditPlayer.Domain.Media;
using RedditPlayer.Domain.MediaProviders;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Linq;
using RedditPlayer.Services;

namespace RedditPlayer.ViewModels
{
    public class SearchViewModel : ReactiveObject
    {
        [Reactive]
        public string Query { get; set; }

        public ReactiveList<Track> Tracks { get; protected set; }
        public ReactiveList<Artist> Artists { get; protected set; }
        public ReactiveList<Album> Albums { get; protected set; }

        public ReactiveCommand<Unit> Search { get; private set; }

        ReactiveCommand<IEnumerable<Track>> searchTracks;
        ReactiveCommand<IEnumerable<Artist>> searchArtists;
        ReactiveCommand<IEnumerable<Album>> searchAlbums;

        IEnumerable<IMediaProvider> mediaProviders;
        INavigator navigator;

        public SearchViewModel(INavigator navigator, IEnumerable<IMediaProvider> mediaProviders)
        {
            this.mediaProviders = mediaProviders;
            this.navigator = navigator;

            Tracks = new ReactiveList<Track> ();
            Artists = new ReactiveList<Artist> ();
            Albums = new ReactiveList<Album> ();

            searchTracks = ReactiveCommand.CreateAsyncTask(ImplSearchTracks);
            searchTracks.Subscribe(tracks => 
            {
                using (Tracks.SuppressChangeNotifications()) 
                {
                    Tracks.Clear();
                    Tracks.AddRange(tracks);
                }
            });

            searchArtists = ReactiveCommand.CreateAsyncTask(ImplSearchArtists);
            searchArtists.Subscribe(artists => 
            {
                using (Artists.SuppressChangeNotifications()) 
                {
                    Artists.Clear();
                    Artists.AddRange(artists);
                }
            });

            searchAlbums = ReactiveCommand.CreateAsyncTask(ImplSearchAlbums);
            searchAlbums.Subscribe(albums =>
            {
                using (Albums.SuppressChangeNotifications())
                {
                    Albums.Clear();
                    Albums.AddRange(albums);
                }
            });

            var canSearch = this.WhenAnyValue(vm => vm.Query)
                                .Select(query => !string.IsNullOrEmpty(query));

            Search = ReactiveCommand.CreateAsyncTask(canSearch, async (arg) =>
            {
                var tasks = new Task[] {
                    searchTracks.ExecuteAsyncTask(),
                    searchArtists.ExecuteAsyncTask(),
                    searchAlbums.ExecuteAsyncTask()
                };

                await Task.WhenAll(tasks);
            });

            Search.Subscribe(_ => SearchCompleted());
        }

        void SearchCompleted()
        {
            navigator.PresentSearchResults (this);
        }

        async Task<IEnumerable<Track>> ImplSearchTracks(object arg)
        {
            var trackTasks = mediaProviders.Select(provider => provider.SearchTracks(Query));
            var trackResults = await Task.WhenAll(trackTasks);

            return trackResults.SelectMany(x => x);
        }

        async Task<IEnumerable<Artist>> ImplSearchArtists(object arg)
        {
            return new List<Artist>();
        }

        async Task<IEnumerable<Album>> ImplSearchAlbums(object arg)
        {
            return new List<Album>();
        }
   }
}

