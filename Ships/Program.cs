using Ships;

var game = new Game();

while (true)
{
    if (game.IsFinished)
    {
        Console.WriteLine("You won! Want to play again? Press y/n to select");
        var key = Console.ReadKey();
        if (key.KeyChar == 'n')
            return;
        if (key.KeyChar == 'y')
            game = new Game();
    }

    game.Play();
}
