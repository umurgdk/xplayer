using System;
namespace RedditPlayer.Domain.Media
{
    public class Artist
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string UniqueId { get; set; }

        public Artist(string uniqueId, string name, string pictureUrl)
        {
			UniqueId = uniqueId;
            Name = name;
            PictureUrl = pictureUrl;
        }
    }
}

