using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasWebService
{
    public static class Locker
    {
        public static Object lockSem = new Object();
    }
}