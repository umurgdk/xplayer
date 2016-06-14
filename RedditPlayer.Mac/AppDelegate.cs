using AppKit;
using Foundation;
using System.Diagnostics;
using RedditPlayer;
using RedditPlayer.Mac.Views;
using WebKit;
using RedditPlayer.Services;
using RedditPlayer.Domain.MediaProviders;
using RedditPlayer.Mac.Players;
using Splat;
using RedditPlayer.Mac.Services;

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

            Locator.CurrentMutable.RegisterConstant (new IMediaProvider [] {
                new Youtube(new YoutubePlayer(windowController.Window)),
                new Soundcloud(new SoundcloudApi ("4fea3da6c7cb6807bcd29df897cb303e"), new SoundcloudPlayer())
            }, typeof (IMediaProvider []));

            var viewModel = new ApplicationViewModel ();

            Locator.CurrentMutable.RegisterConstant (viewModel, typeof (ApplicationViewModel));

            Locator.CurrentMutable.RegisterConstant (new Navigator (viewModel, windowController), typeof (INavigator));

            app = new Application (viewModel);
        }

        public override void DidFinishLaunching (NSNotification notification)
        {
            app.Start ();
        }

        public override void WillTerminate (NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}

