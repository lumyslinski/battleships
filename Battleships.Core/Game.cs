using Battleships.Core.Grid;
using Battleships.Core.Interfaces;
using Battleships.Core.Ships;
using System;

namespace Battleships.Core
{
    public class Game : IDisposable
    {
        public BaseShip[] Ships { get; private set; }
        public IGridManager GridManager { get; private set; }
        public Game(IGridManager gridManager=null)
        {
            this.GridManager = gridManager;
        }

        public void Init()
        {
            if (this.GridManager != null) this.GridManager.Init();
            this.InitShips(GameSettings.DestroyersCount, GameSettings.BattleshipCount);
        }

        public void InitShips(int destroyersCount, int battleshipCount)
        {
            this.Ships = new BaseShip[GameSettings.BattleshipCount+GameSettings.DestroyersCount];
            this.InitShips(0,destroyersCount, typeof(Destroyer));
            this.InitShips(destroyersCount,battleshipCount, typeof(Battleship));
        }
        
        private void InitShips(int start, int count, Type type)
        {
            for (int i = start; i < (start+count); i++)
            {
                if (type.Equals(typeof(Destroyer)))
                    this.Ships[i] = new Destroyer(i);  
                if (type.Equals(typeof(Battleship)))
                    this.Ships[i] = new Battleship(i);
                if (this.GridManager != null) this.GridManager.PlaceShip(this.Ships[i]);
            }
        }

        public bool IsGameFinished()
        {
            bool isGameFinished = false;
            foreach (var ship in this.Ships)
            {
                isGameFinished = ship.IsSunk;
                if (!ship.IsSunk)
                    break;
            }
            return isGameFinished;
        }

        public ShootResponse Shoot(string target)
        {
            var response = new ShootResponse();
            var position = new ShipPosition(target);
            if (position.Row != GridRows.Undefined && position.Column != -1)
                response = ShootAtPosition(position);
            else
                response.SetError("Can not get target. Try again!");
            return response;
        }

        public ShootResponse ShootAtPosition(ShipPosition position)
        {
            var response = new ShootResponse();
            var place = this.GridManager.GetPlace(position);
            place.ShootShip();
            if (place.State == GridStates.Hit)
            {
                if (place.Ship.IsSunk)
                    response.SetStatus(place.State, string.Format("{0}, ship is {1} and is sinking now!", place.State, place.Ship.Name));
                else
                    response.SetStatus(place.State, string.Format("{0}, ship is {1}", place.State, place.Ship.Name));
            }
            else
                response.SetStatus(place.State);
            return response;
        }

        public string PrintStatus(bool isCheatModeEnabled)
        {
            return this.GridManager.GetTableFormat(isCheatModeEnabled);
        }
        
        public void Dispose()
        {
            this.Ships = null;
            if(this.GridManager != null)
            {
                this.GridManager.Dispose();
                this.GridManager = null;
            }
        }
    }
}
