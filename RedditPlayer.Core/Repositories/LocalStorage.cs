using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RedditPlayer.Repositories
{
    public static class LocalStorage
    {
        //public static async Task<Dictionary<string, SubReddit>> GetSubReddits ()
        //{
        //    var subReddits = await BlobCache.UserAccount.GetAllObjects<SubReddit> ();
        //    return subReddits.ToDictionary (x => x.Name);
        //}

        //public static async Task<string> SaveSubReddit (SubReddit subReddit)
        //{
        //    Contract.Requires (subReddit != null);
        //    string key = $"subreddit-{subReddit.Name}";
        //    await BlobCache.UserAccount.InsertObject (key, subReddit);

        //    return key;
        //}
    }
}

