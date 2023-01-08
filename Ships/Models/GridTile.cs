namespace Ships.Models
{
    public class GridTile
    {
        public bool HasShip { get; set; }
        public bool IsHit { get; set; }

        public char GetSymbol() => IsHit ? HasShip ? 'x' : 'o' : ' ';
    }
}
