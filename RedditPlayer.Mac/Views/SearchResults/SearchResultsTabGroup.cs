using System;
using System.Collections.Generic;
using AppKit;
using CoreGraphics;
using Foundation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SearchResults
{
    [Register ("SearchResultsTabGroup")]
    public class SearchResultsTabGroup : ReactiveView
    {
        NSStackView tabsStack;
        Dictionary<string, SearchResultsTabView> tabViews;
        string activeTabIdentifier;

        public delegate void ActiveTabChangedHandler (string identifier);
        public event ActiveTabChangedHandler ActiveTabChanged;

        [Reactive]
        public bool DarkTheme { get; set; }

        #region Constructors

        public SearchResultsTabGroup ()
        {
            Initialize ();
        }

        public SearchResultsTabGroup (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        public SearchResultsTabGroup (NSObjectFlag t) : base (t)
        {
            Initialize ();
        }

        public SearchResultsTabGroup (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        public SearchResultsTabGroup (CoreGraphics.CGRect frameRect) : base (frameRect)
        {
            Initialize ();
        }

        #endregion

        //public override bool IsOpaque => true;

        void Initialize ()
        {
            //WantsLayer = true;
            TranslatesAutoresizingMaskIntoConstraints = false;

            tabsStack = new NSStackView ();
            tabsStack.TranslatesAutoresizingMaskIntoConstraints = false;
            tabsStack.Orientation = NSUserInterfaceLayoutOrientation.Horizontal;
            tabsStack.Spacing = 0;

            AddSubview (tabsStack);

            AddConstraints (FillHorizontal (false, tabsStack));
            AddConstraints (FillVertical (false, tabsStack));

            tabViews = new Dictionary<string, SearchResultsTabView> ();
        }

        public void AddTab (string identifier, string iconName, string title)
        {
            if (tabViews.ContainsKey (identifier))
                return;

            var tabView = new SearchResultsTabView (DarkTheme);

            tabView.ImageView.Image = NSImage.ImageNamed (iconName);
            tabView.TextField.StringValue = title;

            tabViews [identifier] = tabView;
            tabsStack.AddArrangedSubview (tabView);
        }

        public void RemoveTab (string identifier)
        {
            if (tabViews.ContainsKey (identifier)) {
                var tabView = tabViews [identifier];
                tabsStack.RemoveArrangedSubview (tabView);
                tabViews.Remove (identifier);
            }
        }

        public void ActivateTab (string identifier)
        {
            if (activeTabIdentifier == identifier)
                return;

            foreach (var tab in tabViews) {
                if (tab.Key == identifier) {
                    tab.Value.Selected = true;
                    activeTabIdentifier = identifier;
                    ActiveTabChanged?.Invoke (identifier);
                } else {
                    tab.Value.Selected = false;
                }
            }
        }

        public override void DrawRect (CoreGraphics.CGRect dirtyRect)
        {
            if (DarkTheme) {
                NSColor.Clear.Set ();
                NSGraphics.RectFill (dirtyRect, NSCompositingOperation.SourceOver);
                return;
            }

            if (dirtyRect.Y == 0 && !DarkTheme) {
                NSColor.FromRgb (233, 233, 233).Set ();
                NSBezierPath.FillRect (new CGRect (dirtyRect.X, 0, dirtyRect.Width, 1));
            }
        }

        public override void MouseDown (NSEvent theEvent)
        {
            var location = theEvent.LocationInWindow;
            var point = ConvertPointFromView (location, null);

            foreach (var tab in tabViews) {
                if (tab.Value.Frame.Contains (point)) {
                    ActivateTab (tab.Key);
                    break;
                }
            }
        }
    }
}

