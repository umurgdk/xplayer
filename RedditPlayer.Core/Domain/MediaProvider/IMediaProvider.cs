using System;
using System.Linq;
using System.Text.RegularExpressions;
using RedditPlayer.Domain.Media;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace RedditPlayer.Domain.MediaProvider
{
    public interface IMediaProvider
    {
        bool IsSupported { get; }
        bool IsValidUrl (string url);
        Task<ITrack> GetTrakForUrl (string url);
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
            IMediaProvider provider = new Soundcloud (new SoundcloudApi ("aaa"));
            return provider.IsValidUrl (url) ? provider : UnknownProvider.Instance;
            //return providers.FirstOrDefault (provider => provider.IsValidUrl (url)) ?? UnknownProvider.Instance;
        }
    }

    public class Youtube : IMediaProvider
    {
        public bool IsSupported => false;

        public readonly static Youtube Instance = new Youtube ();

        private Youtube () { }

        public bool IsValidUrl (string url)
        {
            return Regex.IsMatch (url, @"(youtube\.com|youtu\.be)");
        }

        public Task<ITrack> GetTrakForUrl (string url)
        {
            throw new NotImplementedException ();
        }
    }

    public class UnknownProvider : IMediaProvider
    {
        public bool IsSupported => false;

        public bool IsValidUrl (string url)
        {
            return true;
        }

        public Task<ITrack> GetTrakForUrl (string url)
        {
            throw new NotImplementedException ();
        }

        public readonly static UnknownProvider Instance = new UnknownProvider ();

        private UnknownProvider ()
        {
        }
    }
}

