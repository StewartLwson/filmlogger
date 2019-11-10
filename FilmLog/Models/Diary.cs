using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLog.Models
{
    public class Diary
    {
        public List<DiaryEntree> entrees = new List<DiaryEntree>();
        readonly private int RecentEntreesCount = 4;

        override public string ToString()
        {
            string output = "";
            if (entrees.Count > 0)
            {
                entrees.Sort((x, y) => y.Date.CompareTo(x.Date));
                DiaryEntree lastEntree = entrees.Last();
                foreach (DiaryEntree entree in entrees)
                {
                    output += entree.Title + " (Watched: " + entree.Date.ToShortDateString() + ")";
                    if (entree != lastEntree)
                    {
                        output += "\n";
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
                List<DiaryEntree> recentEntrees = entrees.GetRange(0, Math.Min(RecentEntreesCount, entrees.Count));
                DiaryEntree lastEntree = recentEntrees.Last();
                foreach (DiaryEntree entree in recentEntrees)
                {
                    output += entree.Title + " (Watched: " + entree.Date.ToShortDateString() + ")";
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
