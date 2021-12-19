using SQLite;

namespace MingitavSozlukMobile
{
    /// <summary>
    /// Default summary for Kelime.cs
    /// </summary>
    public class Kelime
    {
        /// <summary>
        /// The id of the word
        /// </summary>
        [PrimaryKey]
        public long KelimeID { get; set; }

        /// <summary>
        /// Karachay meaning of the word
        /// </summary>
        public string KaracaycaAnlam { get; set; }

        /// <summary>
        /// Turkish meaning of the word
        /// </summary>
        public string TurkceAnlam { get; set; }

        /// <summary>
        /// A flag that controls if the word has been searched before
        /// </summary>
        public long IsSearched { get; set; }

        /// <summary>
        /// Latest search time
        /// </summary>
        public string SearchTime { get; set; }

        /// <summary>
        /// A flag that controls if the word has been saved as favourite word.
        /// </summary>
        public long IsFav { get; set; }

        /// <summary>
        /// Favourite addition time
        /// </summary>
        public string FavTime { get; set; }

    }
}
