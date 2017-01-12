using Autofac;
using OrderHandler.Helpers;

namespace OrderHandler {
	public static class DefaultBootstrapper {
		public static IContainer Configure() {
			var builder = new ContainerBuilder();

			builder.RegisterType<XmlHelper>().As<IXmlHelper>();
			builder.RegisterType<OrderHelper>().As<IOrderHelper>();
		    builder.RegisterType<PriceHelper>().As<IPriceHelper>();

			return builder.Build();
		}
	}
}
