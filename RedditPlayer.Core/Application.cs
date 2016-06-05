using System;
using RedditPlayer.Services;
using System.Reactive.Linq;
using System.Diagnostics;
namespace RedditPlayer
{
    public class Application
    {
        public ApplicationViewModel ViewModel { get; protected set; }
        public INavigator Navigator { get; protected set; }

        public Application(INavigator navigator)
        {
            Navigator = navigator;
            ViewModel = new ApplicationViewModel(navigator);

            ViewModel.Search.Search.Select (_ => {
                Debug.WriteLine ("Searching...");
                return 1;
            });
        }

        public void Start ()
        {
            Navigator.PresentWelcomeScreen (ViewModel.Search);
            Navigator.ShowWindow (this);
        }
    }
}




