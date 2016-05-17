using System;
using RedditPlayer.Domain.MediaProvider;
namespace RedditPlayer.Domain.Media
{
    public interface ITrack
    {
        IMediaProvider Provider { get; }
        string Title { get; }
        string StreamUrl { get; }
    }
}

