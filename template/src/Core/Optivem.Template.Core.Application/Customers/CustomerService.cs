﻿using Optivem.Core.Application;
using Optivem.Core.Application.Services;
using Optivem.Template.Core.Application.Interface.Customers;
using Optivem.Template.Core.Application.Interface.Customers.Requests;
using Optivem.Template.Core.Application.Interface.Customers.Responses;
using System.Threading.Tasks;

namespace Optivem.Template.Core.Application.Customers
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IRequestHandler requestHandler) : base(requestHandler)
        {
        }

        public Task<ListCustomersResponse> ListCustomersAsync()
        {
            return ListAsync<ListCustomersRequest, ListCustomersResponse>();
        }

        public Task<FindCustomerResponse> FindCustomerAsync(int id)
        {
            return FindAsync<int, FindCustomerRequest, FindCustomerResponse>(id);
        }

        public Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            return CreateAsync<CreateCustomerRequest, CreateCustomerResponse>(request);
        }

        public Task<UpdateCustomerResponse> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            return UpdateAsync<UpdateCustomerRequest, UpdateCustomerResponse>(request);
        }

        public Task DeleteCustomerAsync(int id)
        {
            return DeleteAsync<int, DeleteCustomerRequest, DeleteCustomerResponse>(id);
        }


    }
}