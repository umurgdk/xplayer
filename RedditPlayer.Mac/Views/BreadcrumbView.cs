using System;
using ReactiveUI;
using AppKit;
using System.Collections.Generic;
using CoreAnimation;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;
using CoreGraphics;
namespace RedditPlayer.Mac.Views
{
    struct BreadcrumbViewItem
    {
        public string Identifier;
        public string Title;
        public CATextLayer Layer;
    }

    public class BreadcrumbView : ReactiveView
    {
        public const float Height = 30;

        public delegate void ClickedHandler (string identifier);
        public event ClickedHandler Clicked;

        NSStackView stack;
        List<BreadcrumbViewItem> items;

        public override CGSize IntrinsicContentSize => new CGSize (100, Height);

        public override bool WantsUpdateLayer => true;

        public BreadcrumbView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            WantsLayer = true;
            items = new List<BreadcrumbViewItem> ();

            stack = new NSStackView ();
            stack.Orientation = NSUserInterfaceLayoutOrientation.Horizontal;
            stack.TranslatesAutoresizingMaskIntoConstraints = false;

            AddSubview (stack);
            BuildConstraints ();
        }

        public void AddItem (string identifier, string title)
        {
            var layer = new CATextLayer ();
            layer.String = title;
            layer.SetFont ("SF UI Text Bold");
            layer.FontSize = 12;

            items.Add (new BreadcrumbViewItem {
                Identifier = identifier,
                Title = title,
                Layer = layer
            });

            Layer.AddSublayer (layer);
        }

        public void RemoveItem (string identifier)
        {
            var foundItems = items.FindAll (x => x.Identifier == identifier);

            if (foundItems.Count > 0) {
                items.Remove (foundItems [0]);
            }

        }

        public override void UpdateLayer ()
        {
            Layer.BackgroundColor = NSColor.FromRgb (245, 245, 245).CGColor;

            nfloat lastX = 0;
            foreach (var item in items) {
                var itemFrame = new CGRect (lastX, Layer.Frame.GetMidY (), item.Layer.Frame.Width, Layer.Frame.Height);
                item.Layer.Frame = itemFrame;
                lastX += itemFrame.Width;
            }
        }

        void DrawTriangle (nfloat x)
        {
            var point1 = new CGPoint (x, Bounds.Height);
            var point2 = new CGPoint (x + 10, Bounds.GetMidY ());
            var point3 = new CGPoint (x, 0);

            NSBezierPath.StrokeLine (point1, point2);
            NSBezierPath.StrokeLine (point2, point3);
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            NSColor.FromRgb (219, 219, 219).SetStroke ();

            var lastIndex = items.Count - 1;
            for (int i = 0; i < items.Count; i++) {
                if (i != lastIndex) {
                    var item = items [i];
                    DrawTriangle (item.Layer.Frame.X + item.Layer.Frame.Width);
                }
            }
        }

        void BuildConstraints ()
        {
            AddConstraints (FillVertical (stack, false));
            AddConstraints (FillHorizontal (stack, false));
        }
    }
}

