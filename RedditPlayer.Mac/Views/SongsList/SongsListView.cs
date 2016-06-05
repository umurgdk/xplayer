using AppKit;
using Foundation;
using ReactiveUI;
using RedditPlayer.Domain.MediaProviders;
using RedditPlayer.Domain.Reddit;
using RedditPlayer.Mac.DataAdapters;
using static RedditPlayer.Mac.Extensions.NSLayoutExtensions;

namespace RedditPlayer.Mac.Views.SongsList
{
    public class SongsListView : NSView
    {
        NSScrollView scrollView;
        NSClipView clipView;
        GenericTableViewSource<RedditMedia> source;

        public readonly NSTableView TableView;

        public SongsListView ()
        {
            TranslatesAutoresizingMaskIntoConstraints = false;

            scrollView = new NSScrollView ();
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;

            clipView = new NSClipView ();

            TableView = new NSTableView ();
            TableView.HeaderView = null;
            TableView.AddColumn (new NSTableColumn ("SongColumn"));
            TableView.RowHeight = 54;

            var providers = new MediaProvider ();

            var songs = new ReactiveList<RedditMedia> {
                new RedditMedia { Title = "Wish You Were Here", Provider = providers.FromUrl ("soundcloud"), ThumbnailUrl = "http://img2-ak.lst.fm/i/u/174s/394cfbc6b2a74766a4364778c641ca51.jpg"},
                new RedditMedia { Title = "High Hopes", Provider = providers.FromUrl ("soundcloud"), ThumbnailUrl = "http://img2-ak.lst.fm/i/u/174s/322025b870b64e8cced4e68728238022.jpg" }
            };

            source = new GenericTableViewSource<RedditMedia> (
                TableView,
                songs,
                (media, column) => "SongView",
                (media, identifier) => new SongView (),
                (media, identifier, view) => ConfigureSongView (media, view)
            );

            source.RowViewCreator = row => new SongRowView ();

            TableView.Source = source;
            //TableView.Delegate = source;
            TableView.ReloadData ();

            scrollView.AddSubview (clipView);
            scrollView.DocumentView = TableView;

            AddSubview (scrollView);

            BuildConstraints ();
        }

        void ConfigureSongView (RedditMedia media, NSView view)
        {
            var songView = view as SongView;
            songView.Thumbnail.Image = new NSImage (NSUrl.FromString (media.ThumbnailUrl));
            songView.SongTitle.StringValue = media.Title;
            songView.Identifier = "SongView";
        }

        void BuildConstraints ()
        {
            AddConstraints (FillVertical (scrollView, false));
            AddConstraints (FillHorizontal (scrollView, false));
        }
    }
}

