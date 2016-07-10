using System;
using System.Collections.Generic;
namespace RedditPlayer.Domain.Media
{
    public class Playlist
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TimeSpan Duration { get; set; }
        public string CoverUrl { get; set; }

        public IList<Track> Tracks { get; set; }

        public Playlist (string id, string title, DateTime createdAt, DateTime updatedAt, string coverUrl, IList<Track> tracks)
        {
            Id = id;
            Title = title;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Tracks = tracks;
            CoverUrl = coverUrl;
        }

        public Playlist (string id, string title, DateTime createdAt, DateTime updatedAt, string coverUrl)
        {
            Id = id;
            Title = title;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Tracks = new List<Track> ();
            CoverUrl = coverUrl;
        }
    }
}

