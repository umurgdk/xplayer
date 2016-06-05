using System;
using System.Collections.Generic;
using RedditPlayer.Domain.Media;
namespace RedditPlayer.Domain.Reddit
{
    public class RedditDynamicPlaylist : IDynamicPlaylist
    {
        public string CoverUrl { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string UniqueId { get; set; }

        public IList<Track> Update()
        {
            return new List<Track>();
        }
    }
}

