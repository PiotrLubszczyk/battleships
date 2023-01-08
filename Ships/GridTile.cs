namespace Ships
{
    public class GridTile
    {
        private bool HasShip { get; set; }
        public bool IsHit { get; set; }

        public char GetSymbol() => IsHit ? HasShip ? 'x' : 'o' : ' ';
    }
}
