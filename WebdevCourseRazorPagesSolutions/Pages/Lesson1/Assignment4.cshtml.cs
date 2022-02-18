using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Exercises.Pages.Lesson1
{
    public class Assignment4 : PageModel
    {
        public void OnGet()
        {

        }
    }

    public class CategoryConstraint : IRouteConstraint
    {
        public CategoryConstraint()
        {

        }
        public bool Match(HttpContext httpContext, IRouter route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object value))
            {
                string stringValue = Convert.ToString(value).ToLower();
                if (stringValue.StartsWith("cat"))
                {
                    string numberPart = stringValue.Replace("cat", "");
                    bool result = int.TryParse(numberPart, out int number);
                    return result;
                }

                if (stringValue.StartsWith("subcat"))
                {
                    string numberPart = stringValue.Replace("subcat", "");
                    bool result = int.TryParse(numberPart, out int number);
                    return result;
                }
            }

            return false;
        }
    }
}
