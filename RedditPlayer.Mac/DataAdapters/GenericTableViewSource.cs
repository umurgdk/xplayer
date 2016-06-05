using System;
using AppKit;
using System.Collections.Generic;
using ReactiveUI;
namespace RedditPlayer.Mac.DataAdapters
{
    public class GenericTableViewSource<T> : NSTableViewSource, INSTableViewDelegate where T : class
    {
        NSTableView tableView;
        IReactiveList<T> items;

        Action<T, string, NSView> viewBuilder;
        Func<T, string, NSView> viewCreator;
        Func<T, NSTableColumn, string> identifierForItem;

        public Func<int, NSTableRowView> RowViewCreator { get; set; }

        public GenericTableViewSource (NSTableView tableView, IReactiveList<T> items, Func<T, NSTableColumn, string> identifierForItem, Func<T, string, NSView> viewCreator, Action<T, string, NSView> viewBuilder)
        {
            this.items = items;
            this.viewBuilder = viewBuilder;
            this.viewCreator = viewCreator;
            this.tableView = tableView;
            this.identifierForItem = identifierForItem;

            items.Changed.Subscribe (args => tableView.ReloadData ());
        }

        public override NSTableRowView GetRowView (NSTableView tableView, nint row)
        {
            if (RowViewCreator != null) {
                return RowViewCreator ((int)row);
            } else {
                return base.GetRowView (tableView, row);
            }
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            return items.Count;
        }

        public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var item = items [(int)row] as T;
            var identifier = identifierForItem (item, tableColumn);

            var view = tableView.MakeView (identifier, this);
            if (view == null) {
                view = viewCreator (item, identifier);
            }

            viewBuilder (item, identifier, view);

            return view;
        }
    }
}

