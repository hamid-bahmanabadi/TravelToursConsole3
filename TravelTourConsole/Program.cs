
using TravelTourConsole.Services;

Start();

static void Start()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Menu");
        Console.WriteLine("1.Register");
        Console.WriteLine("2.Tour");
        Console.WriteLine("3.Reservation");
        Console.WriteLine("3.Exit");

        Console.Write("Please Choice: ");
        string choice = Console.ReadLine();

        if (choice == "4")
            return;

        if (choice == "1")
        {
            var registrationService = new RegistrationService();
            registrationService.Run();
        }
        else if (choice == "2")
        {

        }

    }
}