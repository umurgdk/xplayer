using System;
using System.Collections.Generic;

namespace RedditPlayer.Actions
{
    public struct UpdateSearchResultsAction : IAction
    {
        public IEnumerable<string> Results { get; private set; }

        public UpdateSearchResultsAction(IEnumerable<string> results)
        {
            Results = results;
        }
    }
}

