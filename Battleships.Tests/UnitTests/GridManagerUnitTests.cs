using Battleships.Core;
using Battleships.Core.Grid;
using Battleships.Core.Ships;
using Xunit;

namespace Battleships.Tests.UnitTests
{
    public class GridManagerUnitTests
    {
        private GridManager gridManager;

        public GridManagerUnitTests()
        {
            this.gridManager = new GridManager();
            this.gridManager.Init();
        }

        [Fact]
        public void TestGridManager_InitGrid()
        {
            this.gridManager.InitGrid(GameSettings.GridSize, GameSettings.GridSize);
            Assert.Equal(gridManager.GetGridLength(), GameSettings.GridSize * GameSettings.GridSize);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Top_Right_Horizontally()
        {
            var destroyer = new Destroyer(1);
            gridManager.PlaceShip(destroyer, (int)GridRows.A, GameSettings.GridArrayMaxSize, ShipPlacementMode.Horizontally);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Top_Right_Vertically()
        {
            var destroyer = new Destroyer(1);
            gridManager.PlaceShip(destroyer, (int)GridRows.A, GameSettings.GridArrayMaxSize, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Top_Left_Horizontally()
        {
            var destroyer = new Destroyer(1);
            gridManager.PlaceShip(destroyer, (int)GridRows.A, 0, ShipPlacementMode.Horizontally);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Top_Left_Vertically()
        {
            var destroyer = new Destroyer(1);
            gridManager.PlaceShip(destroyer, (int)GridRows.A, 0, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Bottom_Right_Horizontally()
        {
            var destroyer = new Destroyer(3);
            gridManager.PlaceShip(destroyer, (int)GridRows.J, GameSettings.GridArrayMaxSize, ShipPlacementMode.Horizontally);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Bottom_Right_Vertically()
        {
            var destroyer = new Destroyer(3);
            gridManager.PlaceShip(destroyer, (int)GridRows.J, GameSettings.GridArrayMaxSize, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Bottom_Left_Horizontally()
        {
            var destroyer = new Destroyer(3);
            gridManager.PlaceShip(destroyer, (int)GridRows.J, 0, ShipPlacementMode.Horizontally);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Bottom_Left_Vertically()
        {
            var destroyer = new Destroyer(3);
            gridManager.PlaceShip(destroyer, (int)GridRows.J, 0, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_Middle()
        {
            var destroyer = new Destroyer(0);
            gridManager.PlaceShip(destroyer, (int)GridRows.E, 4, ShipPlacementMode.Horizontally);
            Assert.True(destroyer.IsPlaced);
            var destroyer1 = new Destroyer(1);
            gridManager.PlaceShip(destroyer1, (int)GridRows.E, 5, ShipPlacementMode.Vertically);
            Assert.False(destroyer1.IsPlaced);
        }

        [Fact]
        public void TestGridManager_PlaceShips_FindRowAvailableForThisColumn()
        {
            var destroyer = new Destroyer(3);
            gridManager.PlaceShip(destroyer, (int)GridRows.A, 0, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);
            var row = gridManager.FindRowAvailableForThisColumn(0);
            Assert.True(row > 0);
        }

        [Fact]
        public void TestGridManager_ShootShip()
        {
            var shoot = "C9";
            var position = new ShipPosition(shoot);
            var destroyer = new Destroyer(3);
            var currentShipLife = destroyer.Life;
            var gridManager = new GridManager();
            gridManager.Init();
            gridManager.PlaceShip(destroyer, (int)position.Row, position.Column, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);
            var place = gridManager.GetPlace(position);
                place.ShootShip();
            Assert.Equal(place.State, GridStates.Hit);
            Assert.True(destroyer.Life == currentShipLife - 1);
        }

        [Fact]
        public void TestGridManager_SinkShip()
        {
            var destroyer = new Destroyer(3);
            var gridManager = new GridManager();
            gridManager.Init();
            gridManager.PlaceShip(destroyer, (int)GridRows.A, 0, ShipPlacementMode.Vertically);
            Assert.True(destroyer.IsPlaced);

            for(int i=0;i<GameSettings.GridSize;i++)
            {
                var row = (GridRows)i;
                var shoot = row + "1";
                var position = new ShipPosition(shoot);
                if (position.Row != GridRows.Undefined && position.Column != -1)
                {
                    var place = gridManager.GetPlace(position);
                    place.ShootShip();
                    if (place.IsThereShip)
                        Assert.Equal(place.State, GridStates.Hit);
                    else
                        Assert.Equal(place.State, GridStates.Miss);
                }
            }
            Assert.True(destroyer.IsSunk);
        }
    }
}
