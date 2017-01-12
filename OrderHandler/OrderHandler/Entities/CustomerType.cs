using System.Collections.Generic;

namespace OrderHandler.Entities {
	public class CustomerType {
		public int Id { get; set; }
		public string Name { get; set; }

		public CustomerType(int id, string name) {
			Id = id;
			Name = name;
		}

		// TODO: Get customerType from datasource to be able to add or remove type runtime
		public static readonly List<CustomerType> CustomerTypes = new List<CustomerType> {
			new CustomerType(1, "privatperson"),
			new CustomerType(2, "företagskund"),
			new CustomerType(3, "stor företagskund")
		};
	}
}
