using Ships.Utils;
using System.Text;

namespace Ships.Models
{
    public class Grid
    {
        private readonly int _gridSize;
        private readonly GridTile[,] _tilesGrid;

        public Grid(int gridSize = 10)
        {
            _gridSize = gridSize;
            _tilesGrid = new GridTile[_gridSize, _gridSize];

            for (var i = 0; i < _gridSize; i++)
            for (var j = 0; j < _gridSize; j++)
                _tilesGrid[i, j] = new GridTile();
        }

        public void MarkTile((int row, int column) coordinates)
        {
            if (coordinates.row >= _gridSize)
                throw new Exception("Invalid row selected, try again.");

            if (coordinates.column >= _gridSize)
                throw new Exception("Invalid column selected, try again.");

            var tile = _tilesGrid[coordinates.row, coordinates.column];
            if (tile.IsHit)
                throw new Exception("Tile already hit.");

            tile.IsHit = true;
        }

        public void Draw()
        {
            var headers =
                $"  |{string.Join('|', Alphabet.CharArray.Take(_gridSize).Select(c => c.ToString().ToUpper()))}|";

            Console.WriteLine("Board");
            Console.WriteLine(headers);

            for (var row = 0; row < _gridSize; row++)
            {
                var builder = new StringBuilder();
                var rowNr = row + 1 >= 10 ? $"{row + 1}" : $" {row + 1}";
                builder.Append($"{rowNr}|");
                var rowTiles = Enumerable.Range(0, _gridSize).Select(c => _tilesGrid[row, c].GetSymbol())
                    .ToList();
                builder.Append(string.Join('|', rowTiles));
                builder.Append('|');
                Console.WriteLine(builder.ToString());
            }
        }

        public void PlaceShip(Ship ship)
        {
            var rand = new Random();

            while (true)
            {
                var start = (row: rand.Next(0, _gridSize - ship.Size), column: rand.Next(0, _gridSize - ship.Size));
                var isHorizontal = rand.NextDouble() >= 0.5;

                var safeAreaCoordinates = GetSafeAreaCoordinates(start, ship.Size, isHorizontal);
                var safeAreaTiles = safeAreaCoordinates.Select(c => _tilesGrid[c.row, c.column]).ToList();

                if (safeAreaTiles.Any(t => t.HasShip))
                    continue;

                var shipTiles = isHorizontal ?
                    Enumerable.Range(start.column, ship.Size).Select(t => _tilesGrid[start.row, t]).ToList() :
                    Enumerable.Range(start.row, ship.Size).Select(t => _tilesGrid[t, start.column]).ToList();

                foreach (var tile in shipTiles)
                    tile.HasShip = true;

                ship.Tiles = shipTiles;
                return;
            }
        }

        private IList<(int row, int column)> GetSafeAreaCoordinates((int row, int column) start, int size,
            bool isHorizontal)
        {
            var adjacentCoordinates = new List<(int row, int column)>();
            var range = Enumerable.Range((isHorizontal ? start.column : start.row) - 1, size + 2).ToList();

            var mid = isHorizontal ? start.row : start.column;
            for (var i = mid - 1; i <= mid + 1; i++)
                adjacentCoordinates.AddRange(isHorizontal ?
                    range.Select(c => (row: i, column: c)) :
                    range.Select(c => (row: c, column: i)));

            return adjacentCoordinates
                .Where(c => c.row >= 0 && c.row < _gridSize && c.column >= 0 && c.column < _gridSize).ToList();
        }
    }
}
