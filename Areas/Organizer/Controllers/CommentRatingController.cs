using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Context;
using webapp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webapp.Models;

namespace webapp.Areas.Organizer.Controllers
{
    
    [ApiController]
    public class CommentRatingController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CommentRatingController(ApplicationDbContext context)
        {
            _context = context;

        }



        [Route("api/CommentRating/SaveCommentEdits")]
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Organizer")]
        public IActionResult SaveCommentEdits([FromBody] CommentRatingVM.Row x)
        {

            Comment comment = _context.Comment.Single(s => s.Id == x.id);
            comment.Text = x.Text;

            _context.Update(comment);
            _context.SaveChanges();
            return Ok();
        }

        //[HttpGet]
        [Route("api/[controller]")]
        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Organizer")]
        public IActionResult Index(string q, int currentPage = 1, int itemsPerPage = 10)
        {
            /*List<CommentRatingVM.Row> commentRatings = _context.Comment.Include(x=>x.Event).Include(x=>x.AppUser) 
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Where(s => q == null || (s.Event.Name).StartsWith(q))
                .Select(x => new CommentRatingVM.Row
                {
                    id = x.Id,
                    Text = x.Text,
                    //RatingId = _context.EventRating.Where(a=>a.EventId == x.EventId && a.UserId == x.UserId).Select(b=>b.Id).FirstOrDefault(),
                    Rating = _context.EventRating.Where(a=>a.EventId == x.EventId && a.UserId == x.UserId).Select(b=>b.Rate).FirstOrDefault(),
                    //UserId = x.UserId,
                    User = x.AppUser.UserName,
                    EventName = x.Event.Name,
                    //EventId = x.EventId
                })
                .ToList();

            CommentRatingVM cr = new CommentRatingVM();
            cr.commentRatings = commentRatings;
            cr.q = q;
            cr.Total = commentRatings.Count();*/

            IQueryable<Comment> queryable = _context.Comment
                .Where(s => q == null || (s.Event.Name).StartsWith(q));


            List<CommentRatingVM.Row> commentRatings = queryable
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new CommentRatingVM.Row
                {
                    id = x.Id,
                    Text = x.Text,
                    //RatingId = _context.EventRating.Where(a=>a.EventId == x.EventId && a.UserId == x.UserId).Select(b=>b.Id).FirstOrDefault(),
                    Rating = _context.EventRating.Where(a => a.EventId == x.EventId && a.UserId == x.UserId).Select(b => b.Rate).FirstOrDefault(),
                    //UserId = x.UserId,
                    User = x.AppUser.UserName,
                    EventName = x.Event.Name
                    //EventId = x.EventId
                })
                .ToList();

            CommentRatingVM cr = new CommentRatingVM { commentRatings = commentRatings, q = q, total = queryable.Count()};

            return Ok(JsonConvert.SerializeObject(cr));
        }

        [Route("api/CommentRating/Delete")]
        [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Organizer")]
        public IActionResult Delete (int CommentId)
        {

            Comment comment = _context.Comment.Single(s => s.Id == CommentId);

            _context.Remove(comment);
            _context.SaveChanges();

            return Ok();
        }

    }
}
