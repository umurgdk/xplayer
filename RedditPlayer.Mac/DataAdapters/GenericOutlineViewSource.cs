using System;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using Foundation;
using AppKit;
using System.Collections.Generic;
namespace RedditPlayer.Mac.DataAdapters
{
    public class GenericOutlineViewSource<T> : NSOutlineViewDataSource
    {
        IList<GenericOutlineItemWrapper> items;

        public GenericOutlineViewSource (NSOutlineView outlineView, IReactiveList<T> items, Func<T, string> groupBy, Func<T, string> iconExtractor, Func<T, string> titleExtractor)
        {
            items.Changed.Subscribe (args => ReloadItems (outlineView, items, groupBy, iconExtractor, titleExtractor));
            ReloadItems (outlineView, items, groupBy, iconExtractor, titleExtractor);
        }

        void ReloadItems (NSOutlineView outlineView, IReactiveList<T> _items, Func<T, string> groupBy, Func<T, string> iconExtractor, Func<T, string> titleExtractor)
        {
            this.items = _items
                    .GroupBy (groupBy)
                    .Select (group => {
                        var children = group.Select (item => new GenericOutlineItemWrapper (iconExtractor (item), titleExtractor (item))).ToList ();
                        return new GenericOutlineItemWrapper ("", group.Key, children, true);
                    })
                    .ToList ();

            outlineView.ReloadData ();
        }

        public override nint GetChildrenCount (NSOutlineView outlineView, NSObject item)
        {
            if (item == null) {
                return items.Count;
            }

            var genericItem = item as GenericOutlineItemWrapper;
            return genericItem.Children.Count;
        }

        public override NSObject GetChild (NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            if (item == null) {
                return FromObject (items [(int)childIndex]);
            }

            var genericItem = item as GenericOutlineItemWrapper;
            return genericItem.Children [(int)childIndex];
        }

        public override bool ItemExpandable (NSOutlineView outlineView, NSObject item)
        {
            var genericItem = item as GenericOutlineItemWrapper;
            return genericItem.IsCategory;
        }

        public override NSObject GetObjectValue (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var genericItem = item as GenericOutlineItemWrapper;
            return FromObject (genericItem.Text);
        }
    }
}

