using System.Collections.Generic;

namespace OrderHandler.Entities {
	public class Article {
		public int ArticleNumber { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }

		public Article(int articleNumber, string name, double price) {
			ArticleNumber = articleNumber;
			Name = name;
			Price = price;
		}

		//Get list from datasource to be able to add and remove products runtime
		public static readonly List<Article> ArticleList = new List<Article> {
			new Article(1, "penna", 2.50),
			new Article(2, "block", 5.45),
			new Article(3, "papper", 0.50),
			new Article(4, "suddgummi", 2.15),
			new Article(5, "cykel", 0.0)
		};
	}
}
