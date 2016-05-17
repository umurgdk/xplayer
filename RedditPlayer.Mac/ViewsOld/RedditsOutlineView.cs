using System;
using AppKit;
using CoreGraphics;
using Foundation;
namespace RedditPlayer.Mac.Views
{
    [Register ("RedditsOutlineView")]
    public class RedditsOutlineView : NSOutlineView
    {
        const float OutlineCellWidth = 11;
        const float OutlineMinLeftMargin = 14;

        public RedditsOutlineView (IntPtr handle) : base (handle)
        {
        }

        public override CGRect GetCellFrame (nint column, nint row)
        {
            var superFrame = base.GetCellFrame (column, row);

            if (column != 0) {
                return superFrame;
            }

            float adjustment = OutlineCellWidth;

            if (superFrame.Location.X - adjustment < OutlineMinLeftMargin) {
                adjustment = (float)Math.Max (0, superFrame.Location.X - OutlineMinLeftMargin);
            }

            //return new CGRect (superFrame.Location.X - adjustment, superFrame.Location.Y, superFrame.Size.Width + adjustment, superFrame.Size.Height);
            return new CGRect (0 - adjustment, superFrame.Location.Y, superFrame.Size.Width + adjustment, superFrame.Size.Height);
        }
    }
}

