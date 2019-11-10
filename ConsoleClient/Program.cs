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
        public static User user = new User();
        public static Favourites favourites = new Favourites();
        public static Diary diary = new Diary();
        public static Watchlist watchlist = new Watchlist();

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name.");
            User newUser = new User();
            newUser.Name = Console.ReadLine();

            user = SqliteDataAccess.LoadProfile(newUser);
            favourites = SqliteDataAccess.LoadFavourites(newUser);
            diary = SqliteDataAccess.LoadDiary(newUser);
            watchlist = SqliteDataAccess.LoadWatchlist(newUser);

            Console.WriteLine("Welcome to Film Log.");
            if (user == null)
            {
                SqliteDataAccess.SaveProfile(newUser);
                user = SqliteDataAccess.LoadProfile(newUser);
            }

            Console.WriteLine("Welcome " + user.Name);
            if (favourites == null)
            {
                Console.WriteLine("Please enter your favourite films.");
                Favourites fs = new Favourites();
                for(int i = 0; i < 4; i++)
                {
                    Favourite f = new Favourite();
                    f.Title = Console.ReadLine();
                    fs.entrees.Add(f);
                }
                SqliteDataAccess.SaveFavourites(user, fs.entrees);
                favourites = new Favourites();
                favourites = SqliteDataAccess.LoadFavourites(user);
            }

            Console.WriteLine("Your favourite films are: " + favourites.ToString());

            if (diary != null)
            {
                Console.WriteLine("Recent adds to your diary: " +
                    diary.RecentEntreesToString());
            }
            else
            {
                Console.WriteLine("Your film diary is empty.");
            }

            if (watchlist != null)
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
                    if (diary != null)
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
                    DiaryEntree diaryEntree = new DiaryEntree();
                    diaryEntree.Title = Console.ReadLine();
                    Console.WriteLine("When did you watch this film? (dd/MM/yyyy)");
                    diaryEntree.Date = DateTime.Parse(Console.ReadLine());
                    List<DiaryEntree> entrees = new List<DiaryEntree>();
                    entrees.Add(diaryEntree);
                    SqliteDataAccess.SaveDiary(user, entrees);
                    diary = SqliteDataAccess.LoadDiary(user);
                    Console.WriteLine("Recent adds to your diary: " +
                    diary.RecentEntreesToString());
                }
                else if (mode == "3")
                {
                    if (watchlist != null)
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
                    WatchlistEntree watchlistEntree = new WatchlistEntree();
                    watchlistEntree.Title = Console.ReadLine();
                    watchlistEntree.Date = DateTime.Now;
                    List<WatchlistEntree> entrees = new List<WatchlistEntree>();
                    entrees.Add(watchlistEntree);
                    SqliteDataAccess.SaveWatchlist(user, entrees);
                    watchlist = SqliteDataAccess.LoadWatchlist(user);
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
                        SqliteDataAccess.ClearProfile(user);
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
