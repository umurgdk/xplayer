using System;
using AppKit;
using Foundation;
using CoreGraphics;

namespace RedditPlayer.Mac.DataAdapters
{
    [Flags]
    public enum BorderLocation
    {
        Top,
        Right,
        Bottom,
        Left,
        None
    }

    public class GrayRowView : NSTableRowView
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

        public BorderLocation BorderLocations { get; set; } = BorderLocation.None;
        public NSColor BorderColor { get; set; }

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
                var genericCellView = ViewAtColumn (0) as ISelectableView;

                if (genericCellView != null) {
                    genericCellView.DidSelectionChanged (Selected);
                }
            }
        }
    }
}