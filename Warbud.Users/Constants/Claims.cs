using System.Collections.Generic;
using System.Linq;

namespace Warbud.Users.Constants
{
    public static class Claims
    {
        public static class ClaimsNames
        {
            public const string Id  = "id";
            public const string Role  = "role";
        }
        
        public static class RoleValues
        {
            public const string Admin  = "Admin";
            public const string BasicUser  = "BasicUser";
            public const string Viewer  = "Viewer";
        }
        
        public static class ClaimValues
        {
            public const string View  = "View";
            public const string Modify  = "Modify";
            public const string Create  = "Create";

            public static List<string> GetValueList()
            {
                return typeof(ClaimValues).GetFields().Select(x => x.Name).ToList();
            }
        }
    }

    public static class Policy
    {
        public static class PolicyNames
        {
            public const string Owner  = "Owner";
            public const string DoILikeYou  = "DoILikeYou";
        }
        
        public static class PolicyValues
        {
            public const string Admin  = "Admin";
            public const string BasicUser  = "BasicUser";
            public const string Viewer  = "Viewer";
            public const string Yes  = "Yes";
            public const string No  = "No";
            
        }
    }
}