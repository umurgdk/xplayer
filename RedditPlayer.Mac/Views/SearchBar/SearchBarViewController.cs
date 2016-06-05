using System;
using AppKit;
using ReactiveUI;
using RedditPlayer.ViewModels;
using System.Reactive.Linq;
namespace RedditPlayer.Mac.Views.SearchBar
{
    public class SearchBarViewController : ReactiveViewController
    {
        SearchViewModel viewModel;

        public new SearchBarView View
        {
            get { return (SearchBarView)base.View; }
            set { base.View = value; }
        }

        public SearchBarViewController (SearchViewModel viewModel)
        {
            View = new SearchBarView ();

            View.SearchField.Changed += (sender, e) => viewModel.Query = View.SearchField.StringValue;
            View.SearchField.Activated += OnSearchActivated;

            viewModel.WhenAnyValue (vm => vm.Query)
                     .Where (query => query != null)
                     .BindTo (View.SearchField, searchField => searchField.StringValue);

            viewModel.WhenAnyObservable (vm => vm.Search.IsExecuting)
                     .Select (b => !b)
                     .BindTo (View.ProgressIndicator, indicator => indicator.Hidden);

            this.viewModel = viewModel;
        }

        public void OnSearchActivated (object sender, EventArgs args)
        {
            if (viewModel.Search.CanExecute(null))
            {
                viewModel.Search.Execute (null);
                View.Window.MakeFirstResponder (null);
            }
        }
    }
}

