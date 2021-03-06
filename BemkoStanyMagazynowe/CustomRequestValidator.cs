using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace BemkoStanyMagazynowe
{
    public class CustomRequestValidator : RequestValidator
    {
        /// <summary>
        /// Validates a string that contains HTTP request data.
        /// </summary>
        /// <param name="context">The context of the current request.</param>
        /// <param name="value">The HTTP request data to validate.</param>
        /// <param name="requestValidationSource">An enumeration that represents the source of request data that is being validated. The following are possible values for the enumeration:QueryStringForm CookiesFilesRawUrlPathPathInfoHeaders</param>
        /// <param name="collectionKey">The key in the request collection of the item to validate. This parameter is optional. This parameter is used if the data to validate is obtained from a collection. If the data to validate is not from a collection, <paramref name="collectionKey"/> can be null.</param>
        /// <param name="validationFailureIndex">When this method returns, indicates the zero-based starting point of the problematic or invalid text in the request collection. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the string to be validated is valid; otherwise, false.
        /// </returns>
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            // Set a default value for the out parameter.
            validationFailureIndex = -1;

            return true;

            //    // All other HTTP input checks are left to the base ASP.NET implementation.
            //    return base.IsValidRequestString(
            //                                        context,
            //                                        value,
            //                                        requestValidationSource,
            //                                        collectionKey,
            //                                        out validationFailureIndex);            
        }
    }
}