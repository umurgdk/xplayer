using System;
using AppKit;
namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerViewController : NSViewController
    {
        public new PlayerView View
        {
            get {
                return (PlayerView)base.View;
            }

            set {
                base.View = value;
            }
        }

        public PlayerViewController ()
        {
            View = new PlayerView ();
        }
    }
}

