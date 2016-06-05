using System;
using System.Collections.Generic;
using RedditPlayer.Models;
using RedditPlayer.Actions;

namespace RedditPlayer.ActionConsumers
{
    public class UpdateSearchResultsConsumer : ActionConsumer<UpdateSearchResultsAction>
    {
        public override void Reduce(StateStore store, ApplicationState state, UpdateSearchResultsAction action)
        {
            state.SearchState.UpdateSearchResults(action.Results);
        }
    }
}

