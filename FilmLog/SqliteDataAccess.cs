using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FilmLog.Models;

namespace FilmLog
{
    public class SqliteDataAccess
    {
        public static User LoadProfile(User user)
        {
            string sql = "SELECT * FROM User WHERE Name=@Name";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>(sql, new { user.Name });
                if (output.Count() == 0)
                {
                    return null;
                }
                return output.First();
            }
        }

        public static Favourites LoadFavourites(User user)
        {
            string sql = "SELECT Title FROM Favourites WHERE Username=@Username";
            Favourites favourites = new Favourites();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Favourite>(sql, new { Username = user.Name });
                if (output.Count() == 0)
                {
                    return null;
                }
                favourites.entrees = output.ToList();
                return favourites;
            }
        }

        public static Diary LoadDiary(User user)
        {
            string sql = "SELECT * FROM Diary WHERE Username=@Username";
            Diary diary = new Diary();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DiaryEntree>(sql, new { Username = user.Name });
                if (output.Count() == 0)
                {
                    return null;
                }
                diary.entrees = output.ToList();
                return diary;
            }
        }

        public static Watchlist LoadWatchlist(User user)
        {
            string sql = "SELECT * FROM Watchlist WHERE Username=@Username";
            Watchlist watchlist = new Watchlist();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<WatchlistEntree>(sql, new { Username = user.Name });
                if (output.Count() == 0)
                {
                    return null;
                }
                watchlist.entrees = output.ToList();
                return watchlist;
            }
        }

        public static void SaveProfile(User profile)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "INSERT INTO User (Name) VALUES (@Name)";
                var output = cnn.Execute(sql, profile);
            }
        }

        public static void SaveFavourites(User profile, List<Favourite> favourites)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "INSERT INTO Favourites (Username, Title) VALUES ('" + profile.Name + "', @Title)";
                foreach (Favourite favourite in favourites)
                {
                    cnn.Execute(sql, favourite);
                }
            }
        }

        public static void SaveDiary(User profile, List<DiaryEntree> diaryEntrees)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "INSERT INTO Diary (Username, Title, Date) VALUES ('" + profile.Name + "', @Title, @Date)";
                foreach (DiaryEntree entree in diaryEntrees)
                {
                    cnn.Execute(sql, entree);
                }
            }
        }

        public static void SaveWatchlist(User profile, List<WatchlistEntree> watchlistEntrees)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string sql = "INSERT INTO Watchlist (Username, Title, Date" +
                    ") VALUES ('" + profile.Name + "', @Title, @Date)";
                foreach (WatchlistEntree entree in watchlistEntrees)
                {
                    cnn.Execute(sql, entree);
                }
            }
        }

        public static void ClearFavourites(User profile)
        {
            string sql = "DELETE FROM Favourites WHERE Username=@Name";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, profile);
            }
        }

        public static void ClearDiary(User profile)
        {
            string sql = "DELETE FROM Diary WHERE Username=@Name";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, profile);
            }
        }

        public static void ClearWatchlist(User profile)
        {
            string sql = "DELETE FROM Watchlist WHERE Username=@Name";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, profile);
            }
        }

        public static void ClearProfile(User profile)
        {
            string sql = "DELETE FROM User WHERE Name=@Name";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sql, profile);
                ClearFavourites(profile);
                ClearDiary(profile);
                ClearWatchlist(profile);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
