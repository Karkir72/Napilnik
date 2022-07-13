using System;
using System.Collections.Generic;

namespace Napilnik
{
    public class Warehouse
    {
        private Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public void Delive(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException();
            if (count < 1)
                throw new ArgumentOutOfRangeException();

            _goods.Add(good, count);
        }

        public bool HasGoods(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException();

            if (_goods.ContainsKey(good) && _goods[good] >= count)
                return true;
            else
                return false;
        }

        public void ShowAllGoods()
        {
            Console.WriteLine("Товары на складе:");

            if (_goods.Count == 0)
                Console.WriteLine("Пусто.");
            else
                foreach (var good in _goods)
                    Console.WriteLine("\n" + good.Key.Name + " " + good.Value);
        }
    }
}
