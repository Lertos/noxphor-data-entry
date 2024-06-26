﻿
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

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
        {
            //break; //TODO: Depending on how states are implemented, uncomment this
        }
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
        MainMenu(); //TODO: Delete this later. Just here for now for quick debugging
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
        Console.WriteLine(" 2 - View Locations (Edit / Delete)");
        Console.WriteLine();
        Console.WriteLine(" b - Back to the previous menu");
        Console.WriteLine(" m - Back to the main menu");
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
        case 1:
            LocationCreator();
            break;
        case 2:
            ShowAllLocations();
            break;
        default:
            errorMessage = "That is not a valid key input";
            return false;
    }

    return true;
}

// Reused for both adding and editing locations
void LocationCreator(bool isNewLocation = true, string existingID = "")
{
    while (true)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine((isNewLocation) ? " --LOCATION CREATION--" : " --LOCATION EDITOR--");
        Console.WriteLine();
        Console.WriteLine(" :q - Go back to the previous menu");
        Console.WriteLine("===============================================");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.Write(" ERROR: ");
            Console.WriteLine(errorMessage);
            Console.WriteLine("===============================================");

            errorMessage = "";
        }

        //ID
        string? id = existingID;

        if (isNewLocation || existingID == "")
        {
            Console.WriteLine("\n==Enter the location ID:");
            id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                errorMessage = "Location ID cannot be empty";
                continue;
            }
            else if (id == ":q") break;
        }
        else
        {
            Console.Write("You are editing the location ID: ");
            Console.WriteLine(id);
        }

        //Display name
        Console.WriteLine("\n==Enter the location display name:");
        string? displayName = Console.ReadLine();

        if (string.IsNullOrEmpty(displayName))
        {
            errorMessage = "Location display name cannot be empty";
            continue;
        }
        else if (displayName == ":q") break;

        //Description
        Console.WriteLine("\n==Enter the location description:");
        string? description = Console.ReadLine();

        if (string.IsNullOrEmpty(description))
        {
            errorMessage = "Location description cannot be empty";
            continue;
        }
        else if (description == ":q") break;

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
        else if (type == ":q") break;

        Location.Type chosenType;
        bool isValidType = Enum.TryParse(type.ToUpper(), out chosenType);

        if (!isValidType )
        {
            errorMessage = "An invalid type was entered.";
            continue;
        }

        try
        {
            Location location = new Location(id, displayName, description, chosenType);

            SaveLocation(location);
        }
        catch (Exception e)
        {
            errorMessage = "Could not create the location. Please check the details and try again. Error: \n" + e.Message;
            return;
        }

        break;
    }
}

void ShowAllLocations()
{
    List<Location> locations = LoadLocations();

    //Test data - delete later
    /*
    List<Location> locations = new();
    for (int i = 0; i < 11; i++)
    {
        Location location = new Location("loc" +  i, "Loc" + i, "This is the loc " + i, Location.Type.BASE);
        locations.Add(location);
    }
    */

    int currentIndex = 0;

    while(true)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine(" --LOCATION VIEWER--");
        Console.WriteLine();
        Console.WriteLine(" HELP: Use the LEFT and RIGHT arrow keys to navigate.");
        Console.WriteLine();
        Console.WriteLine(" 1-9 : Select a location");
        Console.WriteLine("  b  : Go back to the previous menu");
        Console.WriteLine("===============================================");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.Write(" ERROR: ");
            Console.WriteLine(errorMessage);
            Console.WriteLine("===============================================");

            errorMessage = "";
        }

        ShowLocationPage(locations, currentIndex);

        ConsoleKeyInfo cki = Console.ReadKey(false);
        ConsoleKey ck = cki.Key;
        char key = cki.KeyChar;

        int digit;

        if (!Int32.TryParse(key.ToString(), out digit))
        {
            if (ck == ConsoleKey.LeftArrow)
            {
                currentIndex = Math.Max(currentIndex - 9, 0);
                continue;
            }
            else if (ck == ConsoleKey.RightArrow)
            {
                currentIndex = Math.Min(currentIndex + 9, locations.Count);
                continue;
            }
            else if (ck == ConsoleKey.B)
            { 
                break;
            }
        }
        else if (digit > 0 && digit < 10)
        {
            int chosenIndex = currentIndex + (digit - 1);

            if (chosenIndex < 0 || chosenIndex >= locations.Count)
            {
                errorMessage = "That is not a valid location index.";
            }
            else
            {
                ShowSpecificLocation(locations, chosenIndex);
                break;
            }
        }

        errorMessage = "That was not a valid key.";
    }
}

void ShowSpecificLocation(List<Location> locations, int index)
{
    Location location = locations[index];

    if (location == null)
        return;

    while (true)
    {
        Console.Clear();

        Console.WriteLine("===============================================");
        Console.WriteLine(" --LOCATION OPERATIONS--");
        Console.WriteLine();
        Console.WriteLine(" e : Edit this location");
        Console.WriteLine(" d : Delete this location");
        Console.WriteLine(" b : Go back to the previous menu");
        Console.WriteLine("===============================================");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.Write(" ERROR: ");
            Console.WriteLine(errorMessage);
            Console.WriteLine("===============================================");

            errorMessage = "";
        }

        //Show all of the location information
        Console.WriteLine();
        Console.Write(" ID: ");
        Console.WriteLine(location.id);
        Console.Write(" Name: ");
        Console.WriteLine(location.name);
        Console.Write(" Type: ");
        Console.WriteLine(location.locationType);
        Console.Write(" Description: ");
        Console.WriteLine(location.description);

        ConsoleKey ck = Console.ReadKey(false).Key;

        if (ck == ConsoleKey.E)
        {
            LocationCreator(false, location.id);
            break;
        }
        else if (ck == ConsoleKey.D)
        {
            DeleteLocation(location.id);

            locations.RemoveAt(index);
            break;
        }
        else if (ck == ConsoleKey.B)
        {
            break;
        }

        errorMessage = "That was not a valid key.";
    }
}

void ShowLocationPage(List<Location> locations, int currentIndex)
{
    int counter = 0;

    if (currentIndex == locations.Count)
    {
        Console.WriteLine(" --End of Locations--");
        return;
    }

    for (int i = currentIndex; i < Math.Min(currentIndex + 9, locations.Count); i++)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" ");
        sb.Append(counter + 1);
        sb.Append(" - ");
        sb.Append(locations[i].id);
        sb.Append(" : ");
        sb.Append(locations[i].name);
        sb.Append(" | ");
        sb.Append(locations[i].description.Substring(0, Math.Min(locations[i].description.Length, 20)));

        Console.WriteLine(sb.ToString());

        counter++;
    }
}

void DeleteLocation(string locationID)
{
    ObjectStorage storage = new ObjectStorage(GameData.FILE_NAME_LOCATIONS);

    storage.DeleteObject<Location>(locationID);
}

void SaveLocation(Location location)
{
    ObjectStorage storage = new ObjectStorage(GameData.FILE_NAME_LOCATIONS);

    storage.StoreObject(location, location.id);
}

List<Location> LoadLocations()
{
    ObjectStorage storage = new ObjectStorage(GameData.FILE_NAME_LOCATIONS);

    Dictionary<string, Location> dict = storage.LoadObjects<Location>();
    
    if (dict == null || dict.Count == 0)
        return [];

    List<Location> locations = dict.Values.ToList();

    return locations;
}