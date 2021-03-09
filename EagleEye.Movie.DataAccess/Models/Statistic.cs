namespace EagleEye.Movie.DataAccess
{
    public class Statistic
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int? AverageWatchDurationS { get; set; }

        public int? Watches { get; set; }

        public int? ReleaseYear { get; set; }
    }
}
