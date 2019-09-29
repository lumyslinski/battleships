namespace Battleships.Core.Ships
{
    public class Destroyer : BaseShip
    {
        public Destroyer(int id) : base()
        {
            base.Set("Destroyer" + id, 4);
        }
    }
}
