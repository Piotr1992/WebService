using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpers
{
    [Serializable]
    public class CDNResponse
    {
        public int Id { get; set; }
        public string Error { get; set; }
        public string Numer { get; set; }
    }
    [Serializable]
    public class Response
    {
        public int ErrorId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ErrorType
    {
        unknown=0,
        incorrectLogin=1,
        incorrectPassword=2,
        incorrectParameters=3

    }
}