using TravelTourConsole.Services; 
using TravelTourConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq; // برای استفاده در متدهای رزرو

namespace TravelTourConsole
{
    // کلاس Program با ساختار کامل برای اجرای کنسول
    public class Program 
    {
        // سرویس تور که در همه متدهای استاتیک قابل دسترسی است
        private static readonly TourService _tourService = new TourService(); 
        
        // سرویس رزرو
        private static readonly ReservationService _reservationService = new ReservationService(); 
        
        // === متغیر جدید: سرویس ثبت نام/ورود (اضافه شده است) ===
        private static readonly Registration_Login_Service _registrationService = new Registration_Login_Service(); 
        // =====================================================

        // نقطه ورود اصلی برنامه (Main Entry Point)
        static void Main(string[] args)
        {
            // فراخوانی متد Start برای نمایش منوی اصلی
            Start();
        }

        static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("===================================");
                Console.WriteLine("      Travel & Tour Main Menu      ");
                Console.WriteLine("===================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Register Or Login");
                Console.WriteLine("2. Tour Management");
                Console.WriteLine("3. Reservation Management");
                Console.WriteLine("4. Exit");
                Console.Write("Please Choice: ");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // === فراخوانی سرویس ثبت نام/ورود (به روز رسانی شده) ===
                        _registrationService.Run(); 
                        break;
                    case "2":
                        ManageTourMenu();
                        break;
                    case "3":
                        ManageReservationMenu();
                        break;
                    case "4":
                        Console.WriteLine("\nExiting Application...");
                        return; // خروج از حلقه while و پایان برنامه
                    default:
                        Console.WriteLine("\nInvalid choice. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // =========================================================
        //                 متدهای مدیریت تور
        // =========================================================
        
        static void ManageTourMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== TOUR MANAGEMENT ===");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine("1. View All Tours");
                Console.WriteLine("2. Add New Tour");
                Console.WriteLine("3. Update Tour");     
                Console.WriteLine("4. Delete Tour");     
                Console.WriteLine("5. Back to Main Menu");
                
                Console.Write("Please Choice: ");
                string subChoice = Console.ReadLine();

                switch (subChoice)
                {
                    case "1":
                        ViewAllTours(); 
                        break;
                    case "2":
                        AddNewTour(); 
                        break;
                    case "3":                      
                        UpdateTourMenu();
                        break;
                    case "4":                      
                        DeleteTourMenu();
                        break;
                    case "5":                      
                        return; 
                    default:
                        Console.WriteLine("\nInvalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ViewAllTours()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- LIST OF ALL TOURS ---");
            Console.ForegroundColor = ConsoleColor.White;

            var tours = _tourService.GetAllTours(); 

            if (tours.Count == 0)
            {
                Console.WriteLine("No tours available yet.");
            }
            else
            {
                foreach (var tour in tours)
                {
                    // نمایش تعداد افراد رزرو شده و ظرفیت باقی مانده
                    int currentReserved = _reservationService.GetTotalReservedPeople(tour.Id);
                    int remainingCapacity = tour.Capacity - currentReserved;

                    Console.WriteLine($"- ID: {tour.Id} | Title: {tour.Title} | Capacity: {remainingCapacity}/{tour.Capacity} (Reserved: {currentReserved})");
                    Console.WriteLine($"  Dates: {tour.StartDate.ToShortDateString()} to {tour.EndDate.ToShortDateString()} | City: {tour.DestinationCity}");
                    Console.WriteLine("---------------------------------------------");
                }
            }

            Console.WriteLine("\nPress Enter to return to Tour Menu...");
            Console.ReadLine();
        }

        static void AddNewTour()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- ADD NEW TOUR ---");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                Console.Write("Enter Tour Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Capacity (e.g., 20): ");
                int capacity = int.Parse(Console.ReadLine()); 

                Console.Write("Enter Start Date (YYYY-MM-DD): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter End Date (YYYY-MM-DD): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Destination City: ");
                string destinationCity = Console.ReadLine();

                var newTour = new Models.Tour
                {
                    Title = title,
                    Capacity = capacity,
                    StartDate = startDate,
                    EndDate = endDate,
                    DestinationCity = destinationCity
                };

                _tourService.AddTour(newTour);
                Console.WriteLine($"\n[SUCCESS] Tour '{title}' added successfully.");
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Invalid input format. Please enter numbers for Capacity and correct date format (YYYY-MM-DD).");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
        
        static void DeleteTourMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=== DELETE TOUR ===");
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.Write("Enter the ID of the tour to delete: ");
            if (int.TryParse(Console.ReadLine(), out int idToDelete))
            {
                bool success = _tourService.DeleteTour(idToDelete);
                
                if (success)
                {
                    Console.WriteLine($"\n[SUCCESS] Tour with ID {idToDelete} has been successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"\n[ERROR] Tour with ID {idToDelete} not found or could not be deleted.");
                }
            }
            else
            {
                Console.WriteLine("\n[ERROR] Invalid ID format.");
            }
            
            Console.WriteLine("\nPress Enter to return to Tour Menu...");
            Console.ReadLine();
        }

        static void UpdateTourMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== UPDATE TOUR ===");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Enter the ID of the tour to update: ");
            if (!int.TryParse(Console.ReadLine(), out int idToUpdate))
            {
                Console.WriteLine("\n[ERROR] Invalid ID format.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("\nPlease enter NEW values for the tour:");
            ViewAllTours_Simple(); 

            try
            {
                Console.Write("Enter new Tour Title: ");
                string title = Console.ReadLine();
                
                Console.Write("Enter new Capacity (e.g., 20): ");
                int capacity = int.Parse(Console.ReadLine());

                Console.Write("Enter new Start Date (YYYY-MM-DD): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter new End Date (YYYY-MM-DD): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter new Destination City: ");
                string destinationCity = Console.ReadLine();

                var updatedTour = new Models.Tour
                {
                    Id = idToUpdate,
                    Title = title,
                    Capacity = capacity,
                    StartDate = startDate,
                    EndDate = endDate,
                    DestinationCity = destinationCity
                };

                bool success = _tourService.UpdateTour(updatedTour);

                if (success)
                {
                    Console.WriteLine($"\n[SUCCESS] Tour ID {idToUpdate} has been updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\n[ERROR] Tour ID {idToUpdate} not found or update failed.");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] Invalid data format entered for capacity or date.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR] An unexpected error occurred: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nPress Enter to return to Tour Menu...");
            Console.ReadLine();
        }

        static void ViewAllTours_Simple()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- Available Tours (IDs) ---");
            Console.ForegroundColor = ConsoleColor.White;

            var tours = _tourService.GetAllTours(); 
            foreach (var tour in tours)
            {
                int currentReserved = _reservationService.GetTotalReservedPeople(tour.Id);
                Console.WriteLine($"- ID: {tour.Id} | Title: {tour.Title} | Available: {tour.Capacity - currentReserved}");
            }
            Console.WriteLine("-----------------------------");
        }
        
        // =========================================================
        //                 متدهای مدیریت رزرو 
        // =========================================================

        static void ManageReservationMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("=== RESERVATION MANAGEMENT ===");
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine("1. View All Reservations");
                Console.WriteLine("2. Book A Tour (Add New Reservation)");
                Console.WriteLine("3. Update Reservation"); 
                Console.WriteLine("4. Cancel Reservation (Delete)"); 
                Console.WriteLine("5. Back to Main Menu");
                
                Console.Write("Please Choice: ");
                string subChoice = Console.ReadLine();

                switch (subChoice)
                {
                    case "1":
                        ViewAllReservations();
                        break;
                    case "2":
                        AddNewReservation();
                        break;
                    case "3":
                        UpdateReservationMenu();
                        break;
                    case "4":
                        DeleteReservationMenu();
                        break;
                    case "5":
                        return; // بازگشت به Start()
                    default:
                        Console.WriteLine("\nInvalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ViewAllReservations()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("--- LIST OF ALL RESERVATIONS ---");
            Console.ForegroundColor = ConsoleColor.White;

            var reservations = _reservationService.GetAllReservations();

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations available yet.");
            }
            else
            {
                foreach (var r in reservations)
                {
                    var bookedTour = _tourService.GetAllTours().Find(t => t.Id == r.TourId);
                    string tourTitle = bookedTour != null ? bookedTour.Title : "[Tour Not Found]";

                    Console.WriteLine($"- ID: {r.Id} | Tour: {tourTitle} (ID: {r.TourId})");
                    Console.WriteLine($"  Customer: {r.CustomerName} | People: {r.NumberOfPeople} | Date: {r.BookingDate.ToShortDateString()}");
                    Console.WriteLine("---------------------------------------------");
                }
            }

            Console.WriteLine("\nPress Enter to return to Reservation Menu...");
            Console.ReadLine();
        }

        static void AddNewReservation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- BOOK A NEW TOUR ---");
            Console.ForegroundColor = ConsoleColor.White;
            
            // نمایش تورهای موجود
            ViewAllTours_Simple(); 

            try
            {
                Console.Write("Enter Tour ID to book: ");
                int tourId = int.Parse(Console.ReadLine());
                
                // بررسی وجود تور
                var tour = _tourService.GetAllTours().Find(t => t.Id == tourId);
                if (tour == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[ERROR] Tour ID not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write($"Enter number of people (Max: {tour.Capacity}): ");
                int numberOfPeople = int.Parse(Console.ReadLine());

                // بررسی ظرفیت
                int currentReserved = _reservationService.GetTotalReservedPeople(tourId);
                if (currentReserved + numberOfPeople > tour.Capacity)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[ERROR] Capacity exceeded! Current reserved: {currentReserved}, Available: {tour.Capacity - currentReserved}.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Enter Customer Name: ");
                string customerName = Console.ReadLine();

                var newReservation = new Reservation
                {
                    TourId = tourId,
                    NumberOfPeople = numberOfPeople,
                    CustomerName = customerName
                };

                _reservationService.AddReservation(newReservation);
                Console.WriteLine($"\n[SUCCESS] Reservation for Tour ID {tourId} added successfully.");
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] Invalid input format. Please check your inputs (ID, People).");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR] An unexpected error occurred: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        static void UpdateReservationMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== UPDATE RESERVATION ===");
            Console.ForegroundColor = ConsoleColor.White;

            ViewAllReservations_Simple(); 

            Console.Write("Enter the ID of the reservation to update: ");
            if (!int.TryParse(Console.ReadLine(), out int idToUpdate))
            {
                Console.WriteLine("\n[ERROR] Invalid ID format.");
                Console.ReadLine();
                return;
            }

            var existingReservation = _reservationService.GetAllReservations().Find(r => r.Id == idToUpdate);
            if (existingReservation == null)
            {
                Console.WriteLine("\n[ERROR] Reservation ID not found.");
                Console.ReadLine();
                return;
            }

            try
            {
                Console.Write($"Enter new Number of People (Current: {existingReservation.NumberOfPeople}): ");
                int numberOfPeople = int.Parse(Console.ReadLine());

                Console.Write($"Enter new Customer Name (Current: {existingReservation.CustomerName}): ");
                string customerName = Console.ReadLine();

                var updatedReservation = new Reservation
                {
                    Id = idToUpdate,
                    TourId = existingReservation.TourId,
                    NumberOfPeople = numberOfPeople,
                    CustomerName = customerName,
                    BookingDate = existingReservation.BookingDate
                };

                bool success = _reservationService.UpdateReservation(updatedReservation);

                if (success)
                {
                    Console.WriteLine($"\n[SUCCESS] Reservation ID {idToUpdate} has been updated successfully.");
                }
                else
                {
                    Console.WriteLine($"\n[ERROR] Reservation ID {idToUpdate} update failed.");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] Invalid data format entered for Number of People.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nPress Enter to return to Reservation Menu...");
            Console.ReadLine();
        }

        static void DeleteReservationMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=== CANCEL RESERVATION ===");
            Console.ForegroundColor = ConsoleColor.White;

            ViewAllReservations_Simple(); 

            Console.Write("Enter the ID of the reservation to cancel: ");
            if (int.TryParse(Console.ReadLine(), out int idToDelete))
            {
                bool success = _reservationService.DeleteReservation(idToDelete);
                
                if (success)
                {
                    Console.WriteLine($"\n[SUCCESS] Reservation with ID {idToDelete} has been successfully cancelled.");
                }
                else
                {
                    Console.WriteLine($"\n[ERROR] Reservation with ID {idToDelete} not found or could not be cancelled.");
                }
            }
            else
            {
                Console.WriteLine("\n[ERROR] Invalid ID format.");
            }
            
            Console.WriteLine("\nPress Enter to return to Reservation Menu...");
            Console.ReadLine();
        }

        static void ViewAllReservations_Simple()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- Available Reservations (IDs) ---");
            Console.ForegroundColor = ConsoleColor.White;

            var reservations = _reservationService.GetAllReservations(); 
            foreach (var r in reservations)
            {
                var bookedTour = _tourService.GetAllTours().Find(t => t.Id == r.TourId);
                string tourTitle = bookedTour != null ? bookedTour.Title : "[Tour Not Found]";
                
                Console.WriteLine($"- ID: {r.Id} | Tour: {tourTitle} | Customer: {r.CustomerName}");
            }
            Console.WriteLine("------------------------------------");
        }
    }
}
