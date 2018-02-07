using System.Linq;
using Moviedb.Models;

namespace Moviedb.Interfaces
{
    public interface IMovieDb
    {


        Movie FindMovie(int? id);
        void SaveChanges();
    }
}
