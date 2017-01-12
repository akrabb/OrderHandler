using System.Collections.Generic;
using NSubstitute;
using OrderHandler.Entities;
using OrderHandler.Helpers;
using Shouldly;
using Xunit;

namespace OrderHandler.Tests.Helpers {
	public class PriceHelperTests {
		[Fact]
		public void Returns_Not_Null() {
			var order = Substitute.For<Order>();

			var sut = new PriceHelper();

			var result = sut.GetTotalPriceForOrder(order);

			result.ShouldNotBeNull();
		}

		[Theory]
		[InlineData(1, 2, 1, 13.4)]
		[InlineData(3, 4, 1, 29.3)]
		[InlineData(5, 6, 1, 45.2)]
		public void Returns_Calculated_Price(int numberOfFirstArticle, int numberOfSecondArticle, int customerType, double expected) {
			var order = CreateOrder(numberOfFirstArticle, numberOfSecondArticle, customerType);

			var sut = new PriceHelper();

			var result = sut.GetTotalPriceForOrder(order);

			result.ShouldBe(expected);
		}

		[Theory]
		[InlineData(1, 2, 2, 12.06)]
		[InlineData(3, 4, 2, 26.37)]
		[InlineData(5, 6, 3, 40.68)]
		public void Returns_Calculated_Price_With_10_percent_discount(int numberOfFirstArticle, int numberOfSecondArticle, int customerType, double expected) {
			var order = CreateOrder(numberOfFirstArticle, numberOfSecondArticle, customerType);

			var sut = new PriceHelper();

			var result = sut.GetTotalPriceForOrder(order);

			result.ShouldBe(expected);
		}

		private Order CreateOrder(int numberOfFirstArticle, int numberOfSecondArticle, int customerType) {
			var articleList = new List<OrderArticle> {
				new OrderArticle {
					ArticleName = "abc",
					ArticleNumber = 1,
					NumberOfArticles = numberOfFirstArticle
				},
				new OrderArticle {
					ArticleName = "def",
					ArticleNumber = 2,
					NumberOfArticles = numberOfSecondArticle
				},
			};

			var order = new Order {
				CustomerType = customerType,
				Articles = articleList
			};

			return order;
		}
	}
}
