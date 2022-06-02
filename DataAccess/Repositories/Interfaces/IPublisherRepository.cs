using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
