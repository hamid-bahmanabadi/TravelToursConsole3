using System;

namespace TravelTourConsole.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int TourId { get; set; }        // ID توری که رزرو شده است
        public int NumberOfPeople { get; set; } // تعداد افراد رزرو شده
        public string CustomerName { get; set; } // نام مشتری (موقت، تا زمانی که بخش Login پیاده شود)
        public DateTime BookingDate { get; set; } // تاریخ ثبت رزرو
    }
}