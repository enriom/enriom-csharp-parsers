﻿using Optivem.Atomiv.Infrastructure.EntityFrameworkCore;
using Optivem.Atomiv.Template.Core.Common.Orders;
using System.Collections.Generic;

namespace Optivem.Atomiv.Template.Infrastructure.Domain.Persistence.Records
{
    public class OrderStatusRecord : Record<OrderStatus>
    {
        public OrderStatusRecord()
        {
            OrderRecords = new HashSet<OrderRecord>();
        }

        public string Code { get; set; }

        public virtual ICollection<OrderRecord> OrderRecords { get; set; }
    }
}