using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Moviedb.Interfaces;
using Moviedb.Models;

namespace Moviedb.Repository
{
    public class MovieRepository : IDisposable
    {

        private readonly MovieDbContext _movieDbContext;

        public MovieRepository()
        {
            _movieDbContext = new MovieDbContext();
        }

        public Movie GetMovie(int? id)
        {
            return _movieDbContext.Movies.Find(id);
        }

        public Actor GetActor(int? id)
        {
            return _movieDbContext.Actors.Find(id);
        }
        public Producer GetProducer(int? id)
        {
            return _movieDbContext.Producers.Find(id);
        }

        public IEnumerable<Actor> GetActors()
        {
            return _movieDbContext.Actors;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movieDbContext.Movies.Include(m => m.Producer).Include(m => m.Actors).ToList();
        }

        public IEnumerable<Producer> GetProducers()
        {
            return _movieDbContext.Producers.ToList();
        }

        public void AddMovie(Movie movie)
        {
            _movieDbContext.Movies.Add(movie);
        }

        public void AddActor(Actor actor)
        {
            _movieDbContext.Actors.Add(actor);
        }
        public void AddProducer(Producer producer)
        {
            _movieDbContext.Producers.Add(producer);
        }


        public void UpdateActor(Actor actor)
        {
            _movieDbContext.Entry(actor).State = EntityState.Modified;
        }

        public void UpdateProducer(Producer producer)
        {
            _movieDbContext.Entry(producer).State = EntityState.Modified;
        }
        public void RemoveMovie(Movie movie)
        {
            _movieDbContext.Movies.Remove(movie ?? throw new Exception());
        }

        public void RemoveActor(Actor actor)
        {
            _movieDbContext.Actors.Remove(actor ?? throw new Exception());
        }
        public void RemoveProducer(Producer producer)
        {
            _movieDbContext.Producers.Remove(producer ?? throw new Exception());
        }
        public void SaveChanges()
        {
            _movieDbContext.SaveChanges();
        }



        public void Dispose()
        {
            _movieDbContext.Dispose();
            GC.SuppressFinalize(this);

        }
    }
}