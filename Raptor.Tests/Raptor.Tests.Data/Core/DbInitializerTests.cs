using Raptor.Data.Core;
using Xunit;

namespace Raptor.Tests.Data.Core
{
    public class DbInitializerTests
    {
        [Fact]
        public void TryInitializingDatabase() {
            // Should run smoothly wihout any exceptions.
            DbInitializer.Seed();
        }
    }
}
