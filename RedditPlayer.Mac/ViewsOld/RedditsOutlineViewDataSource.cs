using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AppKit;
using Foundation;
using ReactiveUI;
using RedditPlayer.Domain.Reddit;
namespace RedditPlayer.Mac.Views
{
    public class RedditsOutlineViewDataSource : NSOutlineViewDataSource
    {
        public readonly IList<RedditsSourceItem> items;
        readonly NSOutlineView outlineView;

        public RedditsOutlineViewDataSource (NSOutlineView outlineView, IReactiveList<SubReddit> subReddits)
        {
            this.outlineView = outlineView;
            items = new List<RedditsSourceItem> {
                new RedditsSourceItem("Favorites", subReddits.CreateDerivedCollection (r => new RedditsSourceItem(r)))
            };
        }

        public override nint GetChildrenCount (NSOutlineView outlineView, NSObject item)
        {
            if (item == null) {
                return items.Count;
            }

            var sourceItem = item as RedditsSourceItem;
            sourceItem.Children.Reset ();
            return sourceItem.Children.Count;
        }

        public override bool ItemExpandable (NSOutlineView outlineView, NSObject item)
        {
            var sourceItem = item as RedditsSourceItem;
            return sourceItem.IsCategory;
        }

        public override NSObject GetChild (NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            if (item == null) {
                return items [(int)childIndex];
            }

            var sourceItem = item as RedditsSourceItem;
            return sourceItem.Children [(int)childIndex];
        }

        public override NSObject GetObjectValue (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var sourceItem = item as RedditsSourceItem;
            return FromObject (sourceItem.Title);
        }

        public RedditsSourceItem ItemForRow (int row)
        {
            int index = 0;

            foreach (var item in items) {
                if (row >= index && row <= (index + item.Children.Count)) {
                    return item.Children [row - index - 1];
                }

                index += item.Children.Count + 1;
            }

            return null;
        }
    }
}

