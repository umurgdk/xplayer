using System;
using Foundation;
using System.Collections.Generic;
using RedditPlayer.Domain.Reddit;
using ReactiveUI;
using System.Diagnostics;
namespace RedditPlayer.Mac.Views
{
    public class RedditsSourceItem : NSObject
    {
        public IReactiveDerivedList<RedditsSourceItem> Children { get; private set; }
        public string Title { get; set; }

        public readonly bool IsCategory;

        public readonly SubReddit SubReddit;

        public readonly string Icon = "Playlist";

        public RedditsSourceItem (string title, IReactiveDerivedList<RedditsSourceItem> children)
        {
            Title = title;
            Children = children;
            IsCategory = true;

            children.Changed.Subscribe (_ => { });
        }

        public RedditsSourceItem (SubReddit reddit)
            : this (reddit, "Playlist")
        {
        }

        public RedditsSourceItem (SubReddit reddit, string iconName)
        {
            Title = reddit.Title;
            Children = new ReactiveList<RedditsSourceItem> ().CreateDerivedCollection (t => t);
            SubReddit = reddit;

            Icon = iconName;
            IsCategory = false;
        }
    }
}

