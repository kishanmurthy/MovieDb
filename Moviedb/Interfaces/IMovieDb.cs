using Moviedb.Models;

namespace Moviedb.Interfaces
{
    public interface IMovieDb
    {
        Movie GetMovie(int? id);

        Actor GetActor(int? id);

        Producer GetProducer(int? id);

        Actor[] GetActors();

        Actor[] GetActors(int[] ids);

        Movie[] GetMovies();

        Producer[] GetProducers();

        void AddMovie(Movie movie);

        void AddActor(Actor actor);

        void AddProducer(Producer producer);

        void UpdateActor(Actor actor);

        void UpdateMovie(Movie movie);

        void UpdateProducer(Producer producer);

        void RemoveMovie(Movie movie);

        void RemoveActor(Actor actor);

        void RemoveProducer(Producer producer);

    }

}