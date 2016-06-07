using System;
using RedditPlayer.Services;
using System.Timers;
using AppKit;
namespace RedditPlayer.Mac
{
    public class SharedTimer : ITimer
    {
        Timer timer;

        public SharedTimer ()
        {
            timer = new Timer ();
            timer.Elapsed += OnElapsed;
        }

        public double Interval
        {
            get
            {
                return timer.Interval;
            }

            set
            {
                timer.Interval = value;
            }
        }

        public event EventHandler Elapsed;

        public void Start ()
        {
            timer.Start ();
        }

        public void Stop ()
        {
            timer.Stop ();
        }

        void OnElapsed (object sender, ElapsedEventArgs e)
        {
            NSApplication.SharedApplication.InvokeOnMainThread (() => {
                Elapsed?.Invoke (this, null);
            });
        }
    }
}

