using System;
using ReactiveUI;
using AppKit;
using System.Collections.Generic;
using CoreAnimation;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using CoreGraphics;
using RedditPlayer.Mac.Extensions;

namespace RedditPlayer.Mac.Views
{
	struct BreadcrumbViewItem
	{
		public string Identifier;
		public string Title;
		public CATextLayer Layer;
	}

	public class BreadcrumbView : ReactiveView
	{
		public const float Height = 30;

		public delegate void NavigationRequestedHandler (string identifier);
		public event NavigationRequestedHandler NavigationRequested;

		NSStackView stack;
		List<BreadcrumbViewItem> items;

		public override CGSize IntrinsicContentSize => new CGSize (100, Height);

		public override bool WantsUpdateLayer => true;

		NSTrackingArea [] trackingAreas;

		List<CAShapeLayer> arrowLayers;

		public BreadcrumbView ()
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			WantsLayer = true;
			items = new List<BreadcrumbViewItem> ();

			stack = new NSStackView ();
			stack.Orientation = NSUserInterfaceLayoutOrientation.Horizontal;
			stack.TranslatesAutoresizingMaskIntoConstraints = false;

			AddSubview (stack);
			BuildConstraints ();
		}

		public void AddItem (string identifier, string title)
		{
			var layer = new CATextLayer ();
			layer.String = title;
			layer.SetFont ("SF UI Text Bold");
			layer.FontSize = 12;
			layer.ForegroundColor = NSColor.FromRgb (80, 80, 80).CGColor;
			layer.ContentsScale = NSScreen.MainScreen.BackingScaleFactor;

			var attrs = new NSStringAttributes ();
			attrs.Font = NSFont.FromFontName ("SF UI Text Bold", 12);

			var stringSize = NSStringDrawing.StringSize (layer.String, attrs);
			var frame = new CGRect (new CGPoint (0, 0), stringSize);

			layer.Frame = frame;
			layer.AnchorPoint = new CGPoint (0, 0.5f);

			items.Add (new BreadcrumbViewItem {
				Identifier = identifier,
				Title = title,
				Layer = layer
			});

			Layer.AddSublayer (layer);

			NeedsDisplay = true;
		}

		public void RemoveItem (string identifier)
		{
			var foundItems = items.FindAll (x => x.Identifier == identifier);

			if (foundItems.Count > 0) {
				foundItems [0].Layer.RemoveFromSuperLayer ();
				items.Remove (foundItems [0]);
				NeedsDisplay = true;
			}
		}

		public override void UpdateLayer ()
		{
			Layer.BackgroundColor = NSColor.FromRgb (245, 245, 245).CGColor;

			if (arrowLayers != null) {
				arrowLayers.ForEach (layer => layer.RemoveFromSuperLayer ());
			}

			arrowLayers = new List<CAShapeLayer> ();

			nfloat lastX = 8;
			var lastItem = items [items.Count - 1];
			foreach (var item in items) {
				item.Layer.Position = new CGPoint (lastX, Bounds.GetMidY () - 1);
				lastX += item.Layer.Frame.Width + 20;

				if (item.Equals (lastItem))
					continue;

				var arrow = new CAShapeLayer ();
				var path = new NSBezierPath ();
				path.MoveTo (new CGPoint (item.Layer.Frame.Right + 5, Bounds.Height));
				path.LineTo (new CGPoint (item.Layer.Frame.Right + 15, Bounds.GetMidY ()));
				path.LineTo (new CGPoint (item.Layer.Frame.Right + 5, 0));

				arrow.ContentsScale = NSScreen.MainScreen.BackingScaleFactor;
				arrow.Path = path.ToCGPath (false);
				arrow.FillColor = null;
				arrow.StrokeColor = NSColor.FromWhite (0.85f, 1.0f).CGColor;

				arrowLayers.Add (arrow);
				Layer.AddSublayer (arrow);
			}
		}

		void BuildConstraints ()
		{
			AddConstraints (FillVertical (stack, false));
			AddConstraints (FillHorizontal (stack, false));
		}

		public override void MouseUp (NSEvent theEvent)
		{
			var locationInWindow = theEvent.LocationInWindow;
			var location = ConvertPointFromView (locationInWindow, null);

			foreach (var item in items) {
				if (item.Layer.Frame.Contains (location)) {
					NavigationRequested?.Invoke (item.Identifier);
					break;
				}
			}
		}
	}
}

