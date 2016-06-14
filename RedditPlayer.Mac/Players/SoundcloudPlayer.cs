using System;
using RedditPlayer.Domain.Media;
using AVFoundation;
using Foundation;
using RedditPlayer.Domain.MediaProviders;
using CoreMedia;

namespace RedditPlayer.Mac.Players
{
    public class SoundcloudPlayer : IPlayer
    {
        AVPlayer player;
        Track currentTrack;

        public TimeSpan GetElapsedTime ()
        {
            if (player != null) {
                return TimeSpan.FromSeconds (player.CurrentTime.Seconds);
            }

            return TimeSpan.Zero;
        }

        public void Mute ()
        {
            if (player != null)
                player.Muted = true;
        }

        public void Pause ()
        {
            player?.Pause ();
        }

        public void Stop ()
        {
            player?.Pause ();
        }

        public void Play (Track track)
        {
            var audioUrl = NSUrl.FromString (track.AudioUrl);
            if (track != currentTrack) {
                player = AVPlayer.FromUrl (audioUrl);
            }

            player.Play ();
            currentTrack = track;
        }

        public void Seek (float progress)
        {
            player?.Seek (CMTime.FromSeconds (player.CurrentItem.Duration.Seconds * progress, CMTimeScale.MaxValue.Value));
        }

        public void SetVolume (float volume)
        {
            if (player != null)
                player.Volume = volume;
        }

        public void Unmute ()
        {
            if (player != null)
                player.Muted = false;
        }
    }
}

