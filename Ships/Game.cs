using System.Text.RegularExpressions;

namespace Ships
{
    public class Game
    {
        private const string INPUT_REGEX = "^[a-zA-z][0-9]$";
        private readonly Grid _grid;

        public Game()
        {
            _grid = new Grid();
            _grid.Draw();
        }

        public bool Play()
        {
            try
            {
                var input = GetInput();
            }
            catch (Exception e)
            {
                ReDraw();
                Console.WriteLine(e.Message);
                return true;
            }

            ReDraw();

            return true;
        }

        private string GetInput()
        {
            var input = Console.ReadLine();
            var regex = new Regex(INPUT_REGEX);
            if (input == null)
                throw new Exception("Invalid input, try again");

            var trimmedInput = input.Trim();
            var match = regex.Match(trimmedInput);
            if (!match.Success)
                throw new Exception("Invalid input, try again");

            return trimmedInput;
        }

        private void ReDraw()
        {
            Console.Clear();
            _grid.Draw();
        }
    }
}
