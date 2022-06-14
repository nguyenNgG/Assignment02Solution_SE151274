using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        public Task<List<Publisher>> GetList();
        public Task<Publisher> Get(int id);
        public Task Add(Publisher obj);
        public Task Update(Publisher obj);
        public Task Delete(int id);
    }
}
