using System;
using ReactiveUI;
using RedditPlayer.ViewModels;

namespace RedditPlayer.Services
{
    public interface INavigator
    {
        void PresentWelcomeScreen();
        void PresentSearchResults();
        void PresentPlaylist ();

        void ShowWindow (object sender);
    }
}

