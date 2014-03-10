using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Office365ClaimsService
{
    public enum QueryType
    {
        Search,
        Lists,
    }
    public static class QueryStringHelper
    {
        public static Uri BuildQuery(QueryType type,string url,string query)
        {
            UriBuilder bldr = new UriBuilder(url);
            switch (type)
            {
                case QueryType.Search:
                    string restQuery = "";

                    if (url.EndsWith("/") != true)
                    {
                        restQuery = "/";
                    }

                    restQuery += "_api/search/query";

                    bldr.Path += restQuery;

                    bldr.Query = "querytext='" + query + "'";
                    break;
                case QueryType.Lists:
                    break;
                default:
                    break;
            }
            return bldr.Uri;
        }
    }
}