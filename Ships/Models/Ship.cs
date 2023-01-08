namespace Ships.Models
{
    public class Ship
    {
        public Ship(int size) => Size = size;

        public IList<GridTile> Tiles { get; set; } = new List<GridTile>();
        public int Size { get; }
        public bool IsDestroyed => Tiles.All(t => t.IsHit);
    }
}
