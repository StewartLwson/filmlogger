using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLog.Models
{
    public class Watchlist
    {
        public List<WatchlistEntree> entrees = new List<WatchlistEntree>();
        readonly private int RecentEntreesCount = 4;

        override public string ToString()
        {
            string output = "";
            if (entrees.Count > 0)
            {
                entrees.Sort((x, y) => y.Date.CompareTo(x.Date));
                WatchlistEntree lastEntree = entrees.Last();
                foreach (WatchlistEntree entree in entrees)
                {
                    output += entree.Title;
                    if (entree != lastEntree)
                    {
                        output += ", ";
                    }
                }
            }
            return output;
        }

        public string RecentEntreesToString()
        {
            string output = "";
            if (entrees.Count > 0)
            {
                entrees.Sort((x, y) => y.Date.CompareTo(x.Date));
                List<WatchlistEntree> recentEntrees = entrees.GetRange(0, Math.Min(RecentEntreesCount, entrees.Count));
                WatchlistEntree lastEntree = recentEntrees.Last();
                foreach (WatchlistEntree entree in recentEntrees)
                {
                    output += entree.Title;
                    if (entree != lastEntree)
                    {
                        output += ", ";
                    }
                }
            }
            return output;
        }
    }
}
