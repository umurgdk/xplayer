using System;

namespace RedditPlayer.Domain.Media
{
    public interface IPlayer
    {
        TimeSpan GetElapsedTime ();
        void Play (Track track);
        void Pause ();

        void Mute ();
        void Unmute ();
        void SetVolume (float volume);
        void Seek (float progress);
    }
}
