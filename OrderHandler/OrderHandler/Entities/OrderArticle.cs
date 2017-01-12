namespace OrderHandler.Entities {
	public class OrderArticle {
		public int ArticleNumber { get; set; }
		public string ArticleName { get; set; }
		public int NumberOfArticles { get; set; }
		public double ArticlePrice { get; set; }
		public int Discount { get; set; }
	}
}
