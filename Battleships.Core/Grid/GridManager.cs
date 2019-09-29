using Battleships.Core.Interfaces;
using Battleships.Core.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Core.Grid
{
    public class GridManager : IDisposable, IGridManager
    {
        private GridLocation[,] grid;
        public void Init()
        {
            this.InitGrid(GameSettings.GridSize, GameSettings.GridSize);
        }

        public void InitGrid(int columns, int rows)
        {
            this.grid = new GridLocation[rows, columns];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    this.grid[r, c] = new GridLocation();
        }

        public void PlaceShip(BaseShip ship)
        {
            int tryCurrent = 0;
            int tryMax = 10;
            while (tryCurrent <= tryMax && !ship.IsPlaced)
            {
                Random random = new Random();
                ShipPlacementMode mode = GetMode(tryCurrent, random);
                int columnPosition = random.Next(0, GameSettings.GridSize);
                int rowPosition = GetRowPosition(tryCurrent, random, columnPosition);
                if (rowPosition >= 0)
                    this.PlaceShip(ship, rowPosition, columnPosition, mode);
                tryCurrent++;
            }
        }

        public void PlaceShip(BaseShip ship, int rowIndex, int colIndex, ShipPlacementMode mode = ShipPlacementMode.All)
        {
            bool isEnoughPlaceInRightColumnDirection = colIndex + ship.Squares <= GameSettings.GridArrayMaxSize;
            bool isEnoughPlaceInLeftColumnDirection = colIndex - ship.Squares >= 0;
            bool isEnoughPlaceInTopRowDirection = rowIndex - ship.Squares >= 0;
            bool isEnoughPlaceInBottomRowDirection = rowIndex + ship.Squares <= GameSettings.GridArrayMaxSize;
            List<GridLocation> currentShipPlacement = new List<GridLocation>();
            if (isEnoughPlaceInRightColumnDirection && (mode == ShipPlacementMode.Horizontally || mode == ShipPlacementMode.All))
            {
                for (int currentColIndex = colIndex; currentColIndex < (colIndex + ship.Squares); currentColIndex++)
                {
                    PlaceShip(ship, rowIndex, currentColIndex, currentShipPlacement);
                    if (!ship.IsPlaced) break;
                }
            }
            if (!ship.IsPlaced && isEnoughPlaceInLeftColumnDirection && (mode == ShipPlacementMode.Horizontally || mode == ShipPlacementMode.All))
            {
                for (int currentColIndex = colIndex; currentColIndex > (colIndex - ship.Squares); currentColIndex--)
                {
                    PlaceShip(ship, rowIndex, currentColIndex, currentShipPlacement);
                    if (!ship.IsPlaced) break;
                }
            }
            if (!ship.IsPlaced && isEnoughPlaceInTopRowDirection && (mode == ShipPlacementMode.Vertically || mode == ShipPlacementMode.All))
            {
                for (int currentRowIndex = rowIndex; currentRowIndex > (rowIndex - ship.Squares); currentRowIndex--)
                {
                    PlaceShip(ship, currentRowIndex, colIndex, currentShipPlacement);
                    if (!ship.IsPlaced) break;
                }
            }
            if (!ship.IsPlaced && isEnoughPlaceInBottomRowDirection && (mode == ShipPlacementMode.Vertically || mode == ShipPlacementMode.All))
            {
                for (int currentRowIndex = rowIndex; currentRowIndex < (rowIndex + ship.Squares); currentRowIndex++)
                {
                    PlaceShip(ship, currentRowIndex, colIndex, currentShipPlacement);
                    if (!ship.IsPlaced) break;
                }
            }
            currentShipPlacement = null;
        }

        private void PlaceShip(BaseShip ship, int rowIndex, int currentColIndex, List<GridLocation> currentShipPlacement)
        {
            var location = this.grid[rowIndex, currentColIndex];
            if (location.IsThereShip)
            {
                ship.SetIsPlaced(false);
                RevertShipPlacement(currentShipPlacement);
            }
            else
            {
                location.PlaceShip(ship);
                ship.SetIsPlaced(true);
                ship.SetPosition(new ShipPosition(rowIndex, currentColIndex));
                currentShipPlacement.Add(location);
            }
        }
        private void RevertShipPlacement(List<GridLocation> placesToClear)
        {
            foreach (var place in placesToClear)
                place.RemoveShip();
            placesToClear.Clear();
        }

        public int FindRowAvailableForThisColumn(int column)
        {
            int foundRow = -1;
            for (int r = 0; r < GameSettings.GridArrayMaxSize; r++)
            {
                if (!this.grid[r, column].IsThereShip)
                {
                    foundRow = r;
                    break;
                }
            }
            return foundRow;
        }
        private int GetRowPosition(int tryCurrent, Random random, int columnPosition)
        {
            if (tryCurrent < 3)
                return random.Next(0, GameSettings.GridSize);
            else
                return FindRowAvailableForThisColumn(columnPosition);
        }

        private ShipPlacementMode GetMode(int tryCurrent, Random random)
        {
            var modeAll = ShipPlacementMode.All;
            if (tryCurrent <= 3)
                return (ShipPlacementMode)random.Next(0, ((int)modeAll));
            return modeAll;
        }
        public GridLocation GetPlace(ShipPosition position)
        {
            return this.grid[(int)position.Row, position.Column];
        }

        public void Dispose()
        {
            this.grid = null;
        }

        public string GetTableFormat(bool isCheatModeEnabled)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.grid != null)
            {
                for (int c = 0; c <= GameSettings.GridSize; c++)
                {
                    if (c == 0)
                        stringBuilder.Append("_");
                    else
                        stringBuilder.Append(c.ToString());
                    if (c <= GameSettings.GridArrayMaxSize)
                        stringBuilder.Append("|");
                }
                stringBuilder.Append("\n");
                for (int r = 0; r < GameSettings.GridSize; r++)
                {
                    var header = (GridRows)r;
                    stringBuilder.Append(header.ToString() + "|");

                    for (int c = 0; c < GameSettings.GridSize; c++)
                    {
                        var place = this.grid[r, c];
                        var symbol = "o";
                        switch (place.State)
                        {
                            case GridStates.Ship: if (isCheatModeEnabled) symbol = place.Ship.NameSymbol; break;
                            case GridStates.Hit:
                                if (place.IsThereShip && place.Ship.IsSunk)
                                    symbol = "~";
                                else
                                    symbol = "x"; break;
                            case GridStates.Miss: symbol = "+"; break;
                        }
                        stringBuilder.Append(symbol);
                        if (c < GameSettings.GridSize - 1)
                            stringBuilder.Append("|");
                    }
                    stringBuilder.Append("\n");
                }
            }
            return stringBuilder.ToString();
        }

        public int GetGridLength()
        {
            return this.grid.Length;
        }
    }
}
