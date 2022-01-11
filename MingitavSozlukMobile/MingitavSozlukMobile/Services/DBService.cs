using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MingitavSozlukMobile.Services
{
    /// <summary>
    /// A SQLite Database service for dictionary.
    /// </summary>
    public static class DBService
    {
        /// <summary>
        /// Sqlite Database Instance
        /// </summary>
        static SQLiteAsyncConnection db;

        /// <summary>
        /// Starts database connection if it is not started yet.
        /// </summary>
        private static void InitDB()
        {
            // Check if database instance is null
            if (db != null)
                return;

            // Get the database path
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sozluk.db");

            // Make arangements for database connection
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("MingitavSozlukMobile.sozluk.db");

            // Check file existence. If exist, do not copy the newer version...
            if (!File.Exists(databasePath))
            {
                FileStream fileStreamToWrite = File.Create(databasePath);
                embeddedDatabaseStream.Seek(0, SeekOrigin.Begin);
                embeddedDatabaseStream.CopyTo(fileStreamToWrite);
                fileStreamToWrite.Close();
            }

            // Start database
            db = new SQLiteAsyncConnection(databasePath);

        }

        /// <summary>
        /// Loads all the words in the database.
        /// </summary>
        public static async Task<List<Kelime>> LoadAllWords()
        {
            InitDB();

            return await db.Table<Kelime>().ToListAsync();
        }

        /// <summary>
        /// Loads words from database with given string value
        /// </summary>
        /// <param name="s">Given string value</param>
        /// <returns>List of <see cref="Kelime"/></returns>
        public static async Task<List<Kelime>> LoadWordsLike(string str)
        {
            InitDB();

            return await db.Table<Kelime>().Where(s => s.KaracaycaAnlam.StartsWith(str)).ToListAsync();

        }

        /// <summary>
        /// Gets only one word by the help of given id.
        /// </summary>
        /// <param name="id">Given id</param>
        /// <returns></returns>
        public static async Task<Kelime> GetOneWord(long id)
        {
            InitDB();

            return await db.Table<Kelime>().Where(s => s.KelimeID.Equals(id)).FirstOrDefaultAsync();

        }

        /// <summary>
        /// Gets all examples by the help of given word id.
        /// </summary>
        /// <param name="id">Word Id</param>
        /// <returns></returns>
        public static async Task<List<Ornek>> LoadExamples(long id)
        {
            InitDB();

            return await db.Table<Ornek>().Where(s => s.OrnekKelimeID.Equals(id)).ToListAsync();

        }

        /// <summary>
        /// Updates word for history and favourites options
        /// </summary>
        /// <param name="kelime">The word that will be updated</param>
        /// <returns></returns>
        public static async Task<int> UpdateWord(Kelime kelime)
        {
            InitDB();

            return await db.UpdateAsync(kelime); 
        }

        /// <summary>
        /// Loads history words.
        /// </summary>
        public static async Task<List<Kelime>> LoadHistoryWords()
        {
            InitDB();

            return await db.Table<Kelime>().Where(s => s.IsSearched.Equals(1)).OrderByDescending(i => i.SearchTime).Take(50).ToListAsync();
        }

        /// <summary>
        /// Loads favourite words.
        /// </summary>
        public static async Task<List<Kelime>> LoadFavouriteWords()
        {
            InitDB();

            return await db.Table<Kelime>().Where(s => s.IsFav.Equals(1)).OrderByDescending(i => i.SearchTime).ToListAsync();
        }

        /// <summary>
        /// Gets number of words
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfWords()
        {
            InitDB();

            return db.Table<Kelime>().CountAsync().Result;
        }

    }
}
