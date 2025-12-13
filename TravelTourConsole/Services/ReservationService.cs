using TravelTourConsole.Models;
using System.Collections.Generic;
using System;
using System.Linq; // برای متدهای Find و Count

namespace TravelTourConsole.Services
{
    public class ReservationService
    {
        // لیست ذخیره‌سازی رزروها
        private static readonly List<Reservation> _reservations;
        private static int _nextId = 1;

        // سازنده استاتیک برای مقداردهی اولیه لیست
        static ReservationService()
        {
            _reservations = new List<Reservation>();
        }

        // ------------------------------------------------------------------
        // === متدهای CRUD ===
        // ------------------------------------------------------------------

        // [Read] مشاهده تمام رزروها
        public List<Reservation> GetAllReservations()
        {
            return _reservations;
        }

        // [Create] افزودن رزرو جدید
        public void AddReservation(Reservation newReservation)
        {
            newReservation.Id = _nextId++;
            newReservation.BookingDate = DateTime.Now; // ثبت تاریخ رزرو
            _reservations.Add(newReservation);
        }

        // [Delete] حذف رزرو بر اساس ID
        public bool DeleteReservation(int id)
        {
            int countBefore = _reservations.Count;
            _reservations.RemoveAll(r => r.Id == id);
            return _reservations.Count < countBefore;
        }

        // [Update] به‌روزرسانی رزرو (فقط تعداد افراد یا نام مشتری)
        public bool UpdateReservation(Reservation updatedReservation)
        {
            var existingReservation = _reservations.Find(r => r.Id == updatedReservation.Id);

            if (existingReservation == null)
            {
                return false; 
            }

            // فقط فیلدهایی که کاربر منطقاً می‌تواند تغییر دهد را به‌روز می‌کنیم.
            existingReservation.NumberOfPeople = updatedReservation.NumberOfPeople;
            existingReservation.CustomerName = updatedReservation.CustomerName;
            
            return true;
        }
        
        // ------------------------------------------------------------------
        // === متد کمکی: محاسبه تعداد رزروهای یک تور خاص ===
        // ------------------------------------------------------------------

        public int GetTotalReservedPeople(int tourId)
        {
            // جمع زدن تعداد افراد برای همه رزروهایی که مربوط به tourId مشخص هستند
            return _reservations
                .Where(r => r.TourId == tourId)
                .Sum(r => r.NumberOfPeople);
        }
    }
}