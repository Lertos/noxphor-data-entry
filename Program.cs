﻿
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
    bool error = true;
    
    while (error)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine(" Enter in the information as you are asked:");
        Console.WriteLine(" b - Cancel and go back to the Previous Menu");
        Console.WriteLine("===============================================");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.Write(" ERROR: ");
            Console.WriteLine(errorMessage);
            Console.WriteLine("===============================================");

            errorMessage = "";
        }

        //ID
        Console.WriteLine("\n==Enter in the location ID:");
        string id = Console.ReadLine();

        if (string.IsNullOrEmpty(id))
        {
            errorMessage = "Location ID cannot be empty";
            continue;
        }

        //Display name
        Console.WriteLine("\n==Enter in the location display name:");
        string displayName = Console.ReadLine();

        if (string.IsNullOrEmpty(displayName))
        {
            errorMessage = "Location display name cannot be empty";
            continue;
        }

        //Description
        Console.WriteLine("\n==Enter in the location description:");
        string description = Console.ReadLine();

        if (string.IsNullOrEmpty(description))
        {
            errorMessage = "Location description cannot be empty";
            continue;
        }

        //TODO: Check if the location already exists. Also check what happens when you save and the ID exists; should replace.

        error = false;
    }
}

void EditLocation()
{

}

void DeleteLocation()
{

}