using System;
using RedditPlayer.Services;
namespace RedditPlayer.Coordinators
{
    public class WelcomeCoordinator
    {
        INavigator navigator;

        public WelcomeCoordinator (INavigator navigator)
        {
            this.navigator = navigator;

        }


    }
}

