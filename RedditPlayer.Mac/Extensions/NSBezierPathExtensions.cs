using System;
using CoreGraphics;
using AppKit;
namespace RedditPlayer.Mac.Extensions
{
	public static class NSBezierPathExtensions
	{
		public static CGPath ToCGPath (this NSBezierPath path)
		{
			return ToCGPath (path, true);
		}

		public static CGPath ToCGPath (this NSBezierPath path, bool closePath)
		{
			int i, numElements;

			// Need to begin a path here.
			CGPath cgpath = new CGPath ();

			// Then draw the path elements.
			numElements = (int)path.ElementCount;

			if (numElements > 0) {
				CGPoint [] points = new CGPoint [3];
				bool didClosePath = true;

				for (i = 0; i < numElements; i++) {
					switch (path.ElementAt (i, out points)) {
					case NSBezierPathElement.MoveTo:
						cgpath.MoveToPoint (points [0].X, points [0].Y);
						break;

					case NSBezierPathElement.LineTo:
						cgpath.AddLineToPoint (points [0].X, points [0].Y);
						didClosePath = false;
						break;

					case NSBezierPathElement.CurveTo:
						cgpath.AddCurveToPoint (points [0], points [1], points [2]);
						didClosePath = false;
						break;

					case NSBezierPathElement.ClosePath:
						cgpath.CloseSubpath ();
						didClosePath = true;
						break;
					}
				}

				// Be sure the path is closed or Quartz may not do valid hit detection.
				if (!didClosePath && closePath)
					cgpath.CloseSubpath ();
			}

			return cgpath;
		}
	}
}

