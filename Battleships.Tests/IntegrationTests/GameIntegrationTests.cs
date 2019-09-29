using Battleships.Core;
using Battleships.Core.Grid;
using Xunit;

namespace Battleships.Tests.IntegrationTests
{
    public class GameIntegrationTests
    {
        [Fact]
        public void TestGame_Init()
        {
            var g = new Game();
            g.Init();
            Assert.Equal(g.Ships.Length, GameSettings.DestroyersCount + GameSettings.BattleshipCount);
        }

        [Fact]
        public void TestGame_InitShips()
        {
            var gridManager = new GridManager();
                gridManager.Init();
            var g = new Game(gridManager);
                g.InitShips(GameSettings.DestroyersCount, GameSettings.BattleshipCount);
            bool allShipsArePlaced = false;
            foreach (var ship in g.Ships)
                allShipsArePlaced = ship.IsPlaced;
            Assert.True(allShipsArePlaced);
        }

        [Fact]
        public void TestGame_ValidateShipLocations()
        {
            var gridManager = new GridManager();
            var g = new Game(gridManager);
            g.Init();
            foreach (var ship in g.Ships)
                Assert.Equal(ship.Positions.Count, ship.Squares);
        }

        [Fact]
        public void TestGame_CheckStatus()
        {
            var shoot = "A0";
            var gridManager = new GridManager();
            var g = new Game(gridManager);
            g.Init();
            g.Shoot(shoot);
            Assert.False(g.IsGameFinished());
        }

        [Fact]
        public void TestGame_ShootAllShipsFinishGame()
        {
            var gridManager = new GridManager();
            var g = new Game(gridManager);
            g.Init();
            foreach (var ship in g.Ships)
            {
                foreach (var position in ship.Positions)
                {
                    var response = g.ShootAtPosition(position);
                    Assert.Equal(response.Status, GridStates.Hit);
                    Assert.True(response.IsSuccess);
                }
            }
            Assert.True(g.IsGameFinished());
        }
    }
}
