using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerBE.Controllers;
using Shared.Dto.Product;
using Xunit;

namespace UnitTest
{
    public class ProductControllerTest
	{
		[Fact]
		public void ProductController_GetById()
		{
			//Arrange
			var mockProduct = new ProductDto()
			{
                Id = "62dc9d2f-5ae4-4435-b1c3-766967f3889b",
				Name = "Nike 2",
				Price = 3400000,
				Brand = 2,
				Gender = 0,
				Size = 40,
				Rating = 0,
				ImagePath = "//images//",
			};
			var mockProductServie = new MockProductService().MockById(mockProduct, "62dc9d2f-5ae4-4435-b1c3-766967f3889b");
			var controller = new ProductsController(mockProductServie.Object);

			//Act
			var result = controller.GetProduct("62dc9d2f-5ae4-4435-b1c3-766967f3889b");

			//Assert
			Assert.IsAssignableFrom<IActionResult>(result);
			mockProductServie.VerifyGetById(Times.Once(), "62dc9d2f-5ae4-4435-b1c3-766967f3889b");
		}
	}
}

//Id: "62dc9d2f-5ae4-4435-b1c3-766967f3889b",
//				Name: "Nike 2",
//				Price: 3400000,
//				Brand: 2,
//				Gender: 0,
//				Size: 40,
//				Rating: 0,
//				ImagePath: "//images//",
