using System;
using Newtonsoft.Json.Linq;
using RedditPlayer.Domain.MediaProviders;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Domain.Reddit
{
    public class RedditMedia : IEquatable<RedditMedia>
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Key => Url;

        public IMediaProvider Provider { get; set; }

        public static RedditMedia FromJson(JObject jObject)
        {
            var data = jObject["data"];
            var title = data["title"].Value<string>();
            var url = data["url"].Value<string>();
            var thumbnailUrl = data["thumbnail"].Value<string>();

            var mediaProviders = new MediaProvider();
            var provider = mediaProviders.FromUrl(url);

            return new RedditMedia
            {
                Title = title,
                Url = url,
                ThumbnailUrl = thumbnailUrl,
                Provider = provider
            };
        }

        public override int GetHashCode()
        {
            return (Title + Url + ThumbnailUrl).GetHashCode();
        }

        public bool Equals(RedditMedia other)
        {
            return Title == other.Title && Url == other.Url && ThumbnailUrl == other.ThumbnailUrl;
        }

        // TODO: Add duration
        public Track AsTrack()
        {
            return new Track(Url, Provider, Title, ThumbnailUrl, TimeSpan.FromTicks(0), null, null);
        }
    }
}

