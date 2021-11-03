using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Warbud.Shared.Constants
{
    public static class Claim
    {
        public static class Name
        {
            public const string Id  = ClaimTypes.NameIdentifier;
            public const string Role  = ClaimTypes.Role;
        }
        
        public static class Value
        {
            public const string View  = "View";
            public const string Modify  = "Modify";
            public const string Create  = "Create";

            public static List<string> GetValueList()
            {
                return typeof(Value).GetFields().Select(x => x.Name).ToList();
            }
        }
    }

    
}