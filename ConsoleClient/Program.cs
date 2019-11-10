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

        // Application loop
        public static bool appLoop = true;

        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Film Log.");
            Login();
            Console.WriteLine("Welcome " + user.Name);
            if (favourites == null)
            {
                CreateFavourites();
            }

            Console.WriteLine("Your favourite films are: " + favourites.ToString());

            RecentDiary();
            RecentWatchlist();

            while (appLoop)
            {
                Console.WriteLine("What would you like to do? (enter the " +
                    "number next to the action): ");
                Console.WriteLine("1. View film diary.");
                Console.WriteLine("2. Add a film to your diary.");
                Console.WriteLine("3. View watchlist.");
                Console.WriteLine("4. Add a film to your watchlist.");
                Console.WriteLine("5. Update favourite films.");
                Console.WriteLine("6. Reset user data.");
                Console.WriteLine("7. Exit app.");

                string mode = Console.ReadLine();

                switch(mode)
                {
                    case "1":
                        Diary();
                        break;
                    case "2":
                        AddDiaryEntree();
                        RecentDiary();
                        break;
                    case "3":
                        Watchlist();
                        break;
                    case "4":
                        AddWatchlistEntree();
                        RecentWatchlist();
                        break;
                    case "5":
                        UpdateFavourites();
                        break;
                    case "6":
                        ClearUserData();
                        break;
                    case "7":
                        appLoop = false;
                        break;
                    default:
                        Console.WriteLine("Whoops! That option is not valid. " +
                        "Please try again...");
                        break;
                }
            }
        }

        static void Login()
        {
            Console.WriteLine("Please enter your name.");
            User newUser = new User
            {
                Name = Console.ReadLine()
            };
            user = SqliteDataAccess.LoadProfile(newUser);
            if (user == null)
            {
                Register(newUser);
                favourites = null;
                diary = null;
                watchlist = null;
            } else
            {
                favourites = SqliteDataAccess.LoadFavourites(newUser);
                diary = SqliteDataAccess.LoadDiary(newUser);
                watchlist = SqliteDataAccess.LoadWatchlist(newUser);
            }
        }

        static void Register(User newUser)
        {
            SqliteDataAccess.SaveProfile(newUser);
            user = SqliteDataAccess.LoadProfile(newUser);
        }

        private static void ClearUserData()
        {
            Console.WriteLine("This will clears and resets all user " +
                        "data (profile, favourites, diary, watchlist). Are " +
                        "you sure you want to do this? (Y/N)");
            string choice = Console.ReadLine().ToUpper();
            if (choice == "Y")
            {
                SqliteDataAccess.ClearProfile(user);
                appLoop = false;
            }
            else
            {
                Console.WriteLine("You have chosen not to clear " +
                    "your data");
            }
        }

        static void Diary()
        {
            if (diary != null)
            {
                Console.WriteLine(string.Join("\n", diary));
            }
            else
            {
                Console.WriteLine("Your film diary is empty.");
            }
        }

        private static void Watchlist()
        {
            if (watchlist != null)
            {
                Console.WriteLine(string.Join("\n", watchlist));
            }
            else
            {
                Console.WriteLine("Your watchlist is empty.");
            }
        }

        static void RecentDiary()
        {
            if (diary != null)
            {
                Console.WriteLine("Recent adds to your diary: " +
                    diary.RecentEntreesToString());
            }
            else
            {
                Console.WriteLine("Your film diary is empty.");
            }
        }

        static void RecentWatchlist()
        {
            if (watchlist != null)
            {
                Console.WriteLine("Recent adds to your watchlist: " +
                    watchlist.RecentEntreesToString());
            }
            else
            {
                Console.WriteLine("Your watchlist is empty.");
            }
        }

        static void CreateFavourites()
        {
            Console.WriteLine("Please enter your favourite films.");
            Favourites fs = new Favourites();
            for (int i = 0; i < 4; i++)
            {
                Favourite f = new Favourite
                {
                    Title = Console.ReadLine()
                };
                fs.entrees.Add(f);
            }
            SqliteDataAccess.SaveFavourites(user, fs.entrees);
            favourites = new Favourites();
            favourites = SqliteDataAccess.LoadFavourites(user);
        }

        static void UpdateFavourites()
        {
            SqliteDataAccess.ClearFavourites(user);
            CreateFavourites();
        }

        private static void AddDiaryEntree()
        {
            Console.WriteLine("Please add a film to your diary.");
            DiaryEntree diaryEntree = new DiaryEntree
            {
                Title = Console.ReadLine()
            };
            Console.WriteLine("When did you watch this film? (dd/MM/yyyy)");
            diaryEntree.Date = DateTime.Parse(Console.ReadLine());
            List<DiaryEntree> entrees = new List<DiaryEntree>
                    {
                        diaryEntree
                    };
            SqliteDataAccess.SaveDiary(user, entrees);
            diary = SqliteDataAccess.LoadDiary(user);
        }

        private static void AddWatchlistEntree()
        {
            Console.WriteLine("Please add a film to your watchlist.");
            WatchlistEntree watchlistEntree = new WatchlistEntree
            {
                Title = Console.ReadLine(),
                Date = DateTime.Now
            };
            List<WatchlistEntree> entrees = new List<WatchlistEntree>
                    {
                        watchlistEntree
                    };
            SqliteDataAccess.SaveWatchlist(user, entrees);
            watchlist = SqliteDataAccess.LoadWatchlist(user);
        }
    }
}
