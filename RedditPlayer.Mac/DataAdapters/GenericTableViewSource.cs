using System;
using AppKit;
using System.Collections.Generic;
using ReactiveUI;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using Foundation;
using System.Diagnostics;

namespace RedditPlayer.Mac.DataAdapters
{
    public class GenericTableViewSource<TItem> : NSTableViewSource, INSTableViewDelegate
        where TItem : class 
    {
        NSTableView tableView;
        IReactiveList<TItem> items;

        Action<TItem, string, NSView> viewBuilder;
        Func<TItem, string, NSView> viewCreator;
        Func<TItem, NSTableColumn, string> identifierForItem;

        public Func<int, NSTableRowView> RowViewCreator { get; set; }

        public delegate void DoubleClickHandler (TItem item);
        public event DoubleClickHandler DoubleClicked;

        float? cachedRowHeight;

        public GenericTableViewSource (NSTableView tableView, IReactiveList<TItem> items, Func<TItem, NSTableColumn, string> identifierForItem, Func<TItem, string, NSView> viewCreator, Action<TItem, string, NSView> viewBuilder)
        {
            this.items = items;
            this.viewBuilder = viewBuilder;
            this.viewCreator = viewCreator;
            this.tableView = tableView;
            this.identifierForItem = identifierForItem;

            tableView.DoubleAction = new ObjCRuntime.Selector ("doubleClick:");
            tableView.Target = this;

            items.Changed.Subscribe (args => tableView.ReloadData ());
        }

        public override NSTableRowView GetRowView (NSTableView tableView, nint row)
        {
            if (RowViewCreator != null) {
                return RowViewCreator ((int)row);
            }

            return new GrayRowView ();
        }

        public override nfloat GetRowHeight (NSTableView tableView, nint row)
        {
            if (cachedRowHeight == null)
            {
                var view = viewCreator (items[(int)row], "");
                cachedRowHeight = (float)view.FittingSize.Height;
            }

            return cachedRowHeight.Value;
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            return items.Count;
        }

        public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var item = items [(int)row] as TItem;
            var identifier = identifierForItem (item, tableColumn);

            var view = tableView.MakeView (identifier, this);
            if (view == null) {
                view = viewCreator (item, identifier);
            }

            viewBuilder (item, identifier, view);

            return view;
        }

        public override void DidAddRowView (NSTableView tableView, NSTableRowView rowView, nint row)
        {
            var view = rowView.ViewAtColumn (0);
            view.Superview.AddConstraints (FillHorizontal (view, false));
            view.Superview.AddConstraints (FillVertical (view, false));
        }

        [Export("doubleClick:")]
        public void DoubleClick(NSObject sender)
        {
            DoubleClicked?.Invoke (items? [(int)tableView.ClickedRow]);
        }
    }
}

