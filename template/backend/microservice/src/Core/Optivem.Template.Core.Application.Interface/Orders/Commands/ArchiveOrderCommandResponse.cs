﻿using Optivem.Template.Core.Common.Orders;
using System;

namespace Optivem.Template.Core.Application.Orders.Commands
{
    public class ArchiveOrderCommandResponse
    {
        public Guid Id { get; set; }

        public OrderStatus Status { get; set; }
    }
}
