using System;
using OrderHandler.Entities;

namespace OrderHandler.Helpers {
	public interface IOrderHelper {
		void ShowOrderMenu();
		Order GetOrder(Guid orderNumber);
		void ShowOrder();
		void ShowOrderList();
		void CreateOrder();
	}
}
