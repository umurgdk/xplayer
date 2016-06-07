using System;
using RedditPlayer.Services;
using Foundation;
namespace RedditPlayer.Mac.Services
{
    public class Settings : ISettings
    {
        public float Volume
        {
            get
            {
                try {
                    return NSUserDefaults.StandardUserDefaults.FloatForKey ("Volume");
                } catch (Exception e) {
                    return 1;
                }
            }

            set
            {
                NSUserDefaults.StandardUserDefaults.SetFloat (value, "Volume");
            }
        }

        public bool FirstRun
        {
            get
            {
                if (NSUserDefaults.StandardUserDefaults.ValueForKey (new NSString ("FirstRun")) == null) {
                    return true;
                }

                return NSUserDefaults.StandardUserDefaults.BoolForKey ("FirstRun");
            }

            set
            {
                NSUserDefaults.StandardUserDefaults.SetBool (value, "FirstRun");
            }
        }

        public void RemoveAll ()
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject ("FirstRun");
            NSUserDefaults.StandardUserDefaults.RemoveObject ("Volume");
        }
    }
}

