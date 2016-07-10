using System;
using RedditPlayer.Domain.MediaProviders;
namespace RedditPlayer.Domain.Media
{
    public class Track
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public string AudioUrl { get; set; }

        public int OrderInAlbum { get; set; } = 0;
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }
        public IMediaProvider Provider { get; set; }

        public Track (string id, string title, string coverUrl, TimeSpan duration, IMediaProvider provider)
        {
            Id = id;
            Provider = provider;
            Title = title;
            Duration = duration;
            CoverUrl = coverUrl;
        }
    }
}

