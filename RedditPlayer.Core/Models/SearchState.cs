using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections;
using System.Collections.Generic;

namespace RedditPlayer.Models
{
    public class SearchState : ReactiveObject
    {
        [Reactive]
        public string Query { get; set; }

        [Reactive]
        public bool SearchInProgress { get; set; }

        public ReactiveList<string> Results { get; private set; }

        public SearchState()
        {
            Query = "";
            Results = new ReactiveList<string>();
            SearchInProgress = false;
        }

        public void UpdateSearchResults (IEnumerable<string> newResults)
        {
            using (Results.SuppressChangeNotifications()) {
                Results.Clear();
                Results.AddRange(newResults);
            }
        }
    }
}

