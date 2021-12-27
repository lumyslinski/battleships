using Battleships.Core.Ships;

namespace Battleships.Core.Grid
{
    public class GridLocation
    {
        public BaseShip Ship { get; private set; }
        public GridStates State { get; private set; }
        public bool IsThereShip { get { return this.Ship != null; } }

        public GridLocation()
        {
            this.State = GridStates.Empty;
        }

        public GridLocation(BaseShip ship)
        {
            this.Ship = ship;
            this.State = GridStates.Ship;
        }

        public void RemoveShip()
        {
            this.Ship = null;
            this.State = GridStates.Empty;
        }

        public void ShootShip()
        {
            if (this.IsThereShip)
            {
                this.Ship.DecreaseLife();
                this.State = GridStates.Hit;
            } 
            else
            {
                this.State = GridStates.Miss;
            }
        }

        public void PlaceShip(BaseShip ship)
        {
            this.Ship = ship;
            this.State = GridStates.Ship;
        }

        public override string ToString()
        {
            if(this.IsThereShip)
              return this.Ship.Name;
            else
              return State.ToString();
        }
    }
}
