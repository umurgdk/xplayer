using System;
using RedditPlayer.Domain.MediaProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RedditPlayer.Domain.Media
{
    public class Artist
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string UniqueId { get; set; }
        public IMediaProvider MediaProvider { get; protected set; }

        public string Bio { get; set; } = "";

        public Artist (string uniqueId, string name, string pictureUrl, IMediaProvider mediaProvider)
        {
            UniqueId = uniqueId;
            Name = name;
            PictureUrl = pictureUrl;
            MediaProvider = mediaProvider;
        }
    }
}

