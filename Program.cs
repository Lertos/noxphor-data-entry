
using System.Drawing;

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
        HandleSharedKey(key);
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

void HandleSharedKey(char key)
{
    if (key == 'q' || key == 'Q')
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");

        Environment.Exit(0);
    }
    else if (key == 'b' || key == 'B')
    {
        //TODO: Add logic to go back a menu (save previous menu state)
    }
    else if (key == 'm' || key == 'M')
    {
        MainMenu();
    }
    else
    {
        errorMessage = "That is not a valid key input";
    }
}

void LocationMenu()
{
    while (true)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine(" Choose the action you would like to perform.:");
        Console.WriteLine("===============================================");
        Console.WriteLine();
        Console.WriteLine(" 1 - Add New Location");
        Console.WriteLine(" 2 - Edit Existing Location");
        Console.WriteLine(" 3 - Delete a Location");
        Console.WriteLine();
        Console.WriteLine(" b - Back to the Previous Menu");
        Console.WriteLine(" m - Back to the Main Menu");
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

        if (HandleLocationInput())
            break;
    }
}

bool HandleLocationInput()
{
    char key = Console.ReadKey(false).KeyChar;
    int digit;

    if (!Int32.TryParse(key.ToString(), out digit))
    {
        HandleSharedKey(key);
        return true;
    }

    switch (digit)
    {
        //Add new location
        case 1:
            AddNewLocation();
            break;
        //Edit location
        case 2:
            EditLocation();
            break;
        //Delete location
        case 3:
            DeleteLocation();
            break;
        default:
            errorMessage = "That is not a valid key input";
            return false;
    }

    return true;
}

void AddNewLocation()
{
    while (true)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine(" --LOCATION CREATION--");
        Console.WriteLine();
        Console.WriteLine(" < - Go back to the Previous Menu");
        Console.WriteLine("===============================================");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.Write(" ERROR: ");
            Console.WriteLine(errorMessage);
            Console.WriteLine("===============================================");

            errorMessage = "";
        }

        //ID
        Console.WriteLine("\n==Enter the location ID:");
        string? id = Console.ReadLine();

        if (string.IsNullOrEmpty(id))
        {
            errorMessage = "Location ID cannot be empty";
            continue;
        }
        else if (id == ">") break;

        //Display name
        Console.WriteLine("\n==Enter the location display name:");
        string? displayName = Console.ReadLine();

        if (string.IsNullOrEmpty(displayName))
        {
            errorMessage = "Location display name cannot be empty";
            continue;
        }
        else if (id == ">") break;

        //Description
        Console.WriteLine("\n==Enter the location description:");
        string? description = Console.ReadLine();

        if (string.IsNullOrEmpty(description))
        {
            errorMessage = "Location description cannot be empty";
            continue;
        }
        else if (id == ">") break;

        //Type
        Console.Write("\n==Enter the location type. Options are (|");

        foreach (string e in Enum.GetNames(typeof(Location.Type)))
        {
            Console.Write($" {e} |");
        }
        Console.WriteLine($")");

        string? type = Console.ReadLine();

        if (string.IsNullOrEmpty(type))
        {
            errorMessage = "Location type cannot be empty";
            continue;
        }
        else if (id == ">") break;

        Location.Type chosenType;
        bool isValidType = Enum.TryParse(type.ToUpper(), out chosenType);

        if (!isValidType )
        {
            errorMessage = "An invalid type was entered.";
            continue;
        }

        try
        {
            Location location = new Location(id, displayName, description, chosenType, [], []);
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
        }

        //TODO: Check if the location already exists. Also check what happens when you save and the ID exists; should replace.
        //TODO: Save the location to JSON

        break;
    }
}

void EditLocation()
{

}

void DeleteLocation()
{

}