using System;
using CustomerFE.Services;
using Moq;
using Shared.Dto;
using Shared.Dto.Product;

namespace UnitTest
{
    public class MockProductService : Mock<IProductService>
    {
        public MockProductService MockById(ProductDto result, string id)
        {
            Setup(x => x.GetProductByIdAsync(id))
                .ReturnsAsync(result);
            return this;
        }
        public MockProductService VerifyGetById(Times times, string productId)
        {
            Verify(x => x.GetProductByIdAsync(productId), times);

            return this;
        }
    }
}
