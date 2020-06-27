﻿using Atomiv.Infrastructure.MongoDb;
using Atomiv.Template.Core.Common.Orders;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Atomiv.Template.Infrastructure.Domain.Persistence.MongoDb.Records
{
    public class OrderRecord : Record<ObjectId>
    {
        public ObjectId CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatusId { get; set; }

        public List<OrderItemRecord> OrderItems { get; set; }
    }
}
