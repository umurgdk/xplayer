using System;
using AppKit;
namespace RedditPlayer.Mac.Views
{
    public class MainViewController : NSSplitViewController
    {
        public MainViewController ()
        {
            View = new MainView ();
        }

        public void PresentSidebar (NSView view)
        {
            View.Sidebar = view;
        }

        public void PresentContent (NSView view)
        {
            View.Content = view;
        }

        public new MainView View
        {
            get
            {
                return (MainView)base.View;
            }
            set
            {
                base.View = value;
            }
        }
    }
}

