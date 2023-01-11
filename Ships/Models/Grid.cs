using Ships.Utils;
using System.Text;

namespace Ships.Models
{
    public class Grid
    {
        private readonly string _columnHeaders;
        private readonly int _gridSize;
        private readonly GridTile[,] _tilesGrid;

        public Grid(int gridSize = 10)
        {
            _gridSize = gridSize;
            _columnHeaders =
                $"  |{string.Join('|', Alphabet.CharArray.Take(_gridSize).Select(c => c.ToString().ToUpper()))}|";
            _tilesGrid = new GridTile[_gridSize, _gridSize];

            for (var i = 0; i < _gridSize; i++)
            for (var j = 0; j < _gridSize; j++)
                _tilesGrid[i, j] = new GridTile();
        }

        public void MarkTile(Coordinates coordinates)
        {
            if (coordinates.Row >= _gridSize)
                throw new Exception("Invalid row selected, try again.");

            if (coordinates.Column >= _gridSize)
                throw new Exception("Invalid column selected, try again.");

            var tile = _tilesGrid[coordinates.Row, coordinates.Column];
            tile.Hit();
        }

        public void Draw()
        {
            Console.WriteLine("Board");
            Console.WriteLine(_columnHeaders);

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
                var isHorizontal = rand.NextDouble() >= 0.5;

                var startLimit = _gridSize - ship.Size + 1;
                var start = new Coordinates(rand.Next(0, !isHorizontal ? startLimit : _gridSize),
                    rand.Next(0, isHorizontal ? startLimit : _gridSize));

                var safeAreaCoordinates = GetSafeAreaCoordinates(start, ship.Size, isHorizontal);
                var safeAreaTiles = safeAreaCoordinates.Select(c => _tilesGrid[c.Row, c.Column]).ToList();

                if (safeAreaTiles.Any(t => t.HasShip))
                    continue;

                var shipTiles = isHorizontal ?
                    Enumerable.Range(start.Column, ship.Size).Select(t => _tilesGrid[start.Row, t]).ToList() :
                    Enumerable.Range(start.Row, ship.Size).Select(t => _tilesGrid[t, start.Column]).ToList();

                foreach (var tile in shipTiles)
                    tile.HasShip = true;

                ship.Tiles = shipTiles;
                return;
            }
        }

        private IList<Coordinates> GetSafeAreaCoordinates(Coordinates start, int size,
            bool isHorizontal)
        {
            var safeArea = new List<Coordinates>();
            var safeLength = Enumerable.Range((isHorizontal ? start.Column : start.Row) - 1, size + 2).ToList();

            var shipAxis = isHorizontal ? start.Row : start.Column;
            for (var i = shipAxis - 1; i <= shipAxis + 1; i++)
                safeArea.AddRange(isHorizontal ?
                    safeLength.Select(c => new Coordinates(i, c)) :
                    safeLength.Select(c => new Coordinates(c, i)));

            var trimmedSafeArea = safeArea
                .Where(c => c.Row >= 0 && c.Row < _gridSize && c.Column >= 0 && c.Column < _gridSize);

            return trimmedSafeArea.ToList();
        }
    }
}
