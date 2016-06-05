using System;
using System.Collections.Generic;

namespace RedditPlayer.Domain.Media
{
    public interface IDynamicPlaylist
    {
        string Name { get; set; }
        string Description { get; set; }
        string CoverUrl { get; set; }
        string UniqueId { get; set; }

        IList<Track> Update();
    }
}

