﻿using Optivem.Framework.Core.Application;

namespace Optivem.Template.Core.Application.Orders.Responses
{
    public class FindOrderResponse : IResponse<int>
    {
        public int Id { get; set; }
    }
}