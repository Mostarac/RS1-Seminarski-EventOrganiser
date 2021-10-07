using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapp.ViewModels;
using webapp.Context;
using webapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using webapp.Areas.Organizer.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace webapp.Controllers
{
    [Area("Organizer")]
    [Authorize]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _webHostEnvironment;

        public VenueController(ApplicationDbContext context, IHostingEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            var venues = _context.Venue
               .Include(x => x.VenueType)
               .Include(x => x.Address)
               .Where(x=> x.AppUserId == userID)
               .Select(x => new VenueIndexVM
               {
                   VenueID = x.Id,
                   Name = x.Name,
                   ImageUrl = x.ImageUrl,
                   Capacity = x.Capacity,
                   Venuetype = x.VenueType.Name,
                   StreetName = x.Address.Street,
                   CityName = x.Address.City.Name,
                   CountryName = x.Address.City.Country.Name
               }).ToList();


            ViewData["title"] = "My Venues";
            ViewData["_LayoutNavbarBG"] = "background-color: black;";
            ViewData["_layoutBackgroundImg-key"] = "../images/VenueIndexBackground.jpeg";
            ViewData["_layoutBackgroundStyle-key"] = "background-size: 100% 100%; background-attachment: fixed";
            ViewData["VenueIVM-key"] = venues;

            _context.Dispose();

            return View();
        }

        public IActionResult Add()
        {

            List<VenueType> venueTypes = _context.VenueType.ToList();
            List<Address> addresses = _context.Address.ToList();
            List<City> cities = _context.City.ToList();
            List<Country> countries = _context.Country.ToList();

            ViewData["title"] = "Add venue";
            ViewData["_LayoutNavbarBG"] = "background-color: black;";
            ViewData["_layoutBackgroundImg-key"] = "/images/VenueAddBackground.jpg";
            ViewData["_layoutBackgroundStyle-key"] = "background-size: 100% 100%; background-attachment: fixed";
            ViewData["venueTypes-key"] = venueTypes;
            ViewData["addresses-key"] = addresses;
            ViewData["cities-key"] = cities;
            ViewData["countries-key"] = countries;

            _context.Dispose();

            VenueViewModel vm = new VenueViewModel();

            return View(vm);
        }

        public IActionResult AddVenue(VenueViewModel venuevm)
        {

            var uniqueFileName = Image.Upload(venuevm.Picture, _webHostEnvironment, "venues");

            _context.Add(new Venue
            {
                Name = venuevm.Name,
                ImageUrl = uniqueFileName,
                Capacity = venuevm.Capacity,
                VenueTypeId = venuevm.VenueTypeID,
                AddressId = venuevm.StreetID,
                AppUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()
            });

            _context.SaveChanges();

            _context.Dispose();

            return RedirectToAction("Index", "Venue");

        }

        public IActionResult DeleteVenue(int id)
        {

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            Venue v = _context.Venue.Find(id);

            if(v==null)
            {
                return Content("Venue doesn't exist");
            }

            if(v.AppUserId != userID)
                return Content("You can delete only your own Venues.");

            _context.Remove(v);

            _context.SaveChanges();

            _context.Dispose();

            return RedirectToAction("Index", "Venue");
        }

        public IActionResult Edit(int Id)
        {

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            var id = Id;

            List<VenueType> venueTypes = _context.VenueType.ToList();
            List<Address> addresses = _context.Address.ToList();
            List<City> cities = _context.City.ToList();
            List<Country> countries = _context.Country.ToList();

            Venue novi = (_context.Venue.Where(k => k.Id == id).FirstOrDefault());

            if (novi.AppUserId != userID)
                return Content("You can edit only your own Venues!");

            TempData["editit-key"] = id;
            ViewData["novi-key"] = novi;
            ViewData["title"] = "Add venue";
            ViewData["_LayoutNavbarBG"] = "background-color: black;";
            ViewData["_layoutBackgroundImg-key"] = "../../images/VenueAddBackground.jpg";
            ViewData["_layoutBackgroundStyle-key"] = "background-size: 100% 100%; background-attachment: fixed";
            ViewData["venueTypes-key"] = venueTypes;
            ViewData["addresses-key"] = addresses;
            ViewData["cities-key"] = cities;
            ViewData["countries-key"] = countries;

            VenueViewModel vm = new VenueViewModel();

            return View(vm);

        }

        public IActionResult EditVenue(VenueViewModel venuevm)
        {

            string imageurl = "";

            if(venuevm.Picture == null)
            {
                imageurl = venuevm.ImageUrl;
            }
            else
            {
                imageurl = Image.Upload(venuevm.Picture, _webHostEnvironment, "venues");
            }

            Venue novi = new Venue
            {
                Id = (int)TempData["editit-key"],
                Name = venuevm.Name,
                ImageUrl = imageurl,
                Capacity = venuevm.Capacity,
                VenueTypeId = venuevm.VenueTypeID,
                AddressId = venuevm.StreetID,
                AppUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()
            };

            _context.Venue.Update(novi);

            _context.SaveChanges();

            _context.Dispose();

            return RedirectToAction("Index", "Venue");

        }

    }
}