using System;
namespace RedditPlayer.Actions
{
    public struct SearchAction : IAction
    {
        public string Query { get; set; }

        public SearchAction(string query)
        {
            Query = query;
        }
    }
}

