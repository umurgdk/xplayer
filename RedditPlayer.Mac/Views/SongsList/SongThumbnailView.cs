﻿using System;
using Foundation;
using AppKit;
using RedditPlayer.Mac.Extensions;
using CoreGraphics;
using CoreAnimation;
using QuartzComposer;
using System.Threading.Tasks;

namespace RedditPlayer.Mac.Views.SongsList
{
    [Register ("SongThumbnailView")]
    public class SongThumbnailView : NSView
    {
        CALayer imageLayer;
        CAShapeLayer maskLayer;

        NSShadow shadow;

        float cornerRadius;
        public float CornerRadius
        {
            get
            {
                return cornerRadius;
            }

            set
            {
                cornerRadius = value;
                if (Layer != null) {
                    Layer.CornerRadius = cornerRadius;
                }
            }
        }

        bool hasShadow;
        public bool HasShadow
        {
            get
            {
                return hasShadow;
            }

            set
            {
                hasShadow = value;
                if (value) {
                    Shadow = shadow;
                } else {
                    Shadow = null;
                }
            }
        }

        NSImage image;
        public NSImage Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                imageLayer?.RemoveFromSuperLayer ();
                imageLayer = null;
                NeedsDisplay = true;
            }
        }

        public override CGRect Frame
        {
            get
            {
                return base.Frame;
            }
            set
            {
                base.Frame = value;
                NeedsDisplay = true;
            }
        }

        float overlayAlpha = 0;
        public float OverlayAlpha
        {
            get
            {
                return overlayAlpha;
            }

            set
            {
                overlayAlpha = value;
                NeedsDisplay = true;
            }
        }

        NSColor overlayColor = NSColor.Black;
        public NSColor OverlayColor
        {
            get
            {
                return overlayColor;
            }

            set
            {
                overlayColor = value;
                NeedsDisplay = true;
            }
        }

        CALayer overlayLayer;

        public float BorderWidth
        {
            get
            {
                return (float)Layer.BorderWidth;
            }

            set
            {
                Layer.BorderWidth = value;
                NeedsDisplay = true;
            }
        }

        public CGColor BorderColor
        {
            get
            {
                return Layer.BorderColor;
            }

            set
            {
                Layer.BorderColor = value;
                NeedsDisplay = true;
            }
        }

        public override void SetFrameSize (CGSize newSize)
        {
            base.SetFrameSize (newSize);
            NeedsDisplay = true;
        }

        public void SetImageAsync (string url)
        {
            if (url == null) {
                Image = null;
                return;
            }

            var task = Task.Run (() => {
                var _image = new NSImage (NSUrl.FromString (url));

                NSApplication.SharedApplication.InvokeOnMainThread (() => {
                    Image = _image;
                    CATransaction.Flush ();
                });
            });
        }

        public override bool WantsUpdateLayer => true;

        public SongThumbnailView (IntPtr handle) : base (handle)
        {
            Initialize ();
            cornerRadius = 0;
        }

        public SongThumbnailView ()
        {
            Initialize ();
            cornerRadius = 3;
        }

        public SongThumbnailView (float cornerRadius)
        {
            this.cornerRadius = cornerRadius;
            Initialize ();
        }

        public SongThumbnailView (float cornerRadius, NSImage image)
        {
            this.image = image;
            this.cornerRadius = cornerRadius;
            Initialize ();
        }

        void Initialize ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            WantsLayer = true;

            shadow = new NSShadow ();

            shadow.ShadowOffset = new CGSize (0, -2);
            shadow.ShadowBlurRadius = 3;
            shadow.ShadowColor = NSColor.FromWhite (0, 0.55f);
        }

        public override void UpdateLayer ()
        {
            base.UpdateLayer ();

            if (image != null && imageLayer == null) {
                Layer.MasksToBounds = false;

                if (maskLayer == null) {
                    maskLayer = CAShapeLayer.Create () as CAShapeLayer;
                    Layer.AddSublayer (maskLayer);
                    maskLayer.Path = CGPath.FromRoundedRect (Bounds, cornerRadius, cornerRadius);
                }

                imageLayer = CALayer.Create ();
                imageLayer.Mask = maskLayer;
                imageLayer.MasksToBounds = true;

                image.LockFocus ();
                imageLayer.Contents = image.CGImage;
                imageLayer.ContentsGravity = CALayer.GravityResizeAspectFill;
                image.UnlockFocus ();

                Layer.AddSublayer (imageLayer);
            }

            if (image == null) {
                imageLayer?.RemoveFromSuperLayer ();
            }

            if (overlayAlpha > 0 && overlayLayer == null) {
                overlayLayer = new CALayer ();
                overlayLayer.BackgroundColor = overlayColor.ColorWithAlphaComponent (overlayAlpha).CGColor;
                Layer.AddSublayer (overlayLayer);
            }

            if (overlayAlpha == 0 && overlayLayer != null) {
                overlayLayer.RemoveFromSuperLayer ();
                overlayLayer = null;
            }

            CATransaction.Begin ();
            CATransaction.DisableActions = true;

            if (imageLayer != null) {
                if (imageLayer.Bounds.Width != Bounds.Width) {
                    imageLayer.Frame = Bounds;
                    maskLayer.Path = CGPath.FromRoundedRect (Bounds, cornerRadius, cornerRadius);
                }
            }

            if (overlayLayer != null && overlayLayer.SuperLayer != null && overlayLayer.Frame != Bounds) {
                overlayLayer.Frame = Bounds;
            }

            CATransaction.Commit ();
        }
    }
}

