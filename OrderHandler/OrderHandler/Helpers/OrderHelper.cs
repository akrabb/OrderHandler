using System;
using System.Collections.Generic;
using System.Linq;
using OrderHandler.Entities;

namespace OrderHandler.Helpers {
	public class OrderHelper : IOrderHelper {
		private readonly IXmlHelper xmlHelper;
		private readonly IPriceHelper priceHelper;
		const string FilePath = @"C:\Orders";

		public OrderHelper(IXmlHelper xmlHelper,
			IPriceHelper priceHelper) {
			this.xmlHelper = xmlHelper; 
			this.priceHelper = priceHelper;
		}

		public void ShowOrderMenu() {
			Console.WriteLine("1. Skapa order" +
							  Environment.NewLine + "2. Visa orderlista" +
							  Environment.NewLine + "3. Visa order" +
							  Environment.NewLine + "4. Avsluta");
			var userInput = Console.ReadLine();
			int numericUserInput;
			if (int.TryParse(userInput, out numericUserInput)) {
				switch (numericUserInput) {
					case 1:
						CreateOrder();
						break;
					case 2:
						ShowOrderList();
						break;
					case 3:
						ShowOrder();
						break;
					case 4:
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Det valet finns inte!" +
										  Environment.NewLine + "Va snäll och ange ett giltigt värde, 1, 2 eller 3");
						break;
				}
			} else {
				Console.WriteLine("Det valet finns inte!" +
								  Environment.NewLine + "Va snäll och ange ett giltigt värde, 1, 2 eller 3");
			}
		}

		public Order GetOrder(Guid orderNumber) {
			var orderList = xmlHelper.FromXmlFile<OrderList>(FilePath);
			return orderList.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
		}

		public void ShowOrder() {
			Console.WriteLine("Ange ordernummer");
			var userInput = Console.ReadLine();
			Guid orderNumber;
			while (!Guid.TryParse(userInput, out orderNumber)) {
				if (userInput == "menu") {
					ShowOrderMenu();
				} else {
					Console.WriteLine("Ogiltigt val. Ange ett giltigt ordernummer eller menu för att återgå till huvudmenyn.");
				}
				userInput = Console.ReadLine();
			}

			var order = GetOrder(orderNumber);

			Console.WriteLine(FormatHelper.FormatOrder(new List<Order> { order }));
			Console.WriteLine("Skriv menu för att komma till huvudmenyn eller avsluta med valfri tangent.");
			userInput = Console.ReadLine();
			if (userInput == "menu") {
				ShowOrderMenu();
			} else {
				Environment.Exit(0);
			}
		}

		public void ShowOrderList() {
			var orderList = xmlHelper.FromXmlFile<OrderList>(FilePath);
			Console.WriteLine(FormatHelper.FormatOrder(orderList.Orders));
			Console.WriteLine("Skriv menu för att komma till huvudmenyn eller avsluta med valfri tangent.");
			var userInput = Console.ReadLine();
			if (userInput == "menu") {
				ShowOrderMenu();
			} else {
				Environment.Exit(0);
			}
		}

		public void CreateOrder() {
			var order = GetOrderInput();
			var orderList = xmlHelper.FromXmlFile<OrderList>(FilePath) ?? new OrderList();
			orderList.Orders.Add(order);

			xmlHelper.ToXmlFile(orderList, FilePath);

			ShowOrderMenu();
		}

		private Order GetOrderInput() {
			var customerType = InputCustomerType();

			var order = new Order {
				OrderNumber = Guid.NewGuid(),
				CustomerType = customerType,
				OrderDate = DateTime.Now
			};

			InputArticles(order);

			return order;
		}

		private void InputArticles(Order order) {
			var continueAddArticle = "j";
			while(continueAddArticle == "j") {
				Console.WriteLine("Ange typ av artikel:");
				foreach(var item in Article.ArticleList.Where(a => a.Price > 0)) {
					Console.WriteLine($"{item.ArticleNumber}. {item.Name} ({item.Price} kr/st)");
				}
				var userInput = Console.ReadLine();
				int articleNumber;
				while(!int.TryParse(userInput, out articleNumber) || (articleNumber < 1 || articleNumber > 4)) {
					if(userInput == "menu") {
						ShowOrderMenu();
					} else {
						Console.WriteLine("Ogiltigt val. Ange ett värde mellan 1 och 4 eller menu för att återgå till huvudmenyn.");
					}
					userInput = Console.ReadLine();
				}

				var article = Article.ArticleList.FirstOrDefault(a => a.ArticleNumber == articleNumber);
				var orderArticle = new OrderArticle {
					ArticleNumber = articleNumber,
					ArticleName = article?.Name,
					ArticlePrice = article?.Price ?? 0.0
				};

				Console.WriteLine("Ange antal artiklar (1-100st)");
				userInput = Console.ReadLine();
				int numberOfArticles;
				var isNumeric = int.TryParse(userInput, out numberOfArticles);

				while(!isNumeric || numberOfArticles < 1 || numberOfArticles > 100) {
					if(userInput == "menu") {
						ShowOrderMenu();
					} else {
						Console.WriteLine("Ogiltigt val.");
						Console.WriteLine("Antal artiklar måste vara en siffra mellan 1 och 100.");
					}
					userInput = Console.ReadLine();
					isNumeric = int.TryParse(userInput, out numberOfArticles);
				}
				orderArticle.NumberOfArticles = numberOfArticles;

				order.Articles.Add(orderArticle);

				order.TotalPrice = priceHelper.GetTotalPriceForOrder(order);

				Console.WriteLine("Vill du lägga till fler artiklar? (j för ja)");
				continueAddArticle = Console.ReadLine()?.ToLower();
			}
		}

		private int InputCustomerType() {
			Console.WriteLine("Ange typ av kund:");
			foreach(var type in CustomerType.CustomerTypes) {
				Console.WriteLine($"{type.Id}. {type.Name}");
			}

			var userInput = Console.ReadLine();
			int customerType;
			while(!int.TryParse(userInput, out customerType) || (customerType < 1 || customerType > 3)) {
				if(userInput == "menu") {
					ShowOrderMenu();
				} else {
					Console.WriteLine("Ogiltigt val. Ange ett värde mellan 1 och 3 eller menu för att återgå till huvudmenyn.");
				}
				userInput = Console.ReadLine();
			}

			return customerType;
		}
	}
}
