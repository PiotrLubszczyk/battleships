namespace Ships.Models
{
    public class GridTile
    {
        public bool HasShip { get; set; }
        public bool IsHit { get; private set; }

        public char GetSymbol() => IsHit ? HasShip ? 'x' : 'o' : ' ';

        public void Hit()
        {
            if (IsHit)
                throw new Exception("Tile already hit.");

            IsHit = true;
        }
    }
}
