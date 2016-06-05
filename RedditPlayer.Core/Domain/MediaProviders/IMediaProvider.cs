using System;
using System.Linq;
using System.Text.RegularExpressions;
using RedditPlayer.Domain.Media;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using System.Collections.Generic;

namespace RedditPlayer.Domain.MediaProviders
{
    public interface IMediaProvider
    {
        bool IsSupported { get; }
        bool IsValidUrl (string url);
        Task<IList<Track>> GetTrackForId (params string[] ids);

        // TODO: Implement
        Task<IList<Track>> SearchTracks(string query);
    }

    public class MediaProvider
    {
        IMediaProvider [] providers;

        public MediaProvider ()
        {
            //throw new Exception ("Method not implemented");
            //providers = new IMediaProvider[] {
            //    Youtube.Instance,
            //    Soundcloud.Instance
            //};
        }

        public IMediaProvider FromUrl (string url)
        {
            return Youtube.Instance;
            //IMediaProvider provider = new Soundcloud (new SoundcloudApi ("aaa"));
            //return provider.IsValidUrl (url) ? provider : UnknownProvider.Instance;
            //return providers.FirstOrDefault (provider => provider.IsValidUrl (url)) ?? UnknownProvider.Instance;
        }
    }

    //public class UnknownProvider : IMediaProvider
    //{
    //    public bool IsSupported => false;

    //    public bool IsValidUrl (string url)
    //    {
    //        return true;
    //    }

    //    public Task<Track> GetTrackForId (string id)
    //    {
    //        throw new NotImplementedException ();
    //    }

    //    public readonly static UnknownProvider Instance = new UnknownProvider ();

    //    private UnknownProvider ()
    //    {
    //    }
    //}
}

