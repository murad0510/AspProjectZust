using AspProjectZust.Business.Abstract;
using AspProjectZust.Entities.Entity;
using AspProjectZust.WebUI.Helpers;
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
        private UserManager<CustomIdentityUser> _userManager;
        private readonly IUserService _userService;
        private IWebHostEnvironment _webHost;
        private CustomIdentityDbContext _dbContext;

        public HomeController(UserManager<CustomIdentityUser> userManager, IUserService userService, CustomIdentityDbContext dbContext, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _userService = userService;
            _dbContext = dbContext;
            _webHost = webHost;
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


        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyProfile(UserInfoViewModel userInfo)
        {
            var helper = new ImageHelper(_webHost);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (userInfo.File != null)
            {
                userInfo.ImageUrl = await helper.SaveFile(userInfo.File);
                user.ImageUrl = userInfo.ImageUrl;
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            ViewBag.User = user;
            return View();
        }

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
            //var data = await _userService.GetAll();
            //var all = new List<CustomIdentityUser>();
            //for (int i = 0; i < data.Count(); i++)
            //{
            //    if (data[i].Id == user.Id)
            //    {
            //        all.Add(data[i]);
            //    }
            //}
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

        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var requests = _dbContext.FriendRequests.Where(r => r.ReceiverId == current.Id);
            return Ok(requests);
        }

        //[HttpPut]
        public async Task<IActionResult> UserUpdateInfo(UpdateUserViewModel updateUser)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Address = updateUser.Address;
            user.Country = updateUser.Country;
            user.Email = updateUser.Email;
            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.City = updateUser.City;
            user.Gender = updateUser.Gender;
            user.Occupation = updateUser.Occupation;
            user.RelationStatus = updateUser.RelationStatus;
            user.BloodGroup = updateUser.BloodGroup;
            user.Language = updateUser.Language;
            user.DateOfBirth = updateUser.DateOfBirth;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return View("Setting", "Home");
        }

        //[HttpPost]
        //public IActionResult UserChangeProfile(UpdateUserViewModel user)
        //{
        //    return Ok(user);
        //}
    }
}

