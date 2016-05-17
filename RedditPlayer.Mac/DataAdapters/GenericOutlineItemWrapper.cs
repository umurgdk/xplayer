using System;
using ReactiveUI;
using Foundation;
using System.Collections.Generic;
namespace RedditPlayer.Mac.DataAdapters
{
    public class GenericOutlineItemWrapper : NSObject
    {
        public readonly string Icon;
        public readonly string Text;
        public readonly bool IsCategory;
        public readonly IList<GenericOutlineItemWrapper> Children;

        public GenericOutlineItemWrapper (string icon, string title)
            : this (icon, title, new List<GenericOutlineItemWrapper> (), false)
        {
        }

        public GenericOutlineItemWrapper (string icon, string title, IList<GenericOutlineItemWrapper> children, bool isCategory)
        {
            Icon = icon ?? "Playlist";
            Text = title ?? "No Title";
            Children = children;
            IsCategory = isCategory;
        }
    }
}

