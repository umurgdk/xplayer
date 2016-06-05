using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RedditPlayer.Domain.Reddit;
using RedditPlayer.ViewModels;
using System.Threading.Tasks;
using System.Reactive;
using RedditPlayer.Domain.MediaProviders;
using RedditPlayer.Services;

namespace RedditPlayer
{
    public class ApplicationViewModel : ReactiveObject
    {
        public SearchViewModel Search { get; private set; }



        public ReactiveList<SubReddit> Reddits { get; protected set; }

        public ReactiveList<RedditMedia> RedditMedias { get; protected set; }

        //public ReactiveList<SearchResult> SearchResults { get; protected set; }

        [Reactive]
        public SubReddit SelectedSubReddit { get; set; }

        [Reactive]
        public string NewSubRedditName { get; set; }

        public ReactiveCommand<SubReddit> AddSubreddit { get; protected set; }

        public ReactiveCommand<List<RedditMedia>> LoadMedia { get; protected set; }

        public bool CanEditSubRedditName { [ObservableAsProperty]get; private set; }

        public ApplicationViewModel (INavigator navigator)
        {
            Search = new SearchViewModel(navigator, new IMediaProvider[] {
                Youtube.Instance
            });

            Reddits = new ReactiveList<SubReddit> { };
            RedditMedias = new ReactiveList<RedditMedia> { };

            var canExecute = this.WhenAnyValue (vm => vm.NewSubRedditName)
                                 .Select (name => !string.IsNullOrWhiteSpace (name));

            AddSubreddit = ReactiveCommand.CreateAsyncTask (canExecute, name => RedditApi.GetSubReddit (NewSubRedditName));
            AddSubreddit.Subscribe (subReddit => {
                Reddits.Add (subReddit);
            });

            this.WhenAnyObservable (vm => vm.AddSubreddit.IsExecuting)
                .Select (x => !x)
                .ToPropertyEx (this, vm => vm.CanEditSubRedditName);

            var canLoadMedia = this.WhenAnyValue (vm => vm.SelectedSubReddit).Select (x => x != null);

            //LoadMedia = ReactiveCommand.CreateAsyncTask (canLoadMedia, _ => RedditApi.GetSubRedditMedias (SelectedSubReddit));
            //LoadMedia.Subscribe (medias => {
            //    using (RedditMedias.SuppressChangeNotifications ()) {
            //        RedditMedias.Clear ();
            //        RedditMedias.AddRange (medias);
            //    }
            //});

            Reddits.Add (new SubReddit {
                Title = "krautrock",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Favorite"
            });

            Reddits.Add (new SubReddit {
                Title = "Glitch",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Favorite"
            });

            Reddits.Add (new SubReddit {
                Title = "Noise",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Favorite"
            });

            Reddits.Add (new SubReddit {
                Title = "Chillout",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Featured"
            });

            Reddits.Add (new SubReddit {
                Title = "Pop",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Featured"
            });

            Reddits.Add (new SubReddit {
                Title = "R&B",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Featured"
            });

            Reddits.Add (new SubReddit {
                Title = "Ambient",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Featured"
            });

            Reddits.Add (new SubReddit {
                Title = "Rock",
                Description = "Lorem ipsum dolor sit amet.",
                Grouping = "Featured"
            });

            SelectedSubReddit = Reddits [0];
        }

        async Task SearchImpl(string arg)
        {
            
        }
   }
}

