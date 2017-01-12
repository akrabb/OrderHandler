using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkdownLog;
using OrderHandler.Entities;

namespace OrderHandler.Helpers {
	public static class FormatHelper {
		public static string FormatOrder(IList<Order> orderList) {
			var data = Enumerable.Empty<object>()
						.Select(o => new {
							Ordernummer = new Guid(), Datum = DateTime.MinValue, Kundtyp = string.Empty,
							Artiklar = string.Empty, Totalprice = 0.0
						}).ToList();

			foreach(var order in orderList) {
				var articleList = new StringBuilder();
				foreach (var article in order.Articles) {
					articleList.Append($"* {article.NumberOfArticles} {article.ArticleName} (artikelnr. {article.ArticleNumber}, {article.ArticlePrice}/st) ");
				}

				var customerType = CustomerType.CustomerTypes.FirstOrDefault(c => c.Id == order.CustomerType)?.Name;

				data.Add(new { Ordernummer = order.OrderNumber, Datum = order.OrderDate,
					Kundtyp = customerType, Artiklar = articleList.ToString(),
					Totalprice = order.TotalPrice});
			}

			return data.ToMarkdownTable().ToMarkdown();
		}
	}
}
