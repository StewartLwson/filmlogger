using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FilmLog.Models;

namespace FilmLog
{
    public class DataAccess
    {
        // File paths
        private static readonly string root = Environment.CurrentDirectory;

        private static readonly string profilePath = root + @"\UserProfile.txt";
        private static readonly string favouritesPath = root + @"\Favourites.txt";
        private static readonly string diaryPath = root + @"\Diary.txt";
        private static readonly string watchlistPath = root + @"\Watchlist.txt";

        private static string[] paths = new string[4] { profilePath, favouritesPath,
            diaryPath, watchlistPath };

        private static readonly int profileLength = 1;

        // Limit on amount of entrees added at one time
        private static readonly int favouritesEntreeSize = 4;
        private static readonly int diaryEntreeSize = 1;
        private static readonly int watchlistEntreeSize = 1;

        /// <summary>
        /// Creates files from the app's paths array (if not already created).
        /// </summary>
        public static void CreateFiles()
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
        /// Overwrites files from the app's paths array.
        /// </summary>
        public static void ClearFiles()
        {
            for (int i = 0; i < paths.Length; i++)
            {
                using (StreamWriter sw = File.CreateText(paths[i]))
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Updates a text file using input from the console.
        /// </summary>
        /// <param name="path">Path of the file to be updated.</param>
        /// <param name="entreesAmount">Amount of entrees to be entered.</param>
        public static void UpdateFile(string path, int entreesAmount)
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

        public static void UpdateProfile()
        {
            UpdateFile(profilePath, profileLength);
        }

        public static void UpdateFavourites()
        {
            UpdateFile(favouritesPath, favouritesEntreeSize);
        }

        public static void UpdateDiary()
        {
            UpdateFile(diaryPath, diaryEntreeSize);
        }

        public static void UpdateWatchlist()
        {
            UpdateFile(watchlistPath, watchlistEntreeSize);
        }

        /// <summary>
        /// Reads the profile file and returns the users profile.
        /// </summary>
        public static Profile ReadProfile()
        {
            Profile profile = new Profile();
            using (StreamReader sw = File.OpenText(profilePath))
            {
                string name = sw.ReadLine();
                profile.Name = name;
            }
            return profile;
        }

        /// <summary>
        /// Reads the favourites file and returns the users favourites.
        /// </summary>
        public static List<Favourite> ReadFavourites()
        {
            List<Favourite> favourites = new List<Favourite>();
            using (StreamReader sw = File.OpenText(favouritesPath))
            {
                while (!sw.EndOfStream)
                {
                    Favourite favourite = new Favourite
                    {
                        Name = sw.ReadLine()
                    };
                    favourites.Add(favourite);
                }
            }
            return favourites;
        }

        /// <summary>
        /// Reads the diary file and returns the users diary.
        /// </summary>
        public static List<DiaryEntree> ReadDiary()
        {
            List<DiaryEntree> diary = new List<DiaryEntree>();
            using (StreamReader sw = File.OpenText(diaryPath))
            {
                while (!sw.EndOfStream)
                {
                    DiaryEntree entree = new DiaryEntree
                    {
                        Name = sw.ReadLine()
                    };
                    diary.Insert(0, entree);
                }
            }
            return diary;
        }

        /// <summary>
        /// Reads the watchlist file and returns the users watchlist.
        /// </summary>
        public static List<WatchlistEntree> ReadWatchlist()
        {
            List<WatchlistEntree> watchlist = new List<WatchlistEntree>();
            using (StreamReader sw = File.OpenText(watchlistPath))
            {
                while(!sw.EndOfStream) { 
                    WatchlistEntree entree = new WatchlistEntree
                    {
                        Name = sw.ReadLine()
                    };
                    watchlist.Insert(0, entree);
                }
            }
            return watchlist;
        }

        
    }
}
