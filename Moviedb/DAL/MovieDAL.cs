using System;
using Moviedb.Models;
using Moviedb.Repository;
namespace Moviedb.DAL
{
    public class MovieDal : IDisposable
    {
        private readonly MovieDbContext _movieDbContext;

        public MovieDal()
        {
            _movieDbContext = new MovieDbContext();
        }

        private MovieRepository _movieRepository;
        private ActorRepository _actorRepository;
        private ProducerRepository _producerRepository;


        public MovieRepository MovieRepository
        {
            get
            {
                _movieRepository = _movieRepository == null ? new MovieRepository(_movieDbContext) : _movieRepository; 
                return _movieRepository;
            }

        }



        public ActorRepository ActorRepository
        {
            get
            {
                _actorRepository = _actorRepository ?? new ActorRepository(_movieDbContext);
                return _actorRepository;
            }

        }

        public ProducerRepository ProducerRepository
        {
            get
            {
                _producerRepository = _producerRepository ?? new ProducerRepository(_movieDbContext);
                return _producerRepository;
            }

        }


        public void Save()
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