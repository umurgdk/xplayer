using System;
namespace RedditPlayer.Services
{
    public interface ITimer
    {
		double Interval { get; set; }

		event EventHandler Elapsed;

        void Start ();
        void Stop ();
    }
}

