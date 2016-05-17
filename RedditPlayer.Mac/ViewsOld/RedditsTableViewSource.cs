using System;
using AppKit;
using ReactiveUI;
using Foundation;
using System.Reactive.Linq;
using RedditPlayer.Domain.Reddit;
namespace RedditPlayer.Mac
{
    public class RedditsTableViewSource : NSTableViewSource
    {
        IReactiveList<SubReddit> reddits;

        public event EventHandler SelectionChanged;

        public IObservable<SubReddit> SelectedSubReddit { get; protected set; }

        public RedditsTableViewSource (IReactiveList<SubReddit> reddits)
        {
            this.reddits = reddits;
            SelectedSubReddit = Observable.Empty<SubReddit> ();
        }

        public override nint GetRowCount (NSTableView tableView)
        {
            return reddits.Count;
        }

        public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var view = (RedditTableItemView)tableView.MakeView ("RedditTableItemView", this);

            if (view != null) {
                view.Title.StringValue = reddits [(int)row].Title;
            }

            return view;
        }

        public override void SelectionDidChange (NSNotification notification)
        {
            if (SelectionChanged != null) {
                SelectionChanged.Invoke (this, EventArgs.Empty);
            }
        }
    }
}

