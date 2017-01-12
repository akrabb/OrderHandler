using OrderHandler.Entities;

namespace OrderHandler.Helpers {
	public interface IPriceHelper {
		double GetTotalPriceForOrder(Order order);
	}
}
