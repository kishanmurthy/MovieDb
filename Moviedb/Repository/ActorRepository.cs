using Moviedb.Interfaces;
using Moviedb.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Moviedb.Repository
{
    public class ActorRepository : IActor , IDisposable
    {
        private readonly MovieDbContext _movieDbContext;

        public ActorRepository()
        {
            _movieDbContext = new MovieDbContext();
        }
        public ActorRepository(MovieDbContext context)
        {
            _movieDbContext = context;
        }


        public Actor GetActor(int? id)
        {
            return _movieDbContext.Actors.Find(id);
        }

        public Actor[] GetActors()
        {
            return _movieDbContext.Actors.ToArray();
        }

        public Actor[] GetActors(int[] ids)
        {
            var q = _movieDbContext.Actors.Where(e => ids.Contains(e.Id));
            return q.ToArray();
        }

        public void AddActor(Actor actor)
        {
            _movieDbContext.Actors.Add(actor);
        }

        public void UpdateActor(Actor actor)
        {
            _movieDbContext.Entry(actor).State = EntityState.Modified;
        }

        public void RemoveActor(Actor actor)
        {
            _movieDbContext.Actors.Remove(actor ?? throw new Exception());
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