﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetList();
        public Task<Book> Get(int id);
        public Task Add(Book obj);
        public Task Update(Book obj);
        public Task Delete(int id);
    }
}
