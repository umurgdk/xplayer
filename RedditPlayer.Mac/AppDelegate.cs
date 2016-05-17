using AppKit;
using Foundation;
using System.Diagnostics;
using RedditPlayer;
using RedditPlayer.Mac.Views;

namespace RedditPlayer.Mac
{
    [Register ("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate ()
        {
        }

        public override void DidFinishLaunching (NSNotification notification)
        {
            // Insert code here to initialize your application
            Debug.WriteLine ("Hello world");

            var applicationViewModel = new ApplicationViewModel ();

            var playerWindow = new PlayerWindow (applicationViewModel);

            var mainWindowController = new MainWindowController (applicationViewModel, playerWindow);
            mainWindowController.ShowWindow (this);
        }

        public override void WillTerminate (NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}

