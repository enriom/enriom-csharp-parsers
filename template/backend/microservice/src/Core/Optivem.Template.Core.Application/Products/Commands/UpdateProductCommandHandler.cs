﻿using Optivem.Framework.Core.Application;
using Optivem.Framework.Core.Application.Mapping;
using Optivem.Framework.Core.Domain;
using Optivem.Template.Core.Domain.Products;
using System.Threading.Tasks;

namespace Optivem.Template.Core.Application.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<UpdateProductCommandResponse> HandleAsync(UpdateProductCommand request)
        {
            var productId = new ProductIdentity(request.Id);

            var product = await _productRepository.FindAsync(productId);

            if (product == null)
            {
                throw new NotFoundRequestException();
            }

            Update(product, request);

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            var response = _mapper.Map<Product, UpdateProductCommandResponse>(product);
            return response;
        }

        private void Update(Product product, UpdateProductCommand request)
        {
            product.ProductName = request.Description;
            product.ListPrice = request.UnitPrice;
        }
    }
}