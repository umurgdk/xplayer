using System;
using System.Collections.Generic;
using AppKit;
using ReactiveUI;
using RedditPlayer.Mac.Extensions;

namespace RedditPlayer.Mac.Views.Detail
{
    public class DetailView : ReactiveView, IDisposable
    {
        const string TitleFontName = "SF UI Display Thin";
        const float TitleFontSize = 22;

        const string DescriptionFontName = "SF UI Display Regular";
        const int DescriptionFontSize = 12;

        public readonly NSTextField TitleLabel;
        public readonly NSTextField DescriptionLabel;
        public readonly SearchBarView SearchBarView;

        public DetailView ()
        {
            TitleLabel = NSLabel.CreateWithFont (TitleFontName, TitleFontSize);
            TitleLabel.Identifier = "TitleLabel";
            TitleLabel.TextColor = NSColor.FromRgb (51, 51, 51);

            DescriptionLabel = NSLabel.CreateWithFont (DescriptionFontName, DescriptionFontSize);
            DescriptionLabel.Identifier = "DescriptionLabel";
            DescriptionLabel.TextColor = NSColor.FromRgb (142, 142, 142);

            SearchBarView = new SearchBarView ();
            SearchBarView.Identifier = "SearchBarView";

            AddSubview (SearchBarView);
            AddSubview (TitleLabel);
            AddSubview (DescriptionLabel);

            AddDefaultLayoutConstraints ();
        }

        public override void ViewDidMoveToWindow ()
        {
            base.ViewDidMoveToWindow ();
        }

        void AddDefaultLayoutConstraints ()
        {
            var layoutConstraints = new List<NSLayoutConstraint> { };

            layoutConstraints.AddRange (NSLayoutExtensions.FillHorizontal (SearchBarView, false));
            layoutConstraints.AddRange (NSLayoutExtensions.FillHorizontal (TitleLabel, true));
            layoutConstraints.AddRange (NSLayoutExtensions.FillHorizontal (DescriptionLabel, true));
            layoutConstraints.AddRange (NSLayoutExtensions.Stack (StackOrientation.Vertical, true, true, true, SearchBarView, TitleLabel, DescriptionLabel));

            AddConstraints (layoutConstraints.ToArray ());
        }
    }
}

