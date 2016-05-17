using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace RedditPlayer.Mac.Views.Detail
{
    public class DetailViewController : ReactiveViewController, IDisposable
    {
        new DetailView View
        {
            get { return (DetailView)base.View; }
            set { base.View = value; }
        }

        public DetailViewController (ApplicationViewModel viewModel)
        {
            View = new DetailView ();

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

