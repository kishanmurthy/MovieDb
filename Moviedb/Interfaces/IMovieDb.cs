using System.Linq;
using Moviedb.Models;

namespace Moviedb.Interfaces
{
    public interface IMovieDb
    {


        Movie FindMovie(int? id);

        IQueryable<Actor> GetAllActors();

        IQueryable<Movie> GetAllMovies();

        IQueryable<Producer> GetAllProducers();

        void SaveChanges();
    }
}
