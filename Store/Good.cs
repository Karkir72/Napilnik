using System;

namespace Napilnik
{
    public class Good
    {
        public string Name { get; private set;}

        public Good(string name)
        {
            if (name == null)
                throw new ArgumentNullException();
            else
                Name = name;
        }
    }
}
