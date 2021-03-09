using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EagleEye.Movie.DataAccess
{
    public class Metadata
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Duration { get; set; }

        public int? ReleaseYear { get; set; }
    }
}
