using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
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
        public static IBookDao bookDao;
        public static IComputerDao computerDao;
        public static IProductDao productDao;
        public static ICategoryDao categoryDao;
        public static IUserDao userDao;
        public static ILanguageDao languageDao;
        public static ICreditCardDao creditCardDao;
        public static ICommentDao commentDao;
        public static ILabelDao labelDao;

        public static Language CreateExistentLanguage()
        {
            Language language = languageDao.FindByNameAndCountry("es", "Es");
            if (language == null)
            { 
                language = new Language
                {
                    name = "es",
                    country = "ES"
                };
                languageDao.Create(language);
            }
            return language;
        }

        public static User CreateExistentUser(Language language)
        {
            User user = userDao.FindByLogin("user");
            if (user == null)
            {
                user = new User
                {
                    login = "user",
                    name = "usuario",
                    lastName = "dePrueba",
                    password = "passwd",
                    email = "user@user",
                    role = "user",
                    languageId = language.id,
                    favouriteCreditCard = null
                };
                userDao.Create(user);
            }
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

        public static Computer CreateComputer(Category category, string productName, decimal price, string brand)
        {
            Computer computer = new Computer
            {
                product_name = productName,
                price = price,
                releaseDate = DateTime.Now,
                stock = 100,
                categoryId = category.id,
                brand = brand,
                processor = "intel i7",
                os = "Windows"
            };

            computerDao.Create(computer);
            return computer;
        }

        public static Book CreateBook(Category category, string productName, decimal price, string author)
        {
            Book book = new Book
            {
                product_name = productName,
                price = price,
                releaseDate = DateTime.Now,
                stock = 100,
                categoryId = category.id,
                author = author,
                genre = "Fantasy",
                editorial = "Planeta",
                isbnCode = "978-0-226-26421-9"
            };

            bookDao.Create(book);
            return book;
        }

        public static Category CreateCategory(string categoryName)
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
                ownerName = "Name Surname",
                creditType = "debit",
                creditCardNumber = "1234567890123456",
                cvv = 123,
                expirationDate = DateTime.Now.AddYears(1)
            };
       
            creditCardDao.Create(creditCard);
            return creditCard;
        }

        public static Comment CreateComment(User user, Product product)
        {
            Comment comment = new Comment
            {
                userId = user.id,
                productId = product.id,
                text = "Soy un comentario",
                commentDate = DateTime.Now
            };
            commentDao.Create(comment);

            return comment;
        }
    }
}
