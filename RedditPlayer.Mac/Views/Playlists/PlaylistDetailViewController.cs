using System;
using ReactiveUI;
using System.Reactive.Linq;
namespace RedditPlayer.Mac.Views.Playlists
{
    public class PlaylistDetailViewController : ReactiveViewController
    {
        new PlaylistDetailView View
        {
            get { return (PlaylistDetailView)base.View; }
            set { base.View = value; }
        }

        public PlaylistDetailViewController (ApplicationViewModel viewModel)
        {
            View = new PlaylistDetailView ();

            var reddit = viewModel.WhenAnyValue (vm => vm.SelectedSubReddit)
                                  .Where (r => r != null);

            reddit.Select (r => r.Title)
                  .Where (title => !string.IsNullOrEmpty (title))
                  .BindTo (View.TitleLabel, label => label.StringValue);

            reddit.Select (r => r.Description)
                  .Where (description => !string.IsNullOrEmpty (description))
                  .BindTo (View.DescriptionLabel, label => label.StringValue);
        }
    }
}

