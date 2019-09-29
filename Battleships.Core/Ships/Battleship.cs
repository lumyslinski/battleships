namespace Battleships.Core.Ships
{
    public class Battleship: BaseShip
    {
        public Battleship(int id)
        {
            base.Set("Battleship" + id, 5);
        }
    }
}
