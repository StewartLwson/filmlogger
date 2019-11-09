using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLog.Models
{
    public class Favourites
    {
        public List<Favourite> entrees = new List<Favourite>();
        override public string ToString()
        {
            string output = "";
            if (entrees.Count > 0)
            {
                Favourite lastFavourite = entrees.Last();
                foreach (Favourite favourite in entrees)
                {
                    output += favourite.Title;
                    if (favourite != lastFavourite)
                    {
                        output += ", ";
                    }
                }
            }
            return output;
        }
    }
}
