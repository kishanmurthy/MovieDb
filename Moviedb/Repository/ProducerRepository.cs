using System;
using System.Linq;
using Moviedb.Models;
using Moviedb.Interfaces;
using System.Data.Entity;

namespace Moviedb.Repository
{
    public class ProducerRepository : IProducer , IDisposable
    {
        private readonly MovieDbContext _movieDbContext;

        public ProducerRepository()
        {
            _movieDbContext = new MovieDbContext();
        }
        public ProducerRepository(MovieDbContext context)
        {
            _movieDbContext = context;
        }


        public Producer GetProducer(int? id)
        {
            return _movieDbContext.Producers.Find(id);
        }
        
        public Producer[] GetProducers()
        {
            return _movieDbContext.Producers.ToArray();
        }
        
        public void AddProducer(Producer producer)
        {
            _movieDbContext.Producers.Add(producer);
        }

        public void UpdateProducer(Producer producer)
        {
            _movieDbContext.Entry(producer).State = EntityState.Modified;
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
