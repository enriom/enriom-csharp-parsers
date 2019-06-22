﻿using Optivem.Core.Application;

namespace Optivem.Template.Core.Application.Interface.Customers.Requests
{
    public class UpdateCustomerRequest : IRequest<int>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}