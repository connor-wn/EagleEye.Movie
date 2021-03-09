using CsvHelper;
using EagleEye.Movie.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EagleEye.Movie.DataAccess
{
    public class AccessDb : IAccessDb
    {
        private string CsvFilePath { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public Database Database { get; set; }

        public AccessDb()
        {
            Database = new Database();
            using (var reader = new StreamReader($"{CsvFilePath}\\CsvFiles\\metadata.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var metaData = csv.GetRecords<Metadata>();
                Database.Metadata = metaData.ToList();
            }
            using (var reader = new StreamReader($"{CsvFilePath}\\CsvFiles\\stats.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var stats = csv.GetRecords<Stats>();
                Database.Stats = stats.ToList();
            }
        }

        public async Task PostMetadata(MetadataPostViewModel metaDataViewmodel)
        {
            var record = new Metadata
            {
                Id = Database.Metadata.Last().Id + 1,
                MovieId = metaDataViewmodel.MovieId,
                Title = metaDataViewmodel.Title,
                Language = metaDataViewmodel.Language,
                Duration = metaDataViewmodel.Duration,
                ReleaseYear = metaDataViewmodel.ReleaseYear
            };

            Database.Metadata.Add(record);
        }

        public async Task<List<Metadata>> GetMetadata(int movieId)
        {
            var metadata = Database.Metadata.Where(x => x.MovieId == movieId && !string.IsNullOrWhiteSpace(x.Title) && !string.IsNullOrWhiteSpace(x.Language) && x.ReleaseYear != null && x.Duration != null);

            var response = from element in metadata
                           group element by element.Language
                  into groups
                           select groups.OrderByDescending(p => p.Id).First();

            if (response == null || response.Count() == 0)
            {
                return null;
            }

            return response.OrderBy(x => x.Language).ToList();
        }

        public async Task<List<Statistic>> GetStats()
        {
            var movies = Database.Metadata
                .Where(x => !string.IsNullOrWhiteSpace(x.Title) && !string.IsNullOrWhiteSpace(x.Language) && x.ReleaseYear != null && x.Duration != null)
                .GroupBy(x => x.MovieId)
                .Select(x => x.First());

            var response = new List<Statistic>();
            foreach (var movie in movies)
            {
                var stats = Database.Stats.Where(x => x.MovieId == movie.MovieId);

                if (stats == null || stats.Count() == 0)
                {
                    continue;
                }

                long totalWatchTimeMs = stats.Sum(x => (long)x.WatchDurationMs);
                long totalWatchTimeS = totalWatchTimeMs != 0 ? totalWatchTimeMs / 1000 : 0;

                var stat = new Statistic
                {
                    MovieId = movie.MovieId,
                    Watches = stats.Count(),
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseYear
                };

                if (totalWatchTimeS != 0)
                {
                    var averageWatchTime = totalWatchTimeS / stat.Watches;
                    stat.AverageWatchDurationS = (int)averageWatchTime;
                }

                response.Add(stat);
            }

            return response.OrderByDescending(x => x.Watches).ThenByDescending(x => x.ReleaseYear).ToList();
        }
    }
}
