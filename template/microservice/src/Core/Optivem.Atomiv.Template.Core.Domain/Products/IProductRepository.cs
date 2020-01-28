﻿using Optivem.Atomiv.Core.Domain;
using System.Threading.Tasks;

namespace Optivem.Atomiv.Template.Core.Domain.Products
{
    public interface IProductRepository : IRepository
    {
        Task AddAsync(Product product);

        Task<Product> FindAsync(ProductIdentity productId);

        Task UpdateAsync(Product product);
    }
}