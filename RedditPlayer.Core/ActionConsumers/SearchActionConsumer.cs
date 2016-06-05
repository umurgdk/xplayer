using System;
using RedditPlayer.Models;
using RedditPlayer.Actions;

namespace RedditPlayer.ActionConsumers
{
    public class SearchActionConsumer : ActionConsumer<SearchAction>
    {
        public override void Reduce(StateStore store, ApplicationState state, SearchAction action)
        {
            state.SearchState.SearchInProgress = true;
        }
    }
}

