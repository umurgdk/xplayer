using System;
namespace RedditPlayer.Domain.Media
{
    public class Album
    {
        public string Name { get; set; }
        public string UniqueId { get; set; }
        public string CoverUrl { get; set; }

        public Album(string name, string uniqueId, string coverUrl)
        {
            Name = name;
            UniqueId = uniqueId;
            CoverUrl = coverUrl;
        }
    }
}

