using System;
namespace RedditPlayer.Mac.DataAdapters
{
    public interface ISelectableView
    {
        void DidSelectionChanged (bool isSelected);
    }
}

