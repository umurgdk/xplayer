using System;
using Newtonsoft.Json.Linq;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Domain.MediaProviders
{
    public class SoundcloudTrack
    {
        public readonly string Id;
        public readonly string Duration;
        public readonly string Title;
        public readonly string StreamUrl;

        public SoundcloudTrack (string id, string duration, string title, string streamUrl)
        {
            Id = id;
            Duration = duration;
            Title = title;
            StreamUrl = streamUrl;
        }

        static public SoundcloudTrack FromJson (JObject json)
        {
            var id = json ["id"].Value<string> ();
            var duration = json ["duration"].Value<string> ();
            var title = json ["title"].Value<string> ();
            var streamUrl = json ["stream_url"].Value<string> ();

            return new SoundcloudTrack (id, duration, title, streamUrl);
        }
    }
}