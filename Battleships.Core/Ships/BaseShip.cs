using System.Collections.Generic;

namespace Battleships.Core.Ships
{
    public abstract class BaseShip
    {
        public string Name { get; protected set; }
        public string NameSymbol { get; protected set; }
        public int Squares { get; protected set; }
        public int Life { get; protected set; }
        public bool IsPlaced { get; private set; }
        public bool IsSunk { get { return this.Life <= 0; } }
        public List<ShipPosition> Positions { get; private set; }
        protected BaseShip()
        {
        }

        protected void Set(string name, int squares)
        {
            this.Name = name;
            this.NameSymbol = Name.Substring(0, 1);
            this.Squares = squares;
            this.Life = squares;
        }

        public void SetIsPlaced(bool state)
        {
            this.IsPlaced = state;
            if(!state)
                this.Positions = new List<ShipPosition>();
        }

        public void SetPosition(ShipPosition position)
        {
            if (this.Positions == null) this.Positions = new List<ShipPosition>();
            this.Positions.Add(position);
        }

        public void DecreaseLife()
        {
            this.Life--;
        }
    }
}
