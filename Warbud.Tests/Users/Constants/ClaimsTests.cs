using Warbud.Shared.Constants;
using Xunit;

namespace Warbud.Tests.Users.Constants
{
    public class ClaimsTests
    {
        [Fact]
        public void GetAllConstValues_FromRoleValues()
        {
            var values = Claim.Value.GetValueList();
            
            Assert.Equal(3, values.Count);
        }
    }
}