using System;
using AppKit;
using ReactiveUI;
namespace RedditPlayer.Mac.Views.Detail
{
    public class SearchBarViewController : ReactiveViewController
    {
        new SearchBarView View
        {
            get { return (SearchBarView)base.View; }
            set { base.View = value; }
        }

        public SearchBarViewController (ApplicationViewModel viewModel)
        {

        }
    }
}

