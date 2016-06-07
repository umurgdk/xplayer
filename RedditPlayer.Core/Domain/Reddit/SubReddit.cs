using System;
using Newtonsoft.Json.Linq;

namespace RedditPlayer.Domain.Reddit
{
    public class SubReddit : IEquatable<SubReddit>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Grouping { get; set; } = "Featured";

        public string Key => Url;

        public SubReddit ()
        {

        }

        public SubReddit (SubReddit oldSubReddit)
        {
            Name = oldSubReddit.Name;
            Url = oldSubReddit.Url;
            Title = oldSubReddit.Title;
        }

        internal static SubReddit FromJson (JObject jObject)
        {
            var aboutObject = jObject ["data"];
            return new SubReddit {
                Name = aboutObject ["display_name"].Value<string> (),
                Url = aboutObject ["url"].Value<string> (),
                Title = aboutObject ["title"].Value<string> (),
                Description = aboutObject ["public_description"].Value<string> ()
            };
        }

        public override int GetHashCode ()
        {
            return (Name + Url + Title).GetHashCode ();
        }

        public bool Equals (SubReddit other)
        {
            return Name == other.Name && Url == other.Url && Title == other.Title;
        }
    }
}