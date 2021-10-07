using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapp.Context;
using webapp.Models;
using Microsoft.EntityFrameworkCore;
using webapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using webapp.Areas.Organizer.Helpers;

namespace webapp.Areas.Organizer.Controllers
{
    [Area("Organizer")]
    [Authorize]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class RentaGearController : Controller
    {
        private readonly ApplicationDbContext context;

        public RentaGearController(ApplicationDbContext _context)
        {
            context = _context;
        }
        
        public IActionResult Index(int eventId)
        {

            List<EventGear> eventGears = context.EventGear.Where(a => a.EventId == eventId).ToList();

            List<GearImage> gearImages = new List<GearImage>();

            foreach (var x in eventGears)
            {
                gearImages = gearImages.Concat(context.GearImage.Include(g => g.Gear).Where(a => a.Gear.Id == x.GearId).ToList()).ToList();
            }

            ViewData["gearImages-key"] = gearImages;

            ViewData["eventId-key"] = eventId;

            return PartialView();
        }

        public IActionResult AddGear(int eventId, int gearId)
        {
            EventGear eventGear = new EventGear
            {
                EventId = eventId,
                GearId = gearId,
                DateAdded = DateTime.Now
            };

            context.Add(eventGear);

            context.SaveChanges();

            //
            List<EventGear> eventGears = context.EventGear.Where(a => a.EventId == eventId).ToList();

            List<GearImage> gearImages = new List<GearImage>();

            foreach (var x in eventGears)
            {
                gearImages = gearImages.Concat(context.GearImage.Include(g => g.Gear).Where(a => a.Gear.Id == x.GearId).ToList()).ToList();
            }

            ViewData["gearImages-key"] = gearImages;
            //

            ViewData["eventId-key"] = eventId;

            return PartialView("Index", eventId);
        }

        public IActionResult RemoveGear(int eventId, int eventGearId)
        {
            EventGear EG = context.EventGear.Where(x => x.Id == eventGearId).SingleOrDefault();

            context.Remove(EG);

            context.SaveChanges();

            //
            List<EventGear> eventGears = context.EventGear.Where(a => a.EventId == eventId).ToList();

            List<GearImage> gearImages = new List<GearImage>();

            foreach (var x in eventGears)
            {
                gearImages = gearImages.Concat(context.GearImage.Include(g => g.Gear).Where(a => a.Gear.Id == x.GearId).ToList()).ToList();
            }

            ViewData["gearImages-key"] = gearImages;
            //


            ViewData["eventId-key"] = eventId;

            return PartialView("Index", eventId);
        }

        public IActionResult Rent(int eventId)
        {

            string result = "";

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            List<EventGear> eventGear = context.EventGear.Include(x => x.Gear).Where(x => x.EventId == eventId && x.PurchaseId == null).ToList();

            if(eventGear.Count == 0)
            {
                result = "Purchase not made, the cart is empty!";
            }
            else
            {
                float totalSum = 0;

                foreach (var x in eventGear)
                {
                    totalSum += x.Gear.Price;
                }

                Wallet wallet = context.Wallet.Where(x => x.AppUserId == userID).SingleOrDefault();

                float funds = wallet.Credits;
                float discount = wallet.Discount;
                float discountAmount = totalSum * (discount / 100);
                float toPay = totalSum - discountAmount;

                if (toPay > funds)
                {
                    result = "Not enough credits available for purchase. Please top up your wallet.";
                }
                else
                {
                    result = "You have successfully rented the gear!";
                    Purchase purchase = new Purchase
                    {
                        TotalPrice = toPay,
                        TotalDiscount = discountAmount,
                        PurchaseDate = DateTime.Now
                    };

                    context.Add(purchase);
                    context.SaveChanges();

                    int purchaseID = purchase.Id;

                    foreach(var x in eventGear)
                    {
                        x.PurchaseId = purchaseID;
                        context.Update(x);
                        context.SaveChanges();
                    }

                    wallet.Credits -= toPay;
                    float toPayTemp = toPay;
                    if (wallet.Discount < 30 && toPayTemp > 10) //Added discount points (max 30% discount) functionality
                    {
                        while(toPayTemp > 10) {
                            if(wallet.Discount < 30)
                            {
                                wallet.Discount += 1;
                            }
                            toPayTemp -= 10;
                        };
                    }
                    context.Update(wallet);
                    context.SaveChanges();
                }
            }

            ViewData["resultString-key"] = result;
            ViewData["eventId-key"] = eventId;

            return PartialView();
        }

    }
}