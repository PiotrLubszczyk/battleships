namespace Ships
{
    public class GridTile
    {
        public bool HasShip { get; set; }
        public bool IsHit { get; set; }

        // public char GetSymbol() => IsHit ? HasShip ? 'x' : 'o' : ' ';
        public char GetSymbol()
        {
            if (HasShip) return 'x';
            return IsHit ? HasShip ? 'x' : 'o' : ' ';
        }
    }
}
