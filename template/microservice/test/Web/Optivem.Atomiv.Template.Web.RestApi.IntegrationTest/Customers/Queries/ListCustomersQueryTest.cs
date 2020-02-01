﻿using FluentAssertions;
using Optivem.Atomiv.Template.Core.Application.Customers.Commands;
using Optivem.Atomiv.Template.Core.Application.Customers.Queries;
using Optivem.Atomiv.Template.Web.RestApi.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Optivem.Atomiv.Template.Web.RestApi.IntegrationTest.Customers.Queries
{
    public class ListCustomersQueryTest : BaseTest
    {
        public ListCustomersQueryTest(Fixture fixture) : base(fixture)
        {
        }


        [Fact]
        public async Task ListCustomers_ValidRequest_ReturnsResponse()
        {
            // Arrange

            var createRequests = new List<CreateCustomerCommand>
            {
                new CreateCustomerCommand
                {
                    FirstName = "Mary",
                    LastName = "Smith",
                },

                new CreateCustomerCommand
                {
                    FirstName = "John",
                    LastName = "McDonald",
                },

                new CreateCustomerCommand
                {
                    FirstName = "Rob",
                    LastName = "McDonald",
                },

                new CreateCustomerCommand
                {
                    FirstName = "Markson",
                    LastName = "McDonald",
                },

                new CreateCustomerCommand
                {
                    FirstName = "Jake",
                    LastName = "McDonald",
                },

                new CreateCustomerCommand
                {
                    FirstName = "Mark",
                    LastName = "McPhil",
                },

                new CreateCustomerCommand
                {
                    FirstName = "Susan",
                    LastName = "McDonald",
                },
            };

            var createHttpResponses = await CreateCustomersAsync(createRequests);

            // Act

            var listRequest = new ListCustomersQuery
            {
                NameSearch = "ark",
                Limit = 10,
            };

            var listHttpResponse = await Fixture.Api.Customers.ListCustomersAsync(listRequest);

            // Assert

            listHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var expectedRecords = new List<CreateCustomerCommandResponse>
            {
                createHttpResponses[3].Data,
                createHttpResponses[5].Data,
            }
            .Select(e => new ListCustomersRecordResponse
            {
                Id = e.Id,
                Name = $"{e.FirstName} {e.LastName}",
            });

            var listResponse = listHttpResponse.Data;

            listResponse.TotalRecords.Should().Be(createRequests.Count);

            listResponse.Records.Should().BeEquivalentTo(expectedRecords);
        }
    }
}
