using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class AbstractInitializationExaption : Exception
    {
        public AbstractInitializationExaption(string message) : base(message) { }
    }
}
