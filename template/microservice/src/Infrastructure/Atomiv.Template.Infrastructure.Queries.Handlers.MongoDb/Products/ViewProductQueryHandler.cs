﻿using Atomiv.Core.Application;
using Atomiv.Template.Core.Application.Queries.Products;
using Atomiv.Template.Infrastructure.Domain.Persistence.MongoDb;
using Atomiv.Template.Infrastructure.Domain.Persistence.MongoDb.Records;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Atomiv.Template.Infrastructure.Queries.Handlers.MongoDb.Products
{
    public class ViewProductQueryHandler : QueryHandler<ViewProductQuery, ViewProductQueryResponse>
    {
        public ViewProductQueryHandler(MongoDbContext context) : base(context)
        {
        }

        public override async Task<ViewProductQueryResponse> HandleAsync(ViewProductQuery request)
        {
            var productId = request.Id.TryToObjectId();

            if(productId == null)
            {
                return null;
            }

            var filter = Builders<ProductRecord>.Filter
                .Eq(e => e.Id, productId);

            var productRecordCursor = await Context.Products
                .FindAsync(filter);

            var productRecord = await productRecordCursor
                .FirstOrDefaultAsync();

            if (productRecord == null)
            {
                throw new ExistenceException();
            }

            return GetResponse(productRecord);
        }

        private static ViewProductQueryResponse GetResponse(ProductRecord productRecord)
        {
            return new ViewProductQueryResponse
            {
                Id = productRecord.Id.ToString(),
                Code = productRecord.ProductCode,
                Description = productRecord.ProductName,
                UnitPrice = productRecord.ListPrice,
                IsListed = productRecord.IsListed,
            };
        }
    }
}
