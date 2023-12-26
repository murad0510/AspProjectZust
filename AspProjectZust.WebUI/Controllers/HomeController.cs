using AspProjectZust.Business.Abstract;
using AspProjectZust.Entities.Entity;
using AspProjectZust.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspProjectZust.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public UserManager<CustomIdentityUser> _userManager;
        public readonly IUserService _userService;
        public CustomIdentityDbContext _dbContext;

        public HomeController(UserManager<CustomIdentityUser> userManager, IUserService userService, CustomIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _userService = userService;
            _dbContext = dbContext;
        }

        public IActionResult Birthday()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Favorite()
        {
            return View();
        }


        public IActionResult Friends()
        {
            return View();
        }

        public IActionResult HelpAndSupport()
        {
            return View();
        }

        public IActionResult LiveChat()
        {
            return View();
        }

        public IActionResult MarketPlace()
        {
            return View();
        }

        public IActionResult Messages()
        {
            return View();
        }


        public IActionResult MyProfile()
        {
            //UpdateUserViewModel userInfo = new UpdateUserViewModel();
            return View();
        }

        //[HttpPost]
        //public IActionResult MyProfile(UpdateUserViewModel user)
        //{
        //    //UpdateUserViewModel userInfo = new UpdateUserViewModel();
        //    return View();
        //}

        public async Task<IActionResult> NewsFeed()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Setting()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult Weather()
        {
            return View();
        }

        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var data = await _dbContext.Users.Where(i => i.Id != user.Id).ToListAsync();
            return Ok(data);
        }


        public async Task<IActionResult> SendFollow(string id)
        {
            var senderUser = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = _userManager.Users.FirstOrDefault(i => i.Id == id);

            if (receiverUser != null)
            {
                var request = new FriendRequest
                {
                    Content = $"{senderUser.UserName} send friend request at {DateTime.Now.ToLongDateString()}",
                    SenderId = senderUser.Id,
                    Sender = senderUser,
                    ReceiverId = id,
                    Status = "Request"
                };
                _dbContext.FriendRequests.Add(request);
                await _dbContext.SaveChangesAsync();
                await _userManager.UpdateAsync(receiverUser);
                return Ok();
            }
            return BadRequest();
        }

        //public async Task<IActionResult> GetAllRequests()
        //{
        //    var current = await _userManager.GetUserAsync(HttpContext.User);
        //    var requests = _dbContext.FriendRequests.Where(r => r.ReceiverId == current.Id).ToList();
        //    //var allUsers = _dbContext.Users.ToList();

        //    //for (int i = 0; i < requests.Count; i++)
        //    //{
        //    //    for (int k = 0; k < allUsers.Count; k++)
        //    //    {
        //    //        if (requests[i].SenderId == allUsers[k].Id)
        //    //        {
        //    //            requests[i].Sender = allUsers[k];
        //    //        }
        //    //    }
        //    //}
        //    return Ok(requests);
        //}

        public async Task<IActionResult> GetAllRequests()
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var data = _dbContext.FriendRequests.Where(r => r.ReceiverId == current.Id);
            return Ok(data);
        }


        //[HttpPost]
        //public IActionResult UserChangeProfile(UpdateUserViewModel user)
        //{
        //    return Ok(user);
        //}
    }
}

