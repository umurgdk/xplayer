using System;
using AppKit;
using Foundation;

namespace RedditPlayer.Mac.DataAdapters
{
    class GenericOutlineRowView : NSTableRowView
    {
        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;
                SetViewSelected ();
            }
        }

        public override void DrawSeparator (CoreGraphics.CGRect dirtyRect)
        {

        }

        public override void DrawBackground (CoreGraphics.CGRect dirtyRect)
        {

        }

        public override void DrawSelection (CoreGraphics.CGRect dirtyRect)
        {
            NSColor.FromRgb (225, 225, 225).Set ();
            NSBezierPath.FillRect (dirtyRect);
        }

        void SetViewSelected ()
        {
            if (NumberOfColumns > 0) {
                var genericCellView = ViewAtColumn (0) as GenericOutlineCellView;
                genericCellView.Selected = Selected;
            }
        }
    }
}