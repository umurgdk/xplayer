using System;
using ReactiveUI;
using RedditPlayer.Domain.Media;
using ReactiveUI.Fody.Helpers;
using RedditPlayer.Services;
using Splat;
using System.Reactive.Linq;
using System.Diagnostics;
using System.Reactive;
namespace RedditPlayer.ViewModels
{
    public class PlayerViewModel : ReactiveObject
    {
        [Reactive]
        public Track CurrentTrack { get; protected set; }

        [Reactive]
        public float Progress { get; protected set; }

        [Reactive]
        public bool Playing { get; protected set; }

        [Reactive]
        public float Volume { get; set; }

        [Reactive]
        public bool Muted { get; protected set; }

        ITimer timer;

        public PlayerViewModel ()
        {
            timer = Locator.CurrentMutable.GetService<ITimer> ();
            timer.Interval = 1000;
            timer.Elapsed += OnTimerElapsed;

            Progress = 0.0f;

            var settings = Locator.CurrentMutable.GetService<ISettings> ();
            Volume = settings.Volume;

            var volumeObservable = this.WhenAnyValue (vm => vm.Volume)
                .Where (_ => CurrentTrack != null);

            var trackObservable = this.WhenAnyValue (vm => vm.CurrentTrack)
                .Where (track => track != null)
                .DistinctUntilChanged ();

            volumeObservable.CombineLatest (trackObservable, (volume, track) => volume)
                            .Subscribe (volume => SetPlayerVolume (volume));
        }

        void OnTimerElapsed (object sender, EventArgs args)
        {
            if (CurrentTrack == null) {
                timer.Stop ();
                return;
            }

            var elapsed = CurrentTrack.Provider.Player.GetElapsedTime ();
            var elapsedSeconds = (float)elapsed.TotalSeconds;
            var totalSeconds = (float)CurrentTrack.Duration.TotalSeconds;

            Progress = elapsedSeconds / totalSeconds;
        }

        public void Play (Track track)
        {
            timer.Start ();
            track.Provider.Player.Play (track);

            if (track != CurrentTrack) {
                CurrentTrack = track;
                Progress = 0;
            }

            Playing = true;
        }

        public void Pause ()
        {
            CurrentTrack?.Provider.Player.Pause ();
            Playing = false;
        }

        public void ToggleMute ()
        {
            Muted = !Muted;

            if (Muted) {
                CurrentTrack?.Provider.Player.Mute ();
            } else {
                CurrentTrack?.Provider.Player.Unmute ();
            }
        }

        public void Seek (float progress)
        {
            Progress = progress;
            CurrentTrack?.Provider.Player.Seek (progress);
        }

        void SetPlayerVolume (float volume)
        {
            CurrentTrack.Provider.Player.SetVolume (volume);
        }
    }
}

