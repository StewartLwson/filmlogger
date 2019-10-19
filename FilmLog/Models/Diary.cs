﻿using System;
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
                
                DiaryEntree lastEntree = entrees.Last();
                foreach (DiaryEntree entree in entrees)
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
                List<DiaryEntree> recentEntrees = entrees.GetRange(0, Math.Min(RecentEntreesCount, entrees.Count));
                DiaryEntree lastEntree = recentEntrees.Last();
                foreach (DiaryEntree entree in recentEntrees)
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
