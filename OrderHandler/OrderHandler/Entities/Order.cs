using System;
using System.Collections.Generic;

namespace OrderHandler.Entities {
	public class Order {
		public Guid OrderNumber { get; set; }
		public int CustomerType { get; set; }
		public DateTime OrderDate { get; set; }
		public List<OrderArticle> Articles { get; set; }
	    public double TotalPrice { get; set; }

	    public Order() {
			Articles = new List<OrderArticle>();
		}
	}
}
