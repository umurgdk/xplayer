using System;
using AppKit;
using System.Diagnostics;
namespace RedditPlayer.Mac.Views
{
    public class RedditsOutlineRowView : NSTableRowView
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
                NotifyCellViewSelected (value);
            }
        }

        public override void DrawRect (CoreGraphics.CGRect dirtyRect)
        {

        }

        void NotifyCellViewSelected (bool selected)
        {
            if (NumberOfColumns == 0) {
                return;
            }

            var view = ViewAtColumn (0) as RedditSourceItemCellView;

            if (view == null) {
                Debug.WriteLine ("Row view couldn't find view at column 0");
                return;
            }

            view.Selected = selected;
        }


    }
}

