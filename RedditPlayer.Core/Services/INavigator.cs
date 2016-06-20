using System;
using ReactiveUI;
using RedditPlayer.ViewModels;
using RedditPlayer.Domain.Media;

namespace RedditPlayer.Services
{
    public interface INavigator
    {
        void PresentWelcomeScreen();
        void PresentSearchResults();
        void PresentPlaylist();
        void PresentArtist(ArtistDetailViewModel artist);

        void ShowWindow(object sender);
    }
}

