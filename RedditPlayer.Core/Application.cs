using System;
using RedditPlayer.Services;
using System.Reactive.Linq;
using System.Diagnostics;
using RedditPlayer.Domain.MediaProviders;
using Splat;
namespace RedditPlayer
{
    public class Application
    {
        public ApplicationViewModel ViewModel { get; protected set; }

        public Application (ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void Start ()
        {
            var settings = Locator.CurrentMutable.GetService<ISettings> ();

#if DEBUG
            settings.RemoveAll ();
#endif

            if (settings.FirstRun)
                SetFirstRunSettings (settings);

            var navigator = Locator.CurrentMutable.GetService<INavigator> ();
            navigator.PresentWelcomeScreen ();
            navigator.ShowWindow (this);
        }

        void SetFirstRunSettings (ISettings settings)
        {
            Debug.WriteLine ("Setting first run configurations!!!");
            settings.Volume = 1.0f;
            settings.FirstRun = false;
            settings.NumberOfPopularSongs = 5;
        }
    }
}




