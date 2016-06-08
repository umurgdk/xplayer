using System;
using AppKit;
namespace RedditPlayer.Mac.Views
{
	public class PlayerWindowController : NSWindowController
	{
		public PlayerWindowController ()
		{
			Window = new PlayerWindow();
		}

		public void PresentView (NSView view)
		{
			Window.ContentView = view;
		}

		public new PlayerWindow Window
		{
			get
			{
				return (PlayerWindow)base.Window;
			}
			set
			{
				base.Window = value;
			}
		}
	}
}

