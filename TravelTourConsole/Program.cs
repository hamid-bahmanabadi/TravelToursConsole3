
using TravelTourConsole.Services;

Start();

static void Start()
{
    while (true)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.WriteLine("Menu");
        Console.WriteLine("1.Register Or Login");
        Console.WriteLine("2.Tour");
        Console.WriteLine("3.Reservation");
        Console.WriteLine("4.Exit");

        Console.Write("Please Choice: ");
        string choice = Console.ReadLine();

        if (choice == null)
        {
            continue;
        }

        if (choice == "1")
        {
            var registrationService = new Registration_Login_Service();
            registrationService.Run();
        }
        else if(choice == "4")
        {
            break;
        }
     

    }
}