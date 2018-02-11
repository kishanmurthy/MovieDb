using System;
using System.Data.Entity;
using System.Linq;
using Moviedb.Interfaces;
using Moviedb.Models;
using RefactorThis.GraphDiff;

namespace Moviedb.Repository
{
    public class MovieRepository : IMovie,IDisposable
    {

        private readonly MovieDbContext _movieDbContext;

        public MovieRepository()
        {
            _movieDbContext = new MovieDbContext();
        }

        public MovieRepository(MovieDbContext context)
        {
            _movieDbContext = context;
        }


        public Movie GetMovie(int? id)
        {
            return _movieDbContext.Movies.Find(id);
        }

        
        public Movie[] GetMovies()
        {
            return _movieDbContext.Movies.Include(m => m.Producer).Include(m => m.Actors).ToArray();
        }

        
        public void AddMovie(Movie movie)
        {
            _movieDbContext.Movies.Attach(movie);
            _movieDbContext.Movies.Add(movie);
        }

        
        public void UpdateMovie(Movie movie)
        {
            //_movieDbContext.Movies.Attach(movie);
            _movieDbContext.Entry(movie).State = EntityState.Modified;
            _movieDbContext.UpdateGraph(movie, map => map.AssociatedCollection(p => p.Actors));
            _movieDbContext.Configuration.AutoDetectChangesEnabled = true;

        }


        
        public void RemoveMovie(Movie movie)
        {
            _movieDbContext.Movies.Remove(movie ?? throw new Exception());
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