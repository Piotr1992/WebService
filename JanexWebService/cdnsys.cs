using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

namespace JanexWebService
{
    public class Cdnsys
    {
        [DllImport("ClaRUN.dll")]
        static extern void AttachThreadToClarion(int _flag);
        public void AttachThread()
        {
            //try
            //{
            //    AttachThreadToClarion(1);
            //}
            //catch (Exception) { }
        }
    }
}