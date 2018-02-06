using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Moviedb.Interfaces;
using Moviedb.Models;

namespace Moviedb.Repository
{
    public class MovieRepository : IMovieDb ,IDisposable
    {

        private readonly MovieDbContext _movieDbContext;

        public MovieRepository()
        {
            _movieDbContext = new MovieDbContext();
        }

        public Movie FindMovie(int? id)
        {
            return _movieDbContext.Movies.Find(id);
        }

        public Actor FindActor(int? id)
        {
            return _movieDbContext.Actors.Find(id);
        }
        public Producer FindProducer(int? id)
        {
            return _movieDbContext.Producers.Find(id);
        }

        public IQueryable<Actor> GetAllActors()
        {
            return _movieDbContext.Actors;
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return _movieDbContext.Movies.Include("Producer").Include("Actors");
        }

        public IQueryable<Producer> GetAllProducers()
        {
            return _movieDbContext.Producers;
        }

        public void AddMovieToDb(Movie movie)
        {
            _movieDbContext.Movies.Add(movie);
        }

        public void AddActorToDb(Actor actor)
        {
            _movieDbContext.Actors.Add(actor);
        }
        public void AddProducerToDb(Producer producer)
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