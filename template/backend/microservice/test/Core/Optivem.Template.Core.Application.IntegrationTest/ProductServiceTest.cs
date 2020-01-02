namespace Optivem.Template.Core.Application.IntegrationTest
{
    public class ProductServiceTest : Test
    {
        public ProductServiceTest(Fixture fixture)
            : base(fixture)
        {

        }

        // TODO: VC: Implement

        /*

        [Fact]
        public async Task RelistProduct_ValidRequest_ReturnsResponse()
        {
            // Arrange

            var createRequests = new List<CreateProductRequest>
            {
                new CreateProductRequest
                {
                    Code = "APP",
                    Description = "Apple",
                    UnitPrice = 10.50m,
                },

                new CreateProductRequest
                {
                    Code = "BAN",
                    Description = "Banana",
                    UnitPrice = 30.99m,
                },

                new CreateProductRequest
                {
                    Code = "ONG",
                    Description = "Orange",
                    UnitPrice = 35.99m,
                },

                new CreateProductRequest
                {
                    Code = "STR",
                    Description = "Strawberry",
                    UnitPrice = 40.00m,
                },
            };

            var createResponses = await CreateProductsAsync(createRequests);

            var someCreateResponse = createResponses[2];

            var id = someCreateResponse.Id;
            var unlistResponse = await Fixture.ProductService.UnlistProductAsync(id);

            unlistResponse.IsListed.Should().BeFalse();



            var record = _productRecords.Where(e => !e.IsListed).First();
            var id = record.Id;

            var activateResponse = await Fixture.ProductService.RelistProductAsync(id);

            AssertUtilities.NotEmpty(activateResponse.Id);
            Assert.Equal(record.Id, activateResponse.Id);
            Assert.Equal(record.ProductCode, activateResponse.Code);
            Assert.Equal(record.ProductName, activateResponse.Description);
            Assert.Equal(record.ListPrice, activateResponse.UnitPrice);
            Assert.True(activateResponse.IsListed);

            var findResponse = await Fixture.ProductService.FindProductAsync(id);

            Assert.Equal(activateResponse.Id, findResponse.Id);
            Assert.Equal(activateResponse.Code, findResponse.Code);
            Assert.Equal(activateResponse.Description, findResponse.Description);
            Assert.Equal(activateResponse.UnitPrice, findResponse.UnitPrice);
            Assert.Equal(activateResponse.IsListed, findResponse.IsListed);
        }

        [Fact]
        public async Task RelistProduct_InvalidRequest_ThrowsInvalidRequestException()
        {
            var record = _productRecords.Where(e => e.IsListed).First();
            var id = record.Id;

            await Assert.ThrowsAsync<DomainException>(() => Fixture.ProductService.RelistProductAsync(id));
        }

        [Fact]
        public async Task BrowseProducts_ValidRequest_ReturnsResponse()
        {
            for (int i = 0; i < 30; i++)
            {
                var productRecord = new ProductRecord
                {
                    Id = SequentialGuid.SequentialSqlGuidGenerator.Instance.NewGuid(),
                    ProductCode = $"P{i.ToString("0000")}",
                    ProductName = $"Product {i.ToString("0000")}",
                    ListPrice = 100 + i,
                };

                _productRecords.Add(productRecord);

                Fixture.Db.Add(productRecord);
            }

            var browseRequest = new BrowseProductsRequest
            {
                Page = 3,
                Size = 5,
            };

            var browseResponse = await Fixture.ProductService.BrowseProductsAsync(browseRequest);

            Assert.Equal(_productRecords.Count, browseResponse.TotalRecords);

            var skip = (browseRequest.Page - 1) * browseRequest.Size;
            var take = browseRequest.Size;

            var expected = _productRecords.Skip(skip).Take(take).ToList();

            for (int i = 0; i < expected.Count; i++)
            {
                var expectedRecord = expected[i];
                var actualRecord = browseResponse.Records[i];
                Assert.Equal(expectedRecord.Id, actualRecord.Id);
                Assert.Equal(expectedRecord.ProductCode, actualRecord.Code);
                Assert.Equal(expectedRecord.ProductName, actualRecord.Description);
                Assert.Equal(expectedRecord.ListPrice, actualRecord.UnitPrice);
                Assert.Equal(expectedRecord.IsListed, actualRecord.IsListed);
            }
        }

        [Fact]
        public async Task CreateProduct_ValidRequest_ReturnsResponse()
        {
            var createRequest = new CreateProductRequest
            {
                Code = "ABC",
                Description = "My desc",
                UnitPrice = 123.56m,
            };

            var createResponse = await Fixture.ProductService.CreateProductAsync(createRequest);

            AssertUtilities.NotEmpty(createResponse.Id);
            Assert.Equal(createRequest.Code, createResponse.Code);
            Assert.Equal(createRequest.Description, createResponse.Description);
            Assert.Equal(createRequest.UnitPrice, createResponse.UnitPrice);
            Assert.True(createResponse.IsListed);

            var id = createResponse.Id;
            var findResponse = await Fixture.ProductService.FindProductAsync(id);

            Assert.Equal(id, findResponse.Id);
            Assert.Equal(createRequest.Code, findResponse.Code);
            Assert.Equal(createRequest.Description, findResponse.Description);
            Assert.Equal(createRequest.UnitPrice, findResponse.UnitPrice);
            Assert.True(findResponse.IsListed);
        }

        [Fact]
        public async Task CreateProduct_InvalidRequest_ThrowsInvalidRequestException()
        {
            var createRequest = new CreateProductRequest
            {
                Code = null,
                Description = "Something",
                UnitPrice = 123,
            };

            await Assert.ThrowsAsync<InvalidRequestException>(() => Fixture.ProductService.CreateProductAsync(createRequest));
        }

        [Fact]
        public async Task UnlistProduct_ValidRequest_ReturnsResponse()
        {
            var record = _productRecords.Where(e => e.IsListed).First();
            var id = record.Id;

            var deactivateResponse = await Fixture.ProductService.UnlistProductAsync(id);

            AssertUtilities.NotEmpty(deactivateResponse.Id);
            Assert.Equal(record.Id, deactivateResponse.Id);
            Assert.Equal(record.ProductCode, deactivateResponse.Code);
            Assert.Equal(record.ProductName, deactivateResponse.Description);
            Assert.Equal(record.ListPrice, deactivateResponse.UnitPrice);
            Assert.False(deactivateResponse.IsListed);

            var findResponse = await Fixture.ProductService.FindProductAsync(id);

            Assert.Equal(deactivateResponse.Id, findResponse.Id);
            Assert.Equal(deactivateResponse.Code, findResponse.Code);
            Assert.Equal(deactivateResponse.Description, findResponse.Description);
            Assert.Equal(deactivateResponse.UnitPrice, findResponse.UnitPrice);
            Assert.Equal(deactivateResponse.IsListed, findResponse.IsListed);
        }

        [Fact]
        public async Task UnlistProduct_InvalidRequest_ThrowsInvalidRequestException()
        {
            var record = _productRecords.Where(e => !e.IsListed).First();
            var id = record.Id;

            await Assert.ThrowsAsync<DomainException>(() => Fixture.ProductService.UnlistProductAsync(id));
        }

        [Fact]
        public async Task FindProduct_ValidRequest_ReturnsCustomer()
        {
            var customerRecord = _productRecords[0];
            var id = customerRecord.Id;

            var findResponse = await Fixture.ProductService.FindProductAsync(id);

            Assert.Equal(customerRecord.Id, findResponse.Id);
            Assert.Equal(customerRecord.ProductCode, findResponse.Code);
            Assert.Equal(customerRecord.ProductName, findResponse.Description);
            Assert.Equal(customerRecord.ListPrice, findResponse.UnitPrice);
            Assert.Equal(customerRecord.IsListed, findResponse.IsListed);
        }

        [Fact]
        public async Task FindProduct_NotExistRequest_ThrowsNotFoundRequestException()
        {
            var id = Guid.NewGuid();

            await Assert.ThrowsAsync<NotFoundRequestException>(() => Fixture.ProductService.FindProductAsync(id));
        }

        [Fact]
        public async Task ListProducts_ValidRequest_ReturnsResponse()
        {
            var request = new ListProductRequest
            {
            };

            var response = await Fixture.ProductService.ListProductsAsync(request);

            Assert.Equal(_productRecords.Count, response.TotalRecords);

            for (int i = 0; i < _productRecords.Count; i++)
            {
                var expected = _productRecords[i];
                var actual = response.Records[i];

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal($"{expected.ProductCode} - {expected.ProductName}", actual.Name);
            }

            Assert.NotNull(response);
        }

        [Fact]
        public async Task UpdateProduct_ValidRequest_ReturnsResponse()
        {
            var productRecord = _productRecords[0];

            var updateRequest = new UpdateProductRequest
            {
                Id = productRecord.Id,
                Description = "Desc updated",
                UnitPrice = 300m,
            };

            var updateResponse = await Fixture.ProductService.UpdateProductAsync(updateRequest);

            Assert.Equal(updateRequest.Id, updateResponse.Id);
            Assert.Equal(productRecord.ProductCode, updateResponse.Code);
            Assert.Equal(updateRequest.Description, updateResponse.Description);
            Assert.Equal(updateRequest.UnitPrice, updateResponse.UnitPrice);
            Assert.Equal(productRecord.IsListed, updateResponse.IsListed);
        }

        [Fact]
        public async Task UpdateProduct_NotExistRequest_ThrowsNotFoundRequestException()
        {
            var id = Guid.NewGuid();

            var updateRequest = new UpdateProductRequest
            {
                Id = id,
                Description = "Desc updated",
                UnitPrice = 300m,
            };

            await Assert.ThrowsAsync<NotFoundRequestException>(() => Fixture.ProductService.UpdateProductAsync(updateRequest));
        }

        [Fact]
        public async Task UpdateProduct_InvalidRequest_ThrowsInvalidRequestException()
        {
            var record = _productRecords[0];

            var updateRequest = new UpdateProductRequest
            {
                Id = record.Id,
                Description = "Something",
                UnitPrice = -20m,
            };

            await Assert.ThrowsAsync<InvalidRequestException>(() => Fixture.ProductService.UpdateProductAsync(updateRequest));
        }

        */
    }
}