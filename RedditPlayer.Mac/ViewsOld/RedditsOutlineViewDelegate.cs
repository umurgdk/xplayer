using System;
using System.Collections.Generic;
using AppKit;
using Foundation;
namespace RedditPlayer.Mac.Views
{
    public class RedditsOutlineViewDelegate : NSOutlineViewDelegate
    {
        public delegate void ItemSelectedDelegate (RedditsSourceItem item);
        public event ItemSelectedDelegate ItemSelected;

        readonly NSOutlineView outlineView;
        readonly RedditsOutlineViewDataSource dataSource;

        readonly IDictionary<string, NSTableRowView> RowViews;

        public RedditsOutlineViewDelegate (NSOutlineView outlineView, RedditsOutlineViewDataSource dataSource)
        {
            this.outlineView = outlineView;
            this.dataSource = dataSource;
            RowViews = new Dictionary<string, NSTableRowView> { };
        }

        public override bool ShouldEditTableColumn (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            return false;
        }

        public override NSView GetView (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var sourceItem = item as RedditsSourceItem;
            RedditSourceItemCellView view = outlineView.MakeView ("DataCell", this) as RedditSourceItemCellView;

            view.SetItem (sourceItem);

            return view;
        }

        public override bool ShouldSelectItem (NSOutlineView outlineView, NSObject item)
        {
            return outlineView.GetParent (item) != null;
        }

        public override void SelectionDidChange (NSNotification notification)
        {
            NSIndexSet indexSet = outlineView.SelectedRows;

            if (indexSet.Count > 1) {
                return;
            }

            var item = dataSource.ItemForRow ((int)indexSet.FirstIndex);

            if (item != null && ItemSelected != null) {
                ItemSelected (item);
            }
        }

        public override bool IsGroupItem (NSOutlineView outlineView, NSObject item)
        {
            var sourceItem = item as RedditsSourceItem;
            return sourceItem.IsCategory;
        }

        public override NSTableRowView RowViewForItem (NSOutlineView outlineView, NSObject item)
        {
            return new RedditsOutlineRowView ();
        }

        public override bool ShouldCollapseItem (NSOutlineView outlineView, NSObject item)
        {
            return false;
        }

        public override bool ShouldShowOutlineCell (NSOutlineView outlineView, NSObject item)
        {
            return false;
        }
    }
}

