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

        // Limit on amount of entrees added at one time
        static readonly int favouritesEntreeSize = 4;
        static readonly int diaryEntreeSize = 1;
        static readonly int watchlistEntreeSize = 1;

        // Limit on length of "recently added" lists
        static readonly int recentDiarySize = 5;
        static readonly int recentWatchlistSize = 5;

        // App data
        static string profile = "";
        static string[] favourites = null;
        static string[] diary = null;
        static string[] watchlist = null;

        static void Main(string[] args)
        {

            Console.WriteLine(root);

            CreateFiles();

            profile = ReadProfile();
            favourites = ReadFile(favouritesPath);
            diary = ReadList(diaryPath);
            watchlist = ReadList(watchlistPath);
            
            Console.WriteLine("Welcome to Film Log.");
            if (profile == "")
            {
                Console.WriteLine("Please enter your name.");
                UpdateFile(profilePath, profileLength);
                profile = ReadProfile();
            }
            
            Console.WriteLine("Welcome " + profile);
            if (favourites.Length == 0)
            {
                Console.WriteLine("Please enter your favourite films.");
                UpdateFile(favouritesPath, favouritesEntreeSize);
                favourites = ReadFile(favouritesPath);
            }

            Console.WriteLine("Your favourite films are: \n" + string.Join(", ", favourites));

            if (diary.Length != 0)
            {
                string[] diaryRecent = ReadRecentFromDiary();
                Console.WriteLine("Recent adds to your film dairy: " + string.Join(", ", diaryRecent));
            } else
            {
                Console.WriteLine("Your film diary is empty.");
            }
            
            if (watchlist.Length != 0)
            {
                string[] diaryRecent = ReadRecentFromWatchlist();
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
                    UpdateFile(diaryPath, diaryEntreeSize);
                    diary = ReadList(diaryPath);
                    string[] diaryRecent = ReadRecentFromDiary();
                    Console.WriteLine("Recent adds to your film dairy: " + string.Join(", ", diaryRecent));
                }
                else if (mode == "3")
                {
                    Console.WriteLine(string.Join("\n", watchlist));
                }
                else if (mode == "4")
                {
                    Console.WriteLine("Please add a film to your watchlist.");
                    UpdateFile(watchlistPath, watchlistEntreeSize);
                    watchlist = ReadList(watchlistPath);
                    string[] diaryRecent = ReadRecentFromWatchlist();
                    Console.WriteLine("Recent adds to your watchlist: " + string.Join(", ", watchlist));
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

        /// <summary>
        /// Reads a text file and returns contents in string array.
        /// </summary>
        /// <param name="path">Path of the file to be read.</param>
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

        /// <summary>
        /// Updates a text file using input from the console.
        /// </summary>
        /// <param name="path">Path of the file to be updated.</param>
        /// <param name="entreesAmount">Amount of entrees to be entered.</param>
        static void UpdateFile(string path, int entreesAmount)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                for (int i = 0; i < entreesAmount; i++)
                {
                    sw.WriteLine(Console.ReadLine());
                }
                sw.Close();
            }
        }

        /// <summary>
        /// Reads a text file and returns the amount of lines.
        /// </summary>
        /// <param name="path">Path of the file to be read.</param>
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

        /// <summary>
        /// Reads the profile file and returns the user's name.
        /// </summary>
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

        /// <summary>
        /// Reads a file and returns it as an array in reversed order.
        /// </summary>
        /// <param name="path">Path of file to be read</param>
        static string[] ReadList(string path)
        {
            string[] list = ReadFile(path);
            if (list.Length > 0)
            {
                Array.Reverse(list);
            }
            return list;
        }

        /// <summary>
        /// Returns a certain amount of entrees from a list.
        /// </summary>
        /// <param name="path">Path of list.</param>
        /// <param name="amount">Amount of entrees</param>
        static string[] ReadAmountFromList(string path, int amount)
        {
            string[] list = ReadList(path);
            string[] subList = new string[amount];
            Array.Copy(list, subList, amount);
            return subList;
        }

        /// <summary>
        /// Returns a certain amount of entrees from diary.
        /// </summary>
        static string[] ReadRecentFromDiary()
        {
            int sizeRecent = Math.Min(diary.Length, recentWatchlistSize);
            string[] diaryRecent = ReadAmountFromList(diaryPath, sizeRecent);
            return diaryRecent;
        }

        /// <summary>
        /// Returns a certain amount of entrees from watchlist.
        /// </summary>
        static string[] ReadRecentFromWatchlist()
        {
            int sizeRecent = Math.Min(watchlist.Length, recentDiarySize);
            string[] watchlistRecent = ReadAmountFromList(watchlistPath, sizeRecent);
            return watchlistRecent;
        }
    }
}
