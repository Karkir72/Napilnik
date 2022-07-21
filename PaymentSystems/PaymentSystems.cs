using System;
using System.Security.Cryptography;
using System.Text;

namespace Napilnik.PaymentSystems
{
    class Programm
    {
        static void Main(string[] args)
        {
            Order order = new Order(12345, 12000, "RUB");

            PaymentSystem system1 = new PaymentSystem(new System1("pay.system1.ru"));
            PaymentSystem system2 = new PaymentSystem(new System2("order.system2.ru"));
            PaymentSystem system3 = new PaymentSystem(new System3("system3.com", "23jfwbejigb28934tjkwe"));

            system1.ShowPayingLink(order);
            system2.ShowPayingLink(order);
            system3.ShowPayingLink(order);
        }
    }

    public class Order
    {
        private int _id;
        private int _amount;
        private string _curency;

        public int Id => _id;
        public int Amount => _amount;
        public string Curency => _curency;

        public Order(int id, int amount, string curency)
        {
            _id = id;
            _amount = amount;
            _curency = curency;
        }
    }

    class PaymentSystem
    {
        private IPaymentSystem _paymentSystem;

        public PaymentSystem(IPaymentSystem paymentSystem)
        {
            _paymentSystem = paymentSystem;
        }

        public void ShowPayingLink(Order order)
        {
            Console.WriteLine(_paymentSystem.GetPayingLink(order));
        }
    }

    public interface IPaymentSystem
    {
        string GetPayingLink(Order order);
        string GetHash(Order order);
    }

    public class System1 : IPaymentSystem
    {
        string _mainPage;

        public System1(string mainPage)
        {
            _mainPage = mainPage;
        }

        public string GetPayingLink(Order order)
        {

            string link = _mainPage + "/order?amount=" + order.Amount + "&hash=" + GetHash(order);

            return link;
        }

        public string GetHash(Order order)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(order.Id.ToString()));

            return Convert.ToBase64String(hash);
        }
    }

    public class System2 : IPaymentSystem
    {
        string _mainPage;

        public System2(string mainPage)
        {
            _mainPage = mainPage;
        }

        public string GetPayingLink(Order order)
        {
            string link = _mainPage + "/pay?hash=" + GetHash(order);

            return link;
        }

        public string GetHash(Order order)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(order.Id.ToString() + order.Amount.ToString()));

            return Convert.ToBase64String(hash);
        }
    }

    public class System3 : IPaymentSystem
    {
        string _mainPage;
        string _secretKey;

        public System3(string mainPage, string secretKey)
        {
            _mainPage = mainPage;
            _secretKey = secretKey;
        }

        public string GetPayingLink(Order order)
        {
            string link = _mainPage + "/pay?amount=" + order.Amount + "&curency=" + order.Curency + "&hash=" + GetHash(order);

            return link;
        }

        public string GetHash(Order order)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(order.Id.ToString() + order.Amount.ToString() + _secretKey));

            return Convert.ToBase64String(hash);
        }
    }
}
