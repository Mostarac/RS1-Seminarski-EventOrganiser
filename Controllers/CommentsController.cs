using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using webapp.Models;
using webapp.Services.Interfaces;
using webapp.ViewModels;

namespace webapp.Controllers
{
    [Authorize(Roles = "User")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentsController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        public IActionResult Index(int eventId)
        {
            return PartialView(_commentService.GetEventComments(eventId));
        }

        [HttpPost]
        public IActionResult Create(CommentViewModel.Row commentVm)
        {
            var model = new Comment
            {
                Text = commentVm.Text,
                EventId = commentVm.EventId,
                CreatedDate = DateTime.Now,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _commentService.Create(model);
            return RedirectToAction("Index", new { eventId = commentVm.EventId });
        }
    }
}