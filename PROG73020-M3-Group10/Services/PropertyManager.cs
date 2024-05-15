using EntityLibrary.Entities;
using LogicLibrary.Services;
using PROG73020_M3_Group10.Models;

namespace PROG73020_M3_Group10.Services
{
    public class PropertyManager : IPropertyManager
    {
        private readonly PropertyDBContext _propertyDBContext;

        public PropertyManager(PropertyDBContext propertyDBContext)
        {
            _propertyDBContext = propertyDBContext;
        }

        public void AddBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void AddProperty(Property property)
        {
            throw new NotImplementedException();
        }

        public void ApproveBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void CancelBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void DenyBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void EditProperty(Property property)
        {
            throw new NotImplementedException();
        }

        public Booking? GetBookingById(int bookingId)
        {
            throw new NotImplementedException();
        }

        public List<Booking>? GetBookings()
        {
            throw new NotImplementedException();
        }

        public Customer? GetCustomerById(int customerId)
        {
            string customerIdString = customerId.ToString();
            return _propertyDBContext.Customers.FirstOrDefault(i => i.Id == customerIdString);
        }

        public Customer? GetCustomerByName(string username)
        {
            return _propertyDBContext.Customers.FirstOrDefault(i => i.UserName == username);
        }

        public List<Customer>? GetCustomers()
        {
            throw new NotImplementedException();
        }

        public List<Property>? GetProperties()
        {
            return _propertyDBContext.Properties.OrderBy(p => p.Address).ToList();
        }

        public List<Property>? GetPropertiesByOwner(string ownerId)
        {
            // If we fail to parse the Id, we just return an empty list.
            if (!int.TryParse(ownerId, out int ownerIdInt))
            {
                return new List<Property>();
            }
            return _propertyDBContext.Properties.Where(p => p.OwnerID == ownerIdInt).OrderBy(p => p.Address).ToList();
        }

        public Property? GetPropertyById(int propertyId)
        {
            return _propertyDBContext.Properties.FirstOrDefault(i => i.PropertyId == propertyId);
        }

        public void RemoveProperty(Property property)
        {
            throw new NotImplementedException();
        }
    }
}
