using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceTest
{
    public static class Locker
    {
        public static Object lockSem = new Object();
    }
}
