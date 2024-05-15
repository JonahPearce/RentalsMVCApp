using EntityLibrary.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public interface IPropertyManager
    {
        public List<Property>? GetProperties();

        public List<Property>? GetPropertiesByOwner(string ownerId);

        public List<Customer>? GetCustomers();

        public List<Booking>? GetBookings();

        public Property? GetPropertyById(int propertyId);

        public Customer? GetCustomerById(int customerId);

        public Customer? GetCustomerByName(string username);

        public Booking? GetBookingById(int bookingId);

        public void AddProperty(Property property);

        public void RemoveProperty(Property property);

        public void EditProperty(Property property);

        public void AddBooking(Booking booking);

        public void CancelBooking(Booking booking);

        public void ApproveBooking(Booking booking);

        public void DenyBooking(Booking booking);
    }
}
