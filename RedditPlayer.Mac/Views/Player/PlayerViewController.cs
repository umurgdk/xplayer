using System;
using AppKit;
using System.Reactive.Linq;
using RedditPlayer.ViewModels;
using ReactiveUI;
using RedditPlayer.Domain.Media;
using Foundation;

namespace RedditPlayer.Mac.Views.Player
{
    public class PlayerViewController : NSViewController
    {
        public new PlayerView View
        {
            get
            {
                return (PlayerView)base.View;
            }

            set
            {
                base.View = value;
            }
        }

        public PlayerViewController (PlayerViewModel viewModel)
        {
            View = new PlayerView ();

            viewModel.WhenAnyValue (vm => vm.CurrentTrack)
                     .Where (track => track != null)
                     .Subscribe (track => UpdateTrackInformation (track));

            viewModel.WhenAnyValue (vm => vm.CurrentTrack)
                     .Select (track => track != null)
                     .BindTo (View.PlayerControls.PlayButton, button => button.Enabled);

            viewModel.WhenAnyValue (vm => vm.Playing)
                     .Subscribe (playing => {
                         if (playing) {
                             View.PlayerControls.ShowPauseButton ();
                         } else {
                             View.PlayerControls.ShowPlayButton ();
                         }
                     });

            var mutedObservable = viewModel.WhenAnyValue (vm => vm.Muted);

            viewModel.WhenAnyValue (vm => vm.Volume)
                     .CombineLatest (mutedObservable, (volume, muted) => muted ? 0.0f : volume)
                     .BindTo (View.SoundControl.VolumeSlider, slider => slider.FloatValue);

            mutedObservable.BindTo (View.SoundControl, sc => sc.Muted);

            View.PlayerControls.PlayButton.Activated += (sender, e) => viewModel.Play (viewModel.CurrentTrack);
            View.PlayerControls.PauseButton.Activated += (sender, e) => viewModel.Pause ();
            View.SoundControl.VolumeSlider.Activated += (sender, e) => viewModel.Volume = (sender as NSSlider).FloatValue;
            View.SoundControl.MuteButton.Activated += (sender, e) => viewModel.ToggleMute ();

            var progressObservable = View.Progress
                .WhenAnyValue (p => p.Progress);

            progressObservable
                .Where (_ => !View.Progress.IsDragging)
                .BindTo (viewModel, vm => vm.Progress);

            progressObservable
                .Where (_ => View.Progress.IsDragging)
                .Throttle (TimeSpan.FromMilliseconds (100), RxApp.MainThreadScheduler)
                .Subscribe (progress => viewModel.Seek (progress));

            var draggingObservable = View.Progress.WhenAnyValue (p => p.IsDragging);

            var updateProgress = true;
            viewModel.WhenAnyValue (vm => vm.Progress)
                     .Where (_ => !View.Progress.IsDragging)
                     .DistinctUntilChanged ()
                     .Subscribe (progress => View.Progress.Progress = progress);

            View.Progress
                .WhenAnyValue (vm => vm.IsDragging)
                .Subscribe (dragging => updateProgress = !dragging);
        }

        void UpdateTrackInformation (Track track)
        {
            View.SongTitle.StringValue = track.Title;
            View.CoverImage.Image = new NSImage (NSUrl.FromString (track.CoverUrl));
        }
    }
}

