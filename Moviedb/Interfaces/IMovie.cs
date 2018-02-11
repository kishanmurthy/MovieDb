using Moviedb.Models;

namespace Moviedb.Interfaces
{
    public interface IMovie
    {
        Movie GetMovie(int? id);

        Movie[] GetMovies();

        void AddMovie(Movie movie);

        void UpdateMovie(Movie movie);

        void RemoveMovie(Movie movie);
    }
}