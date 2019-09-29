using Battleships.Core.Grid;
using System;

namespace Battleships.Core.Ships
{
    public class ShipPosition
    {
        public GridRows Row { get; set; }
        public int Column { get; set; }

        public ShipPosition(string position)
        {
            this.Row = GridRows.Undefined;
            this.Column = -1;
            if (!String.IsNullOrEmpty(position) && (position.Length == 2 || position.Length == 3))
            {
                GridRows row = GridRows.A;
                Enum.TryParse(position.Substring(0, 1), out row);
                this.Row = row;
                int column = 0;
                int.TryParse(position.Substring(1, position.Length - 1), out column);
                this.Column = column - 1;
            }
        }

        public ShipPosition(int row, int column)
        {
            this.Row = (GridRows)row;
            this.Column = column;
        }
    }
}
