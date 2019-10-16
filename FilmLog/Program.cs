using System;
using System.IO;
using System.Collections.Generic;

namespace FilmLog
{
    class Program
    {
        // File paths
        static readonly string root = Environment.CurrentDirectory;
        
        static readonly string profilePath = root + @"\UserProfile.txt";
        static readonly string favouritesPath = root + @"\Favourites.txt";
        static readonly string diaryPath = root + @"\Diary.txt";
        static readonly string watchlistPath = root + @"\Watchlist.txt";

        static string[] paths = new string[4] { profilePath, favouritesPath, diaryPath, watchlistPath };

        static readonly int profileLength = 1;

        // Initial Favs Size
        static readonly int favsSize = 4;


        static string profile = "";
        static string[] favourites = null;
        static string[] diary = null;
        static string[] watchlist = null;

        static void Main(string[] args)
        {

            Console.WriteLine(root);

            CreateFiles();

            profile = ReadProfile();
            favourites = ReadFavourites();
            diary = ReadDiary();
            watchlist = ReadWatchlist();
            
            Console.WriteLine("Welcome to Film Log.");
            if (profile == "")
            {
                Console.WriteLine("Please enter your name.");
                UpdateProfile();
                profile = ReadProfile();
            }
            
            Console.WriteLine("Welcome " + profile);
            if (favourites.Length == 0)
            {
                Console.WriteLine("Please enter your favourite films.");
                UpdateFavourites();
                favourites = ReadFavourites();
            }

            Console.WriteLine("Your favourite films are: \n" + string.Join(", ", favourites));

            if (diary.Length != 0)
            {
                int sizeRecent = Math.Min(diary.Length, 5);
                string[] diaryRecent = new string[sizeRecent];
                Array.Copy(diary, diaryRecent, sizeRecent);
                Console.WriteLine("Recent adds to your film dairy: " + string.Join(", ", diaryRecent));
            } else
            {
                Console.WriteLine("Your film diary is empty.");
            }
            
            if (watchlist.Length != 0)
            {
                int sizeRecent = Math.Min(watchlist.Length, 5);
                string[] watchlistRecent = new string[sizeRecent];
                Array.Copy(watchlist, watchlistRecent, sizeRecent);
                Console.WriteLine("Recent adds to your watchlist: " + string.Join(", ", watchlist));
            } else
            {
                Console.WriteLine("Your watchlist is empty.");
            }
            

            bool appLoop = true;

            while(appLoop)
            {
                Console.WriteLine("What would you like to do? (enter the number next to the action): ");
                Console.WriteLine("1. View film diary.");
                Console.WriteLine("2. Add a film to your diary.");
                Console.WriteLine("3. View watchlist.");
                Console.WriteLine("4. Add a film to your watchlist.");
                Console.WriteLine("5. Exit app.");
                string mode = Console.ReadLine();
                if (mode == "1")
                {
                    Console.WriteLine(string.Join("\n", diary));
                }
                else if (mode == "2")
                {
                    Console.WriteLine("Please add a film to your diary.");
                    UpdateDiary();
                    diary = ReadDiary();
                    Console.WriteLine("Your latest watched films: " + string.Join(", ", diary));
                }
                else if (mode == "3")
                {
                    Console.WriteLine(string.Join("\n", watchlist));
                }
                else if (mode == "4")
                {
                    Console.WriteLine("Please add a film to your watchlist.");
                    UpdateWatchlist();
                    watchlist = ReadWatchlist();
                    Console.WriteLine("Films you want to watch: " + string.Join(", ", watchlist));
                }
                else if (mode == "5")
                {
                    appLoop = false;
                }
                else
                {
                    Console.WriteLine("Whoops! That option is not valid. Please try again...");
                }
            }  
        }

        static void CreateFiles()
        {
            for(int i = 0; i < paths.Length; i++)
            {
                if (!File.Exists(paths[i]))
                {
                    using (StreamWriter sw = File.CreateText(paths[i]))
                    {
                        sw.Close();
                    }
                }
            }
        }

        static string[] ReadFile(string path)
        {

            int entrees = GetSizeOfEntrees(path);
            string[] contents = new string[entrees];

            using (StreamReader sw = File.OpenText(path))
            {
                for (int i = 0; i < entrees; i++)
                {
                    contents[i] = sw.ReadLine();
                }
                sw.Close();
            }
            return contents;
        }

        static void UpdateFile(string path, int contentLength)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                for (int i = 0; i < contentLength; i++)
                {
                    sw.WriteLine(Console.ReadLine());
                }
                sw.Close();
            }
        }

        private static int GetSizeOfEntrees(string path)
        {
            int size = 0;
            using (StreamReader sr = File.OpenText(path))
            {
                while (sr.ReadLine() != null)
                {
                    size++;
                }
                sr.Close();
            }
            return size;
        }

        static string ReadProfile()
        {
            string[] profile = ReadFile(profilePath);

            string name = "";
            if (profile.Length > 0)
            {
                name = profile[0];
            }
            return name;
        }

        static void UpdateProfile()
        {
            UpdateFile(profilePath, profileLength);
        }

        static string[] ReadFavourites()
        {
            string[] favourites = ReadFile(favouritesPath);
            return favourites;
        }

        static void UpdateFavourites()
        {
            UpdateFile(favouritesPath, favsSize);
        }

        static string[] ReadList(string path)
        {
            string[] list = ReadFile(path);
            if (list.Length > 0)
            {
                Array.Reverse(list);
            }
            return list;
        }

        static string[] ReadDiary()
        {
            return ReadList(diaryPath);
        }

        static void UpdateDiary()
        {
            UpdateFile(diaryPath, 1);
        }

        static string[] ReadWatchlist()
        {
            return ReadList(watchlistPath);
        }

        static void UpdateWatchlist()
        {
            UpdateFile(watchlistPath, 1);
        }

    }
}
