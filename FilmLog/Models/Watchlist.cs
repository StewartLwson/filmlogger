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
                WatchlistEntree lastEntree = entrees.Last();
                foreach (WatchlistEntree entree in entrees)
                {
                    output += entree.Name;
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
                List<WatchlistEntree> recentEntrees = entrees.GetRange(0, Math.Min(RecentEntreesCount, entrees.Count));
                WatchlistEntree lastEntree = recentEntrees.Last();
                foreach (WatchlistEntree entree in recentEntrees)
                {
                    output += entree.Name;
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
