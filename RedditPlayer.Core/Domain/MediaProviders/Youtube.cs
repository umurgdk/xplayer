using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using System.Threading.Tasks;
using RedditPlayer.Domain.Media;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace RedditPlayer.Domain.MediaProviders
{
    public class Youtube : IMediaProvider
    {
        YouTubeService service;

        public IPlayer Player { get; protected set; }

        public Youtube(IPlayer player)
        {
            Player = player;

            service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyB2-jvhH8wLcY1Ao3BRwVIGYGfF8_1CA8U",
                ApplicationName = "XPlayer"
            });
        }

        public async Task<IList<Track>> GetTrackForId(params string[] ids)
        {
            var request = service.Videos.List("id,snippet,contentDetails");
            request.Id = string.Join(",", ids);

            var list = await request.ExecuteAsync();

            if (list.Items != null && list.Items.Count > 0)
            {
                return list.Items.Select(video =>
                {
                    var duration = XmlConvert.ToTimeSpan(video.ContentDetails.Duration);
                    return new Track(video.Id, video.Snippet.Title, video.Snippet.Thumbnails.Default__.Url, duration, this);
                }).ToList();
            }

            return new List<Track>();
        }

        public async Task<IList<Track>> SearchTracks(string query)
        {
            var request = service.Search.List("id");
            request.Q = query;
            request.Type = "video";

            var list = await request.ExecuteAsync();

            if (list.Items != null)
            {
                var ids = list.Items.Select(i => i.Id.VideoId);
                return await GetTrackForId(ids.ToArray());
            }

            return new List<Track>();
        }
    }
}

