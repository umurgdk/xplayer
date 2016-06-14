using System;
using NUnit.Framework;
using System.Threading.Tasks;
using RedditPlayer.Domain.MediaProviders;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Tests.Domain.MediaProviders
{
    [TestFixture]
    public class SoundcloudTests
    {
        [Test]
        public async Task SearchTracks ()
        {
            var soundcloud = new Soundcloud (new SoundcloudApi ("4fea3da6c7cb6807bcd29df897cb303e"), new SoundcloudPlayer ());
            var tracks = await soundcloud.SearchTracks ("pink floyd");

            Assert.GreaterOrEqual (tracks.Count, 1);
        }
    }

    public class SoundcloudPlayer : IPlayer
    {
        public TimeSpan GetElapsedTime ()
        {
            throw new NotImplementedException ();
        }

        public void Mute ()
        {
            throw new NotImplementedException ();
        }

        public void Pause ()
        {
            throw new NotImplementedException ();
        }

        public void Play (Track track)
        {
            throw new NotImplementedException ();
        }

        public void Seek (float progress)
        {
            throw new NotImplementedException ();
        }

        public void SetVolume (float volume)
        {
            throw new NotImplementedException ();
        }

        public void Unmute ()
        {
            throw new NotImplementedException ();
        }
    }
}

