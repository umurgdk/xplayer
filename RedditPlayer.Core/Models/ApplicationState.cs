using System;
using ReactiveUI;
namespace RedditPlayer.Models
{
    public class ApplicationState : ReactiveObject
    {
        public SearchState SearchState { get; private set; }

        public ApplicationState()
        {
        }
    }
}

