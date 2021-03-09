using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EagleEye.Movie.DataAccess
{
    public class Stats
    {
        [Name("movieId")]
        public int MovieId { get; set; }

        [Name("watchDurationMs")]
        public int WatchDurationMs { get; set; }
    }
}
