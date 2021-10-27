using System.Collections.Generic;
using System.Linq;

namespace Warbud.Users.Constants
{
    public static class Claims
    {
        public static class Names
        {
            public const string Id  = "id";
            public const string Role  = "role";
        }
        
        public static class RoleValues
        {
            public const string View  = "View";
            public const string Modify  = "Modify";
            public const string Create  = "Create";

            public static List<string> GetValueList()
            {
                return typeof(RoleValues).GetFields().Select(x => x.Name).ToList();
            }
        }
    }
}