using System;
using System.Collections.Generic;
using RedditPlayer.Domain.MediaProviders;

namespace RedditPlayer.Domain.Media
{
    public class Album
    {
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public string CoverUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IMediaProvider MediaProvider { get; set; }

        public string ArtistId { get; set; }
        public IList<Track> Tracks { get; set; }

        public Album (string name, string uniqueId, string coverUrl, IMediaProvider mediaProvider)
        {
            Name = name;
            UniqueId = uniqueId;
            CoverUrl = coverUrl;
            MediaProvider = mediaProvider;

            Tracks = new List<Track> ();
        }
    }
}

