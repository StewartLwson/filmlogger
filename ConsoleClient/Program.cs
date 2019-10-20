using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLog;
using FilmLog.Models;

namespace ConsoleClient
{
    class Program
    {
        // App data
        public static Profile profile = new Profile();
        public static Favourites favourites = new Favourites();
        public static Diary diary = new Diary();
        public static Watchlist watchlist = new Watchlist();

        static void Main(string[] args)
        {
            DataAccess.CreateFiles();

            profile = DataAccess.ReadProfile();
            favourites.entrees = DataAccess.ReadFavourites();
            diary.entrees = DataAccess.ReadDiary();
            watchlist.entrees = DataAccess.ReadWatchlist();

            Console.WriteLine("Welcome to Film Log.");
            if (profile.Name == null)
            {
                Console.WriteLine("Please enter your name.");
                DataAccess.UpdateProfile();
                profile = DataAccess.ReadProfile();
            }

            Console.WriteLine("Welcome " + profile.Name);
            if (favourites.entrees.Count == 0)
            {
                Console.WriteLine("Please enter your favourite films.");
                DataAccess.UpdateFavourites();
                favourites.entrees = DataAccess.ReadFavourites();
            }

            Console.WriteLine("Your favourite films are: " + favourites.ToString());

            if (diary.entrees.Count > 0)
            {
                Console.WriteLine("Recent adds to your diary: " +
                    diary.RecentEntreesToString());
            }
            else
            {
                Console.WriteLine("Your film diary is empty.");
            }

            if (watchlist.entrees.Count > 0)
            {
                Console.WriteLine("Recent adds to your watchlist: " +
                    watchlist.RecentEntreesToString());
            }
            else
            {
                Console.WriteLine("Your watchlist is empty.");
            }


            bool appLoop = true;

            while (appLoop)
            {
                Console.WriteLine("What would you like to do? (enter the " +
                    "number next to the action): ");
                Console.WriteLine("1. View film diary.");
                Console.WriteLine("2. Add a film to your diary.");
                Console.WriteLine("3. View watchlist.");
                Console.WriteLine("4. Add a film to your watchlist.");
                Console.WriteLine("5. Reset user data.");
                Console.WriteLine("6. Exit app.");
                string mode = Console.ReadLine();
                if (mode == "1")
                {
                    if (diary.entrees.Count > 0)
                    {
                        Console.WriteLine(string.Join("\n", diary));
                    } else
                    {
                        Console.WriteLine("Your film diary is empty.");
                    }
                    
                }
                else if (mode == "2")
                {
                    Console.WriteLine("Please add a film to your diary.");
                    DataAccess.UpdateDiary();
                    diary.entrees = DataAccess.ReadDiary();
                    Console.WriteLine("Recent adds to your diary: " +
                    diary.RecentEntreesToString());
                }
                else if (mode == "3")
                {
                    if (watchlist.entrees.Count > 0)
                    {
                        Console.WriteLine(string.Join("\n", watchlist));
                    } else
                    {
                        Console.WriteLine("Your watchlist is empty.");
                    }
                    
                }
                else if (mode == "4")
                {
                    Console.WriteLine("Please add a film to your watchlist.");
                    DataAccess.UpdateWatchlist();
                    watchlist.entrees = DataAccess.ReadWatchlist();
                    Console.WriteLine("Recent adds to your watchlist: " +
                        watchlist.RecentEntreesToString());
                }
                else if (mode == "5")
                {
                    Console.WriteLine("This will clears and resets all user " +
                        "data (profile, favourites, diary, watchlist). Are " +
                        "you sure you want to do this? (Y/N)");
                    string choice = Console.ReadLine().ToUpper();
                    if (choice == "Y")
                    {
                        DataAccess.ClearFiles();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have chosen not to clear " +
                            "your data");
                    }
                }
                else if (mode == "6")
                {
                    appLoop = false;
                }
                else
                {
                    Console.WriteLine("Whoops! That option is not valid. " +
                        "Please try again...");
                }
            }
        }
    }
}
