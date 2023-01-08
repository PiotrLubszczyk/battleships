using Ships.Models;
using Ships.Utils;
using System.Text.RegularExpressions;

namespace Ships
{
    public class Game
    {
        private const string INPUT_REGEX = "^[a-zA-Z][0-9]{1,2}$";

        private readonly Grid _grid;
        private readonly Ship[] _ships = { new(4), new(4), new(5) };

        public Game()
        {
            _grid = new Grid();

            foreach (var ship in _ships)
                _grid.PlaceShip(ship);

            Draw();
        }

        public bool IsFinished => _ships.All(s => s.IsDestroyed);

        public void Play()
        {
            try
            {
                _grid.MarkTile(GetInput());
            }
            catch (Exception e)
            {
                Draw();
                Console.WriteLine(e.Message);
                return;
            }

            Draw();
        }

        private (int row, int column) GetInput()
        {
            var input = Console.ReadLine();
            var regex = new Regex(INPUT_REGEX);
            if (input == null)
                throw new Exception("Invalid input, try again");

            var trimmed = input.Trim().ToLower();
            var match = regex.Match(trimmed);
            if (!match.Success)
                throw new Exception("Invalid input, try again");

            var inputNumbers = string.Join(string.Empty, trimmed.Skip(1).Take(trimmed.Length - 1));
            var row = int.Parse(inputNumbers) - 1;
            var column = Array.IndexOf(Alphabet.CharArray, trimmed.First());

            return (row, column);
        }

        private void Draw()
        {
            Console.Clear();

            Console.WriteLine("Ships");
            var groupedShips = _ships.GroupBy(s => s.Size).OrderBy(g => g.Key);
            Console.WriteLine(string.Join(", ", groupedShips.Select(g => $"{g.Count(s => !s.IsDestroyed)}x{g.Key}")));

            _grid.Draw();
        }
    }
}
