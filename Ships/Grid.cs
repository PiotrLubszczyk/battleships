using System.Text;

namespace Ships
{
    public class Grid
    {
        private readonly char[] _chars =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'
        };

        private readonly GridTile[,] _tilesGrid;

        public Grid(int size = 10)
        {
            _tilesGrid = new GridTile[size, size];

            for (var i = 0; i < _tilesGrid.GetLength(0); i++)
            for (var j = 0; j < _tilesGrid.GetLength(1); j++)
                _tilesGrid[i, j] = new GridTile();
        }

        public void Draw()
        {
            var columnsCount = _tilesGrid.GetLength(0);
            var headers = $"  |{string.Join('|', _chars.Take(columnsCount).Select(c => c.ToString().ToUpper()))}|";

            Console.WriteLine("Board");
            Console.WriteLine(headers);

            for (var row = 0; row < columnsCount; row++)
            {
                var builder = new StringBuilder();
                var rowNr = row + 1 >= 10 ? $"{row + 1}" : $" {row + 1}";
                builder.Append($"{rowNr}|");
                var rowTiles = Enumerable.Range(0, _tilesGrid.GetLength(1)).Select(c => _tilesGrid[row, c].GetSymbol())
                    .ToArray();
                builder.Append(string.Join('|', rowTiles));
                builder.Append('|');
                Console.WriteLine(builder.ToString());
            }
        }
    }
}
