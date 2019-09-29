using Battleships.Core.Grid;
using Battleships.Core.Ships;
using Xunit;

namespace Battleships.Tests.UnitTests
{
    public class ShipUnitTests
    {
        [Fact]
        public void TestBattleship()
        {
            Battleship battleship = new Battleship(1);
            Assert.Equal(battleship.Name, "Battleship1");
            Assert.Equal(battleship.Squares, 5);
        }

        [Fact]
        public void TestDestroyer()
        {
            Destroyer destroyer = new Destroyer(2);
            Assert.Equal(destroyer.Name, "Destroyer2");
            Assert.Equal(destroyer.Squares, 4);
        }

        [Fact]
        public void TestGame_GridLocation()
        {
            var state = GridStates.Ship;
            var ship = new Destroyer(1);
            GridLocation location = new GridLocation(ship);
            Assert.Equal(location.Ship, ship);
            Assert.Equal(location.State, state);
        }
    }
}
