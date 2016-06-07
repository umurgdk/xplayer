using System;
using CoreAnimation;
using Foundation;
namespace RedditPlayer.Mac.Extensions
{
    public static class CAAnimationCollection
    {
        public static CABasicAnimation Scale (float from, float to, double duration)
        {
            var animation = new CABasicAnimation ();
            animation.KeyPath = "transform.scale";
            animation.From = new NSNumber (from);
            animation.To = new NSNumber (to);
            animation.Duration = duration;
            animation.FillMode = CAFillMode.Forwards;
            animation.RemovedOnCompletion = false;

            return animation;
        }

        public static CABasicAnimation Scale (float to, double duration)
        {
            var animation = new CABasicAnimation ();
            animation.KeyPath = "transform.scale";
            animation.To = new NSNumber (to);
            animation.Duration = duration;
            animation.FillMode = CAFillMode.Forwards;
            animation.RemovedOnCompletion = false;

            return animation;
        }

        public static CABasicAnimation ScaleUp (double duration)
        {
            return Scale (0, 1, duration);
        }

        public static CABasicAnimation ScaleDown (double duration)
        {
            return Scale (1, 0, duration);
        }
    }
}

