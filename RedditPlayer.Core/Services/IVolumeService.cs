using System;

namespace RedditPlayer.Services
{
    public interface IVolumeService
    {
        void Increase();
        void Decrease();
        void Set(float volume);
        float Get();
    }
}

