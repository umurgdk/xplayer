using System;
using WebKit;

using RedditPlayer.Domain.Media;
using Foundation;
using AppKit;
using CoreGraphics;
using System.Timers;

namespace RedditPlayer.Mac.Players
{
    public class YoutubePlayer : NSObject, IPlayer, IWebFrameLoadDelegate
    {
        Track currentTrack;

        NSWindow window;
        WebView webView;

        bool loadingNewPlayer = false;

        public YoutubePlayer (NSWindow window)
        {
            this.window = window;
        }

        public void Pause ()
        {
            webView.WindowScriptObject.EvaluateWebScript ("pauseVideo()");
        }

        public void Play ()
        {
            webView.WindowScriptObject.EvaluateWebScript ("playVideo()");
        }

        public void Play (Track track)
        {
            if (track != currentTrack) {
                currentTrack = track;
                LoadTrackPage (track);
                return;
            }

            Play ();
        }

        void LoadTrackPage (Track track)
        {
            loadingNewPlayer = false;

            if (webView != null) {
                webView.FrameLoadDelegate = null;
                webView.MainFrame.LoadHtmlString (new NSString ("<html><body></body></html>"), NSUrl.FromString ("about:blank"));
            }

            webView = new WebView (new CGRect (0, 0, 10, 10), "youtube player", "players");
            webView.FrameLoadDelegate = this;

            var url = string.Format ("https://www.youtube.com/watch?v={0}", track.UniqueId);
            var request = NSUrlRequest.FromUrl (NSUrl.FromString (url));

            webView.Preferences.JavaScriptEnabled = false;
            webView.MainFrame.LoadRequest (request);

            window.ContentView.AddSubview (webView);
            webView.Hidden = true;
        }

        [Export ("webView:didFinishLoadForFrame:")]
        public void FinishedLoad (WebView sender, WebFrame frame)
        {
            if (!loadingNewPlayer) {
                Console.WriteLine ("Frame loaded!!!");
                var playerScriptPath = NSBundle.MainBundle.PathForResource ("YoutubePlayer", "js");
                var playerScript = NSData.FromFile (playerScriptPath).ToString (NSStringEncoding.UTF8).ToString ();

                webView.Preferences.JavaScriptEnabled = true;
                sender.WindowScriptObject.EvaluateWebScript (playerScript.Replace ("{{videoId}}", currentTrack.UniqueId));
                Play ();
                loadingNewPlayer = true;
                return;
            }

            loadingNewPlayer = false;
        }

        public TimeSpan GetElapsedTime ()
        {
            try {
                var number = webView.StringByEvaluatingJavaScriptFromString ("(function () {try { return getElapsedSeconds(); } catch (e) {}})()");

                var seconds = double.Parse (number);
                var span = TimeSpan.FromSeconds (seconds);

                return span;
            } catch (Exception e) {
                return TimeSpan.FromSeconds (0);
            }
        }

        public void Mute ()
        {
            if (webView != null) {
                webView.WindowScriptObject.EvaluateWebScript ("mute()");
            }
        }

        public void Unmute ()
        {
            if (webView != null) {
                webView.WindowScriptObject.EvaluateWebScript ("unmute()");
            }
        }

        public void SetVolume (float volume)
        {
            if (webView != null) {
                webView.WindowScriptObject.EvaluateWebScript ($"setVolume({volume})");
            }
        }
    }
}

