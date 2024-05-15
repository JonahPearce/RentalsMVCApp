using EntityLibrary.Entities;
using LogicLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG73020_M3_Group10.Models;
using PROG73020_M3_Group10.Services;

namespace PROG73020_M3_Group10.Controllers
{
    public class PropertyController : Controller
    {
        public PropertyController(IPropertyManager propertyManager, PropertyDBContext propertyDBContext)
        {
            _propertyManager = propertyManager;
            _propertyContext = propertyDBContext;
        }

        // Default Route for properties
        [HttpGet("/properties")]
        public IActionResult GetProperties()
        {
            PropertiesViewModel model = new PropertiesViewModel()
            {
                AllProperties = _propertyManager.GetProperties()
            };
            return View(model);
        }


        // Default Route for properties with a Search
        [HttpPost("/properties")]
        public IActionResult GetProperties(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.SearchTerm = search;
            }

            PropertiesViewModel model = new PropertiesViewModel
            {
                AllProperties = _propertyManager.GetProperties()
            };

            return View(model);
        }

        // Route for viewing the specific details of a listing (Remember Owners cannot edit from here, they edit from My Listings.)
        [HttpGet("/properties/{id}")]
        public IActionResult PropertyDetails(int id)
        {

            Property property = _propertyManager.GetPropertyById(id);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        [Authorize]
        // Default Route for properties
        [HttpGet("/myproperties/")]
        public IActionResult GetMyListings()
        {
            Customer customer = _propertyManager.GetCustomerByName(User.Identity.Name);

            PropertiesViewModel model = new PropertiesViewModel()
            {
                AllProperties = _propertyManager.GetPropertiesByOwner(customer.Id)
            };
            return View(model);
        }

        private IPropertyManager _propertyManager;
        private PropertyDBContext _propertyContext;
    }
}
