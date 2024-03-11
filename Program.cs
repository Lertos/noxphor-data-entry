
string errorMessage = "";

MainMenu();

void MainMenu()
{
    while (true)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine(" Choose an object type you would like to edit:");
        Console.WriteLine("===============================================");
        Console.WriteLine();
        Console.WriteLine(" 1 - Locations");
        Console.WriteLine();
        Console.WriteLine(" q - Quit the application");
        Console.WriteLine();
        Console.WriteLine("===============================================");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.Write(" ");
            Console.WriteLine(errorMessage);
            Console.WriteLine("===============================================");
            
            errorMessage = "";
        }

        if (HandleInput())
            break;
    }
}

bool HandleInput()
{
    char key = Console.ReadKey(false).KeyChar;
    int digit;

    if (!Int32.TryParse(key.ToString(), out digit))
    {
        if (key == 'q' || key == 'Q')
        {
            Environment.Exit(0);
        }
        else
        {
            errorMessage = "That is not a valid key input";
            return true;
        }
    }

    switch (digit)
    {
        case 1:
            LocationMenu();
            break;
        default:
            errorMessage = "That is not a valid key input";
            return false;
    }

    return true;
}

void LocationMenu()
{

}