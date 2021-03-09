using System;
using System.Collections.Generic;
using System.Text;

namespace EagleEye.Movie.DataAccess
{
    public class Database
    {
        public List<Metadata> Metadata { get; set; }

        public List<Stats> Stats { get; set; }
    }
}
