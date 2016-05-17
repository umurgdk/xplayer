using System;
using AppKit;
namespace RedditPlayer.Mac.DataAdapters
{
    public class GenericOutlineViewDelegate<T> : NSOutlineViewDelegate
    {
        GenericOutlineViewSource<T> dataSource;

        public GenericOutlineViewDelegate (NSOutlineView outlineView, GenericOutlineViewSource<T> dataSource)
        {
            this.dataSource = dataSource;
        }

        public override NSView GetView (NSOutlineView outlineView, NSTableColumn tableColumn, Foundation.NSObject item)
        {
            var genericItem = item as GenericOutlineItemWrapper;
            var view = outlineView.MakeView (GenericOutlineCellView.KIdentifier, this) as GenericOutlineCellView;

            if (view == null) {
                view = new GenericOutlineCellView ();
            }

            view.SetItem (genericItem);

            return view;
        }

        public override nfloat GetRowHeight (NSOutlineView outlineView, Foundation.NSObject item)
        {
            return 22;
        }

        public override NSTableRowView RowViewForItem (NSOutlineView outlineView, Foundation.NSObject item)
        {
            return new GenericOutlineRowView ();
        }

        public override bool IsGroupItem (NSOutlineView outlineView, Foundation.NSObject item)
        {
            var genericItem = item as GenericOutlineItemWrapper;
            return genericItem.IsCategory;
        }

        public override bool ShouldSelectItem (NSOutlineView outlineView, Foundation.NSObject item)
        {
            var genericItem = item as GenericOutlineItemWrapper;
            return !genericItem.IsCategory;
        }

        public override bool ShouldShowOutlineCell (NSOutlineView outlineView, Foundation.NSObject item)
        {
            return false;
        }
    }
}

