using System;
using AppKit;
using System.Threading.Tasks;
using Foundation;
namespace RedditPlayer.Mac.Extensions
{
    public static class NSImageViewExtensions
    {
        public static void SetImageAsync (this NSImageView imageView, string url)
        {
            if (url == null)
                return;

            Task.Run (() => {
                var image = new NSImage (NSData.FromUrl (NSUrl.FromString (url)));

                NSApplication.SharedApplication.InvokeOnMainThread (() => {
                    if (imageView == null)
                        return;

                    imageView.Image = image;
                });
            });
        }
    }
}

