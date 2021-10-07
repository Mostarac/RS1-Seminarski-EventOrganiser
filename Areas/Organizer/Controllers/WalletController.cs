using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp.Areas.Organizer.Helpers;
using webapp.Context;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Areas.Organizer.Controllers
{
    [Area("Organizer")]
    [Authorize]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class WalletController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalletController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            if (_context.Wallet.Where(k => k.AppUserId == userID).FirstOrDefault()==null)
            {
                _context.Add(new Wallet { AppUserId = userID, Credits = 0, Discount = 0 });

                _context.SaveChanges();
            }

            ViewData["_LayoutNavbarBG"] = "background-color: black;";
            ViewData["_layoutBackgroundImg-key"] = "/images/WalletBackground.jpg";
            ViewData["_layoutBackgroundStyle-key"] = "background-size: 100% 100%; background-attachment: fixed";
            ViewData["EOCreditsAmount-key"] = _context.Wallet.Where(k => k.AppUserId == userID).FirstOrDefault().Credits;

            return View();
        }

        public IActionResult Funds()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            ViewData["EOCreditsAmountF-key"] = (float)_context.Wallet.Where(k => k.AppUserId == userID).FirstOrDefault().Credits;
            return PartialView();
        }

        /*public IActionResult TopUp(string Code)
        {

            TempData["TopUpCounter-key"] = 0;

            if (Code == null)
                return PartialView();
            else
            {
                var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

                TopUpCard TUC = _context.TopUpCard.Where(x => x.Code == Code).FirstOrDefault();

                TopUp TU = new TopUp
                {
                    TopUpCardId = TUC.Id,
                    AppUserId = userID,
                    TopUpDate = DateTime.Now
                };

                Wallet WLT = _context.Wallet.Where(k => k.AppUserId == userID).FirstOrDefault();

                WLT.Credits += (float)TUC.Amount;

                _context.Update(WLT);

                _context.Add(TU);

                _context.SaveChanges();

                //ViewData["Message-key"] = "Successfully added!";
                //return Content("Successfully added!");

                return PartialView();
            }

            //return PartialView();
        }*/

        [HttpGet]
        public IActionResult TopUp()
        {
            WalletVm vm = new WalletVm();
            return PartialView(vm);
        }

        [HttpPost]
        public IActionResult TopUp(WalletVm vm)
        {

                var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

                TopUpCard TUC = _context.TopUpCard.Where(x => x.Code == vm.CardCode).FirstOrDefault();

            if(TUC==null)
            {
                return Content("Top up card not found!");
            }

            if (_context.TopUp.Include(x=>x.TopUpCard).Any(x => x.TopUpCard.Code == vm.CardCode))
            {
                return Content("Top up card already used!");
            }

            TopUp TU = new TopUp
                {
                    TopUpCardId = TUC.Id,
                    AppUserId = userID,
                    TopUpDate = DateTime.Now
                };

                Wallet WLT = _context.Wallet.Where(k => k.AppUserId == userID).FirstOrDefault();

                WLT.Credits += (float)TUC.Amount;

                _context.Update(WLT);

                _context.Add(TU);

                _context.SaveChanges();

            //ViewData["Message-key"] = "Successfully added!";
            //return Content("Successfully added!");

            return PartialView();

        }  
        
        public IActionResult TopUpIndex ()
        {
            var userID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

            List<TopUp> topUps = _context.TopUp.Include(x => x.TopUpCard).Where(x => x.AppUserId == userID).ToList();

            ViewData["topUps-key"] = topUps;

            return PartialView();
        }

    }
}