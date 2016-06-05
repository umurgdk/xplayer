using System;
using AppKit;
using CoreGraphics;
namespace RedditPlayer.Mac.Views.SongsList
{
    public class SongRowView : NSTableRowView
    {
        public override void DrawSelection (CGRect dirtyRect)
        {
            NSColor.FromRgb (100, 100, 100).Set ();
            NSBezierPath.FillRect (dirtyRect);
        }

        void NotifyCellViewSelected ()
        {
            if (NumberOfColumns > 0) {
                var songView = ViewAtColumn (0) as SongView;
                songView.DidSelectionChange (Selected);
            }
        }

        public override bool Selected
        {
            get
            {
                return base.Selected;
            }
            set
            {
                base.Selected = value;
                NotifyCellViewSelected ();
            }
        }

    }
}

