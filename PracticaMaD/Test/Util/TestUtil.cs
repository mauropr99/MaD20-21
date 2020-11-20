using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LanguageDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Test.Util
{
    public class TestUtil
    {
        public static IOrderDao orderDao;
        public static IOrderLineDao orderLineDao;
        public static IProductDao productDao;
        public static ICategoryDao categoryDao;
        public static IUserDao userDao;
        public static ILanguageDao languageDao;
        public static ICreditCardDao creditCardDao;

        public static Language CreateExistentLanguage()
        {
            Language language = new Language
            {
                name = "español",
                country = "España"
            };
            languageDao.Create(language);

            return language;
        }

        public static User CreateExistentUser( Language language)
        {
            User user = new User
            {
                login = "user",
                name = "usuario",
                lastName = "dePrueba",
                password = "passwd",
                address = "A Coruña",
                email = "user@user",
                role = "user",
                languageId = language.id
            };
            userDao.Create(user);

            return user;
        }

        public static Order CreateOrder(User user, CreditCard credirCard, List<OrderLine> orderLines)
        {
            Order order = new Order
            {
                postalAddress = "A Coruña"
            };
            foreach (OrderLine orderLine in orderLines)
            {
                order.totalPrice = orderLine.price + order.totalPrice;
            }
            order.description = "Regalo para mauro";
            order.creditCardId = credirCard.id;
            order.userId = user.id;
            orderDao.Create(order);
            foreach (OrderLine orderLine in orderLines)
            {
                orderLine.orderId = order.id;
                orderLineDao.Create(orderLine);
            }
            return order;
        }

        public static OrderLine CreateOrderLine(Product product)
        {
            OrderLine orderLine = new OrderLine
            {
                quantity = 2,
                price = product.price,
                productId = product.id
            };

            orderLineDao.Create(orderLine);
            return orderLine;
        }

        public static Product CreateProduct(Category category, string productName, decimal price)
        {
            Product product = new Product
            {
                product_name = productName,
                price = price,
                releaseDate = DateTime.Now,
                stock = 100,
                categoryId = category.id
            };

            productDao.Create(product);
            return product;
        }

        public static Category CreateCategory( string categoryName)
        {
            Category category = new Category
            {
                name = categoryName
            };

            categoryDao.Create(category);
            return category;
        }

        public static CreditCard CreateCreditCard()
        {
            CreditCard creditCard = new CreditCard
            {
                creditType = "debit",
                creditCardNumber = "1234567891234567",
                cvv = 123,
                expirationDate = DateTime.Now.AddYears(1)
            };

            creditCardDao.Create(creditCard);
            return creditCard;
        }
    }
}
