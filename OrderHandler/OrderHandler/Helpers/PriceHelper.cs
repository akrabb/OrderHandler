using System;
using System.Linq;
using OrderHandler.Entities;

namespace OrderHandler.Helpers {
	public class PriceHelper : IPriceHelper {
		public double GetTotalPriceForOrder(Order order) {
			double totalPrice = 0.0;

			foreach(var article in order.Articles) {
				var listArticle = Article.ArticleList
					.FirstOrDefault(a => a.ArticleNumber == article.ArticleNumber);
				var price = article.NumberOfArticles * listArticle?.Price ?? 0.0;
				totalPrice += price;
			}

			if(order.CustomerType > 1) {
				totalPrice = totalPrice * (1 - 0.1);
				totalPrice = Math.Round(totalPrice, 2);
			}

			return totalPrice;
		}
	}
}
