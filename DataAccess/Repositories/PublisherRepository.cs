using BusinessObject;
using DataAccess.DAOs;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        public Task<List<Publisher>> GetList() => PublisherDAO.Instance.GetList();
        public Task<Publisher> Get(int id) => PublisherDAO.Instance.Get(id);
        public Task Add(Publisher obj) => PublisherDAO.Instance.Add(obj);
        public Task Update(Publisher obj) => PublisherDAO.Instance.Update(obj);
        public Task Delete(int id) => PublisherDAO.Instance.Delete(id);
    }
}
