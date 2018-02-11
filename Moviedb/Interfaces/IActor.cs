using Moviedb.Models;

namespace Moviedb.Interfaces
{
    internal interface IActor
    {
        Actor GetActor(int? id);

        Actor[] GetActors();

        Actor[] GetActors(int[] ids);

        void AddActor(Actor actor);

        void UpdateActor(Actor actor);

        void RemoveActor(Actor actor);
    }
}