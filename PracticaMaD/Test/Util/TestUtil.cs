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

        public static Language CreateExistentLanguage(Language language)
        {
            language.name = "español";
            language.country = "España";
            languageDao.Create(language);

            return language;
        }

        public static User CreateExistentUser(User user, Language language)
        {

            user.login = "user";
            user.name = "usuario";
            user.lastName = "dePrueba";
            user.password = "passwd";
            user.address = "A Coruña";
            user.email = "user@user";
            user.role = "user";
            user.languageId = language.id;
            userDao.Create(user);

            return user;
        }

        public static Order CreateOrder(User user, CreditCard credirCard, List<OrderLine> orderLines, Order order)
        {
            order.postalAddress = "A Coruña";
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

        public static OrderLine CreateOrderLine(Product product, OrderLine orderLine)
        {
            orderLine.quantity = 2;
            orderLine.price = product.price;
            orderLine.productId = product.id;

            orderLineDao.Create(orderLine);
            return orderLine;
        }

        public static Product CreateProduct(Category category, string productName, decimal price, Product product)
        {
            product.product_name = productName;
            product.price = price;
            product.releaseDate = DateTime.Now;
            product.stock = 100;
            product.categoryId = category.id;

            productDao.Create(product);
            return product;
        }

        public static Category CreateCategory(Category category, string categoryName)
        {
            category.name = categoryName;

            categoryDao.Create(category);
            return category;
        }

        public static CreditCard CreateCreditCard(CreditCard creditCard)
        {
            creditCard.creditType = "debit";
            creditCard.creditCardNumber = "1234567891234567";
            creditCard.cvv = 123;
            creditCard.expirationDate = DateTime.Now.AddYears(1);

            creditCardDao.Create(creditCard);
            return creditCard;
        }
    }
}
