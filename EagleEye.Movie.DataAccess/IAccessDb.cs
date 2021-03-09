using EagleEye.Movie.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye.Movie.DataAccess
{
    public interface IAccessDb
    {
        Task PostMetadata(MetadataPostViewModel metaDataViewmodel);

        Task<List<Metadata>> GetMetadata(int movieId);

        Task<List<Statistic>> GetStats();
    }
}
