using AppKit;
using Foundation;
using System.Diagnostics;
using RedditPlayer;
using RedditPlayer.Mac.Views;
using WebKit;
using RedditPlayer.Services;

namespace RedditPlayer.Mac
{
    [Register ("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
		Application app;
		Navigator navigator;

        public AppDelegate ()
        {
			app = new Application (new Navigator ());
        }

        public override void DidFinishLaunching (NSNotification notification)
        {
            // Insert code here to initialize your application
            //NSUserDefault NSConstraintBasedLayoutVisualizeMutuallyExclusiveConstraints
            NSUserDefaults.StandardUserDefaults.SetBool (true, "NSConstraintBasedLayoutVisualizeMutuallyExclusiveConstraints");

			app.Start ();
        }

        public override void WillTerminate (NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}

