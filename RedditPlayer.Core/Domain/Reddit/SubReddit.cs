using System;
using System.Net.Http;
using System.Collections.Immutable;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RedditPlayer.Domain.Reddit
{
    public class SubReddit : IEquatable<SubReddit>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Grouping { get; set; } = "Featured";

        public ImmutableDictionary<string, RedditMedia> Medias { get; private set; } = new Dictionary<string, RedditMedia> { }.ToImmutableDictionary ();

        public string Key => Url;

        public SubReddit ()
        {

        }

        public SubReddit (SubReddit oldSubReddit, ImmutableDictionary<string, RedditMedia> newMedias)
        {
            Name = oldSubReddit.Name;
            Url = oldSubReddit.Url;
            Title = oldSubReddit.Title;
            Medias = newMedias;
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

        public SubReddit AddDistinctMedias (IEnumerable<RedditMedia> newMedias)
        {
            var finalMedias = Medias;
            foreach (var item in newMedias) {
                if (finalMedias.ContainsKey (item.Key)) {
                    finalMedias = finalMedias.SetItem (item.Key, item);
                } else {
                    finalMedias = finalMedias.Add (item.Key, item);
                }
            }

            return new SubReddit (this, finalMedias);
        }

        public SubReddit SetMedias (ImmutableDictionary<string, RedditMedia> newMedias)
        {
            return new SubReddit (this, newMedias);
        }

        public SubReddit UpsertMedia (RedditMedia newMedia)
        {
            if (Medias.ContainsValue (newMedia)) {
                return SetMedias (Medias.SetItem (newMedia.Key, newMedia));
            }

            return SetMedias (Medias.Add (newMedia.Key, newMedia));
        }

        public override int GetHashCode ()
        {
            return (Name + Url + Title).GetHashCode () + Medias.GetHashCode ();
        }

        public bool Equals (SubReddit other)
        {
            return Name == other.Name && Url == other.Url && Title == other.Title && Medias == other.Medias;
        }
    }
}