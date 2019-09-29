using Battleships.Core.Grid;
using Battleships.Core.Ships;
using Xunit;

namespace Battleships.Tests.UnitTests
{
    public class AlgorithmUnitTests
    {
        [Fact]
        public void TestGame_ConvertShootToRowAndColumn()
        {
            var shoot = "C9";
            var position = new ShipPosition(shoot);
            Assert.Equal(position.Row, GridRows.C);
            Assert.Equal(position.Column, 8);
        }

    }
}
