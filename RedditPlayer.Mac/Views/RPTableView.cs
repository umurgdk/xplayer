using System;
using AppKit;
using CoreGraphics;
namespace RedditPlayer.Mac.Views
{
    public class RPTableView : NSTableView
    {
        public override void DrawGrid (CoreGraphics.CGRect clipRect)
        {
            var lastRowRect = RectForRow (RowCount - 1);
            var myClipRect = new CGRect (0, 0, lastRowRect.Width, lastRowRect.GetMaxY ());
            var finalClipRect = CGRect.Intersect (clipRect, myClipRect);

            base.DrawGrid (finalClipRect);
        }
    }
}

