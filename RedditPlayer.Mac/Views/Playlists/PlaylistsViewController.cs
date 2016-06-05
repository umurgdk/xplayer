using System;
using ReactiveUI;
using RedditPlayer.Domain.Reddit;
using RedditPlayer.Mac.DataAdapters;
namespace RedditPlayer.Mac.Views.Playlists
{
    public class PlaylistsViewController : ReactiveViewController
    {
        GenericOutlineViewSource<SubReddit> dataSource;
        GenericOutlineViewDelegate<SubReddit> dataDelegate;

        public PlaylistsViewController (ApplicationViewModel viewModel)
        {
            var view = new PlaylistsView ();

            dataSource = new GenericOutlineViewSource<SubReddit> (
                view.OutlineView,
                viewModel.Reddits,
                r => r.Grouping,
                r => r.Grouping == "Favorite" ? "Star" : "Playlist",
                r => r.Title);

            dataDelegate = new GenericOutlineViewDelegate<SubReddit> (view.OutlineView, dataSource);

            view.OutlineView.DataSource = dataSource;
            view.OutlineView.Delegate = dataDelegate;

            view.OutlineView.ExpandItem (null, true);

            View = view;
        }
    }
}

