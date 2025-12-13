using TravelTourConsole.Models;
using System.Collections.Generic;
using System;
using System.Linq; // این using برای متد UpdateTour اضافه شده است

namespace TravelTourConsole.Services
{
    public class TourService
    {
        private static readonly List<Models.Tour> _tours;
        private static int _nextId = 1; 

        static TourService()
        {
            _tours = new List<Models.Tour>()
            {
                new Models.Tour { Id = _nextId++, Title = "تور ۳ روزه شیراز", Capacity = 20, StartDate = new DateTime(2025, 12, 25), EndDate = new DateTime(2025, 12, 28), DestinationCity = "Shiraz" },
                new Models.Tour { Id = _nextId++, Title = "سفر ۵ روزه آنتالیا", Capacity = 50, StartDate = new DateTime(2026, 01, 10), EndDate = new DateTime(2026, 01, 15), DestinationCity = "Antalya" }
            };
        }

        public List<Models.Tour> GetAllTours()
        {
            return _tours;
        }

        public void AddTour(Models.Tour newTour)
        {
            newTour.Id = _nextId++;
            _tours.Add(newTour);
            // Console.WriteLine($"\n[SUCCESS] Tour '{newTour.Title}' added with ID: {newTour.Id}");
        }

        // ------------------------------------------------------------------
        // === متد جدید: به‌روزرسانی (Update) ===

        public bool UpdateTour(Models.Tour updatedTour)
        {
            // پیدا کردن تور قدیمی با استفاده از ID
            var existingTour = _tours.Find(t => t.Id == updatedTour.Id);

            if (existingTour == null)
            {
                return false; // تور پیدا نشد
            }

            // به‌روزرسانی تمام ویژگی‌ها
            existingTour.Title = updatedTour.Title;
            existingTour.Capacity = updatedTour.Capacity;
            existingTour.StartDate = updatedTour.StartDate;
            existingTour.EndDate = updatedTour.EndDate;
            existingTour.DestinationCity = updatedTour.DestinationCity;

            return true; // به‌روزرسانی موفق
        }
        
        // ------------------------------------------------------------------
        // === متد جدید: حذف (Delete) ===

        public bool DeleteTour(int id)
        {
            // حذف تور بر اساس ID و برگرداندن true/false بسته به موفقیت حذف
            int countBefore = _tours.Count;
            _tours.RemoveAll(t => t.Id == id);
            
            // اگر تعداد تورها کم شده باشد، یعنی حذف موفقیت‌آمیز بوده است.
            return _tours.Count < countBefore; 
        }
    }
}