using System;
using Newtonsoft.Json.Linq;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Domain.MediaProviders
{
    public class YoutubeTrack
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public TimeSpan Duration { get; set; }

        public Track ToTrack()
        {
            return new Track(Id, Youtube.Instance, Title, ThumbnailUrl, Duration, null, null);
        }

        public static YoutubeTrack FromJson(JObject jObject)
        {
            var video = jObject["videos"][0];

            return new YoutubeTrack
            {
                Id = video["id"].Value<string>(),
                Title = video["snippet"]["title"].Value<string>(),
                ThumbnailUrl = GetBestThumbnail(video["snippet"]["thumbnails"]),
                Duration = video["contentDetails"]["duration"].Value<TimeSpan>()
            };
	    }

        static string GetBestThumbnail (JToken thumbnails)
        {
            if (thumbnails["high"] != null)
            {
                return thumbnails["high"].Value<string>();
            }

            if (thumbnails["medium"] != null) 
            {
                return thumbnails["medium"].Value<string>();
            }

            if (thumbnails["default"] != null)
            {
                return thumbnails["default"].Value<string>();
            }

            return null;
        }
    }
}

