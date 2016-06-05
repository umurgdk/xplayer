using System;
using AppKit;
using RedditPlayer.Mac.Views.SongsList;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.Playlists
{
    public class PlaylistDetailView : NSView
    {
        const string TitleFontName = "SF UI Display Thin";
        const float TitleFontSize = 22;

        const string DescriptionFontName = "SF UI Display Regular";
        const int DescriptionFontSize = 12;

        public readonly NSTextField TitleLabel;
        public readonly NSTextField DescriptionLabel;
        public readonly SongsListView SongsTable;

        NSStackView outerStack;

        public PlaylistDetailView ()
        {
            TitleLabel = NSLabel.CreateWithFont (TitleFontName, TitleFontSize);
            TitleLabel.Identifier = "TitleLabel";
            TitleLabel.TextColor = NSColor.FromRgb (51, 51, 51);

            DescriptionLabel = NSLabel.CreateWithFont (DescriptionFontName, DescriptionFontSize);
            DescriptionLabel.Identifier = "DescriptionLabel";
            DescriptionLabel.TextColor = NSColor.FromRgb (142, 142, 142);

            SongsTable = new SongsListView ();

            outerStack = new NSStackView ();
            outerStack.TranslatesAutoresizingMaskIntoConstraints = false;
            outerStack.Orientation = NSUserInterfaceLayoutOrientation.Vertical;
            outerStack.Distribution = NSStackViewDistribution.Fill;
            outerStack.Spacing = 10;
            outerStack.EdgeInsets = new NSEdgeInsets (20, 0, 0, 0);

            outerStack.AddArrangedSubview (TitleLabel);
            outerStack.AddArrangedSubview (DescriptionLabel);
            outerStack.AddArrangedSubview (SongsTable);

            AddSubview (outerStack);

            AddConstraint (MinimumHeight (SongsTable, 100));
            AddConstraints (FillHorizontal (true, TitleLabel, DescriptionLabel));
            AddConstraints (FillHorizontal (outerStack, false));
            AddConstraints (FillVertical (outerStack, false));
        }
    }
}

