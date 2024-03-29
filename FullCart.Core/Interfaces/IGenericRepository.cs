﻿using FullCart.Core.Entities;
using FullCart.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsNoTrackingAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetEntityWithSpec(ISpecifications<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec);
        Task<int> CountAsync(ISpecifications<T> spec);
        void Add(T entity);
        Task AddOrUpdateBulk(IEnumerable<T> entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
