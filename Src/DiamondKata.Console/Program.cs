// See https://aka.ms/new-console-template for more information
using DiamondKata;

Console.WriteLine($"Diamond Kata in C#{Environment.NewLine}");

var quit = false;
var generator = new TextDiamondGenerator();
while (!quit)
{
    Console.WriteLine("Enter a letter A-Z or q to quit.");
    var input = Console.ReadLine();
    if(null != input && input.Length == 1)
    {
        if (input[0] != 'q')
        {
            var result = generator.GenerateDiamond(input[0]);
            if (result.IsSuccess)
            {
                Console.WriteLine($"{Environment.NewLine}Diamond for {input[0]}:{Environment.NewLine}");
                foreach (var line in result.Value)
                {
                    Console.WriteLine($"{line}");
                }
                Console.WriteLine();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }
        else
        {
            quit = true;
        }
    }
    else
    {
        Console.WriteLine("Please only enter a single, uppercase letter.");
    }
}
