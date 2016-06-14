using System;
using NUnit.Framework;
using RedditPlayer.Domain.MediaProviders;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Tests
{
    [TestFixture ()]
    public class YoutubeTest
    {
        [Test ()]
        public async Task GetTrackForIds ()
        {
            var youtube = new Youtube (new YoutubeDummyPlayer ());

            var tracks = await youtube.GetTrackForId ("Vy3DvF8nibA");

            Assert.AreEqual ("Top 10 Things I Wish I Had Known About C++", tracks [0].Title);
            Assert.IsNotEmpty (tracks [0].CoverUrl);
        }
    }

    public class YoutubeDummyPlayer : IPlayer
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

