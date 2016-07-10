using System;
namespace RedditPlayer.Services
{
    public interface ISettings
    {
        void RemoveAll ();

        bool FirstRun { get; set; }
        float Volume { get; set; }
        int NumberOfPopularSongs { get; set; }
    }
}

