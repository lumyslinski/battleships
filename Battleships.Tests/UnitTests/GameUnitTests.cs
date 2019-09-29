using Battleships.Core;
using Xunit;

namespace Battleships.Tests.UnitTests
{
    public class GameUnitTests
    {
        private Game game;

        public GameUnitTests()
        {
            game = new Game();
        }

        [Fact]
        public void TestGame()
        {
            Game game = new Game();
            Assert.NotNull(game);
        } 
    }
}
