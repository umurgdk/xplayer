using System;
using AppKit;
using System.Collections.Generic;
using Foundation;
using RedditPlayer.Domain.Reddit;

namespace RedditPlayer.Mac
{
    public class RedditMediasTableViewSource : NSTableViewSource
    {
        IList<RedditMedia> medias;

        public RedditMediasTableViewSource (IList<RedditMedia> medias)
        {
            this.medias = medias;
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            return medias.Count;
        }

        public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var view = (RedditMediaItemView)tableView.MakeView ("RedditMediaItemView", this);
            var media = medias [(int)row];

            if (view != null) {
                view.Title.StringValue = media.Title;
                view.Url.StringValue = media.Url;

                var imageUrl = NSUrl.FromString (media.ThumbnailUrl);

                if (imageUrl != null && imageUrl.Scheme != null && imageUrl.Host != null) {
                    view.Image.Image = new NSImage (imageUrl);
                }
            }

            return view;
        }
    }
}

