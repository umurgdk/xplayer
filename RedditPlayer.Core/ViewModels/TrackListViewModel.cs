using System;
using System.Collections.Generic;
using RedditPlayer.Domain.Media;
using ReactiveUI;

namespace RedditPlayer.ViewModels
{
    public class TrackListViewModel
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public IList<Track> Tracks { get; set; }

        private TrackListViewModel (string imageUrl, string title, DateTime createdAt, IList<Track> tracks)
        {
            ImageUrl = imageUrl;
            Title = title;
            CreatedAt = createdAt;
            Tracks = tracks;
        }

        public static TrackListViewModel FromAlbum (Album album)
        {
            return new TrackListViewModel (album.CoverUrl, album.Name, album.ReleaseDate, album.Tracks);
        }

        public static TrackListViewModel FromPlaylist (Playlist playlist)
        {
            return new TrackListViewModel (playlist.CoverUrl, playlist.Title, playlist.CreatedAt, playlist.Tracks);
        }
    }
}

