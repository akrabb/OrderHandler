using Autofac;
using OrderHandler.Helpers;

namespace OrderHandler {
	class Program {
		private static IOrderHelper orderHelper;

		static void Main(string[] args) {
			var container = DefaultBootstrapper.Configure();
			using (var scope = container.BeginLifetimeScope()) {
				orderHelper = scope.Resolve<IOrderHelper>();
			}

			orderHelper.ShowOrderMenu();
		}
	}
}
