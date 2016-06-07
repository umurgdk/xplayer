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
        public PlayerViewModel Player { get; private set; }

        public ApplicationViewModel()
        {
            Search = new SearchViewModel ();
            Player = new PlayerViewModel ();
        }
   }
}

