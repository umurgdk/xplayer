using System;
using NUnit.Framework;
using RedditPlayer.Domain.MediaProviders;
using System.Threading.Tasks;

namespace RedditPlayer.Tests
{
    [TestFixture()]
    public class YoutubeTest
    {
        [Test()]
        public async Task GetTrackForIds()
        {
            var youtube = Youtube.Instance;

            var tracks = await youtube.GetTrackForId("Vy3DvF8nibA");

            Assert.AreEqual("Top 10 Things I Wish I Had Known About C++", tracks[0].Title);
            Assert.IsNotEmpty(tracks[0].CoverUrl);
        }
    }
}

