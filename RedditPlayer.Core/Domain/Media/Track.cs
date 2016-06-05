using System;
using RedditPlayer.Domain.MediaProviders;
namespace RedditPlayer.Domain.Media
{
    public class Track
    {
        public string Title    { get; set; }
		public string CoverUrl { get; set; }
        public TimeSpan Duration    { get; set; }

        public string UniqueId { get; set; }
        public string ArtistId { get; set; }
        public string AlbumId  { get; set; }
        public IMediaProvider Provider { get; set; }

        public Track(string uniqueId, IMediaProvider provider, string title, string coverUrl, TimeSpan duration, string artistId, string albumId)
        {
			UniqueId = uniqueId;
            Provider = provider;
            Title = title;
            Duration = duration;
            ArtistId = artistId;
            AlbumId = albumId;
            CoverUrl = coverUrl;
        }
    }
}

