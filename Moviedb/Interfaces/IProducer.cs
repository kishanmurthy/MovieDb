using Moviedb.Models;
namespace Moviedb.Interfaces
{
    interface IProducer
    {
        Producer GetProducer(int? id);
        Producer[] GetProducers();
        void AddProducer(Producer producer);
        void UpdateProducer(Producer producer);
        void RemoveProducer(Producer producer);
    }
}
