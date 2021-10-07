using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Context;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Areas.Organizer.ViewComponents
{
    public class RentGearViewComponent : ViewComponent
    {
        private ApplicationDbContext kontekst;

        public RentGearViewComponent(ApplicationDbContext _kontekst)
        {
            kontekst = _kontekst;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventId, string Mode)
        {

            if (Mode=="AllGear")
            {

                ViewData["RentGearIndex-key"] = new RentGearIndexVM
                {
                    Mode = Mode,
                    Rows = kontekst.Gear.Include(x => x.GearType).Select(x=> new RentGearIndexVM.Row
                    {
                        GearId = x.Id,
                        GearTypeId = x.GearType.Id,
                        Price = x.Price,
                        GearName = x.Name,
                        EventId = eventId
                    }).ToList()
                };

                return View();

            }
            else if (Mode=="MyCart")
            {
                ViewData["RentGearIndex-key"] = new RentGearIndexVM
                {
                    Mode = Mode,
                    Rows = kontekst.EventGear.Include(x => x.Gear).Where(x=> x.EventId == eventId && x.PurchaseId == null).Select(x => new RentGearIndexVM.Row
                    {
                        EventGearId = x.Id,
                        GearId = x.Gear.Id,
                        GearTypeId = x.Gear.GearType.Id,
                        Price = x.Gear.Price,
                        GearName = x.Gear.Name,
                        EventId = eventId
                    }).ToList()
                };

                return View();
            }
            else if(Mode=="Purchased")
            {
                ViewData["RentGearIndex-key"] = new RentGearIndexVM
                {
                    Mode = Mode,
                    Rows = kontekst.EventGear.Include(x => x.Gear).Where(x => x.EventId == eventId && x.PurchaseId != null).Select(x => new RentGearIndexVM.Row
                    {
                        EventGearId = x.Id,
                        GearId = x.Gear.Id,
                        GearTypeId = x.Gear.GearType.Id,
                        Price = x.Gear.Price,
                        GearName = x.Gear.Name,
                        EventId = eventId
                    }).ToList()
                };

                return View();
            }
            //
            /*else if (Mode == "DrawGear")
            {
                List<EventGear> eventGears = kontekst.EventGear.Where(a => a.EventId == eventId).ToList();

                List<GearImage> gearImages = new List<GearImage>();

                foreach (var x in eventGears)
                {
                    gearImages = gearImages.Concat(kontekst.GearImage.Include(g => g.Gear).Where(a => a.Gear.Id == x.GearId).ToList()).ToList();
                }

                ViewData["gearImages-key"] = gearImages;

                ViewData["RentGearIndex-key"] = new RentGearIndexVM
                {
                    Mode = Mode,
                    Rows = null
                };

                return View();
            }*/
            //
            else
            {
                return View();
            }

        }
    }
}
