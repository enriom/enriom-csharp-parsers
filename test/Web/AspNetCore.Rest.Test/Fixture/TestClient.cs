﻿using Optivem.Common.Http;
using Optivem.Infrastructure.Http.System;
using Optivem.Framework.Test.Web.AspNetCore.Rest.Fake;
using Optivem.Test.Xunit.AspNetCore;
using Optivem.Web.AspNetCore.Fake.Dtos.Customers;
using Optivem.Web.AspNetCore.Fake.Models;
using System.Collections.Generic;
using Optivem.Web.AspNetCore.Rest.Fake.Dtos.Customers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Optivem.Web.AspNetCore.Test
{
    public class TestClient : BaseTestValidatedJsonClient<Startup>
    {
        public TestClient()
        {
            Values = new ValuesControllerClient(ControllerClientFactory);
            Exceptions = new ExceptionsControllerClient(ControllerClientFactory);
            Customers = new CustomersControllerClient(ControllerClientFactory);
        }
        public ValuesControllerClient Values { get; }

        public ExceptionsControllerClient Exceptions { get; }

        public CustomersControllerClient Customers { get; }

        protected override void Setup(IConfigurationRoot configuration)
        {
            // NOTE: No startup actions
        }

        protected override string GetConfigurationJsonFile()
        {
            return null;
        }
    }

    public class ValuesControllerClient : BaseControllerClient
    {
        public ValuesControllerClient(IControllerClientFactory clientFactory) 
            : base(clientFactory, "api/values")
        {
        }

        public Task<List<string>> GetAllAsync()
        {
            return Client.GetAsync<List<string>>();
        }
    }

    public class ExceptionsControllerClient : BaseControllerClient
    {
        public ExceptionsControllerClient(IControllerClientFactory clientFactory) 
            : base(clientFactory, "api/exceptions")
        {
        }

        public Task GetAsync(int id)
        {
            return Client.GetByIdAsync(id);
        }
    }

    public class CustomersControllerClient : BaseControllerClient
    {
        public CustomersControllerClient(IControllerClientFactory clientFactory) 
            : base(clientFactory, "api/customers")
        {
        }

        public Task<CustomerGetAllResponse> GetAllAsync()
        {
            return Client.GetAsync<CustomerGetAllResponse>();
        }

        public Task<string> GetCsvExportsAsync()
        {
            // TODO: VC: Returning raw...
            return Client.GetAsync("exports", "text/csv");
        }

        public Task<string> PostImportsAsync(string content)
        {
            return Client.PostAsync("imports", content, "text/csv", "application/json");
        }

        public Task<CustomerPostResponse> PostAsync(CustomerPostRequest request)
        {
            return Client.PostAsync<CustomerPostRequest, CustomerPostResponse>(request);
        }
    }
}