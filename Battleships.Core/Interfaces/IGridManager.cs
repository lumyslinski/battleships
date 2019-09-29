using Battleships.Core.Grid;
using Battleships.Core.Ships;

namespace Battleships.Core.Interfaces
{
    public interface IGridManager
    {
        void Dispose();
        int FindRowAvailableForThisColumn(int column);
        GridLocation GetPlace(ShipPosition position);
        string GetTableFormat(bool isCheatModeEnabled);
        void Init();
        void InitGrid(int columns, int rows);
        void PlaceShip(BaseShip ship);
        void PlaceShip(BaseShip ship, int rowIndex, int colIndex, ShipPlacementMode mode = ShipPlacementMode.All);
    }
}