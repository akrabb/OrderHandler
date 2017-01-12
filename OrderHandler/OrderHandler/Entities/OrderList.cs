using System.Collections.Generic;

namespace OrderHandler.Entities {
	public class OrderList {
		public List<Order> Orders { get; set; }

		public OrderList() {
			Orders = new List<Order>();
		}
	}
}
