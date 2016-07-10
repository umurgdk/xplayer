using AppKit;
using Foundation;
using RedditPlayer.Domain.MediaProviders;
using RedditPlayer.Mac.Players;
using RedditPlayer.Mac.Services;
using RedditPlayer.Mac.Views;
using RedditPlayer.Services;
using Splat;
using System;

namespace RedditPlayer.Mac
{
    [Register ("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        Application app;
        Navigator navigator;

        public AppDelegate ()
        {

            NSUserDefaults.StandardUserDefaults.SetBool (true, "NSConstraintBasedLayoutVisualizeMutuallyExclusiveConstraints");
            NSUserDefaults.StandardUserDefaults.SetBool (true, "WebKitDeveloperExtras");

            Locator.CurrentMutable.Register (() => new SharedTimer (), typeof (ITimer));
            Locator.CurrentMutable.RegisterConstant (new Settings (), typeof (ISettings));

            var windowController = new PlayerWindowController ();
            var mainViewController = new MainViewController ();

            //windowController.PresentView (mainViewController.View);

            Locator.CurrentMutable.RegisterConstant (new IMediaProvider [] {
                new Spotify(new SpotifyPlayer()),
                new Youtube(new YoutubePlayer(windowController.Window)),
                new Soundcloud(new SoundcloudApi ("4fea3da6c7cb6807bcd29df897cb303e"), new SoundcloudPlayer())
            }, typeof (IMediaProvider []));

            var viewModel = new ApplicationViewModel ();

            //var playlistViewController = new PlaylistsViewController (viewModel);
            //playlistViewController.View.SetContentHuggingPriorityForOrientation (500, NSLayoutConstraintOrientation.Horizontal);
            //mainViewController.PresentSidebar (playlistViewController.View);

            Locator.CurrentMutable.RegisterConstant (viewModel, typeof (ApplicationViewModel));

            Locator.CurrentMutable.RegisterConstant (new Navigator (viewModel, windowController, mainViewController), typeof (INavigator));

            app = new Application (viewModel);
        }

        public override void DidFinishLaunching (NSNotification notification)
        {
            app.Start ();

            //NSError error;
            //SPSession.CreateSharedSession (NSData.FromArray (appKey), "per.umurgdk.xplayer", (uint)SPAsyncLoadingPolicy.Immediate, out error);
            //SPSession.Shared.WeakDelegate = this;

            //playbackManager = new SPPlaybackManager (SPSession.Shared);

            //SPSession.Shared.AttemptLogin ("umur.gedik", "1Xiackok9m9u1r");
        }

        public override void WillTerminate (NSNotification notification)
        {
            // Insert code here to tear down your application
        }

        //[Export ("sessionDidLoginSuccessfully:")]
        //public void DidLoginSuccessfully (SPSession session)
        //{
        //    Console.WriteLine ("Logged in");
        //}
    }
}

