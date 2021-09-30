using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace PortalApp.Controllers
{
    public class guidClass
    {
        public static bool isGuid(string guidString)
        {
            bool IsGuid = false;
            if (!string.IsNullOrEmpty(guidString))
            {
                Regex guidRegExp = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                IsGuid = guidRegExp.IsMatch(guidString);
            }
            return IsGuid;
        }
    }
}