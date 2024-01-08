﻿using AspProjectZust.Business.Abstract;
using AspProjectZust.Entities.Entity;
using AspProjectZust.WebUI.Helpers;
using AspProjectZust.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        [HttpPost(Name = "AddMessage")]
        public async Task<IActionResult> AddMessage(MessageViewModel model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var chat = await _dbContext.Chats.FirstOrDefaultAsync(c => c.SenderId == model.SenderId && c.ReceiverId == model.ReceiverId
           || c.SenderId == model.ReceiverId && c.ReceiverId == model.SenderId);
                if (chat != null)
                {
                    var message = new Message
                    {
                        ChatId = chat.id,
                        Content = model.Message,
                        WriteTime = DateTime.Now,
                        HasSeen = false,
                        IsImage = false,
                        ReceiverId = model.ReceiverId,
                        SenderId = user.Id,
                    };

                    if (user.Id != model.ReceiverId)
                    {
                        message.ReceiverId = model.ReceiverId;
                        message.SenderId = user.Id;
                    }
                    else
                    {
                        message.SenderId = user.Id;
                        message.ReceiverId = model.SenderId;
                    }

                    await _dbContext.Messages.AddAsync(message);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        public async Task<IActionResult> Messages(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var chat = await _dbContext.Chats.Include(nameof(Chat.Receiver)).FirstOrDefaultAsync(c => c.SenderId == user.Id && c.ReceiverId == id || c.ReceiverId == user.Id && c.SenderId == id);
            var receiver = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (chat == null)
            {
                chat = new Chat
                {
                    Messages = new List<Message>(),
                    ReceiverId = id,
                    SenderId = user.Id,
                };

                await _dbContext.Chats.AddAsync(chat);
                await _dbContext.SaveChangesAsync();
            }

            if (chat.ReceiverId == user.Id)
            {
                chat.Receiver = _dbContext.Users.FirstOrDefault(u => u.Id == chat.SenderId);
            }

            List<Message> messages = new List<Message>();
            if (chat != null)
            {
                messages = await _dbContext.Messages.Where(c => c.ChatId == chat.id).OrderBy(d => d.WriteTime).ToListAsync();
            }

            chat.Messages = messages;

            var model = new ChatViewModel
            {
                CurrentChat = chat,
                Sender = receiver,
                CurrentUserId = user.Id,
            };

            return View(model);
        }

        public async Task<IActionResult> GetAllMessages(string receiverId, string senderId)
        {
            var chat = await _dbContext.Chats.Include(nameof(Chat.Receiver)).FirstOrDefaultAsync(c => c.SenderId == receiverId && c.ReceiverId == senderId || c.ReceiverId == receiverId && c.SenderId == senderId);

            var receiver = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == senderId);
            var sender = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == receiverId);
            var messages = await _dbContext.Messages.Where(m => m.ReceiverId == receiverId && m.SenderId == senderId || m.SenderId == receiverId && m.ReceiverId == senderId).OrderBy(d => d.WriteTime).ToListAsync();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            chat.Messages = messages;

            //var model = new ChatViewModel
            //{
            //    CurrentChat = chat,
            //    Sender = receiver,
            //    CurrentUserId = user.Id,
            //};
            return Ok(new { Messages = messages, CurrenUserId = user.Id, Chat = chat, ReceiverImageUrl = receiver.ImageUrl, SenderImageUrl = sender.ImageUrl });
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

        //public async Task<IActionResult> GetFriends()
        //{
        //    var current = await _userManager.GetUserAsync(HttpContext.User);
        //    var friends = await _dbContext.Friends.Include(nameof(Friend.YourFriend)).ToListAsync();
        //    var myfriends = friends.Where(f => f.OwnId == current.Id).ToList();
        //    return Ok(myfriends);
        //}

        public async Task<IActionResult> NewsFeed()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;

            Console.WriteLine(user.FollowersCount);
            Console.WriteLine(user.FollowingCount);

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

        public async Task<IActionResult> AlreadySent(string id)
        {
            try
            {
                var senderUser = await _userManager.GetUserAsync(HttpContext.User);
                var receiverUser = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == id);

                var request = await _dbContext.FriendRequests.FirstOrDefaultAsync(f => f.SenderId == senderUser.Id && f.ReceiverId == receiverUser.Id && f.Status == "Request");

                _dbContext.FriendRequests.Remove(request);

                //if (receiverUser != null)
                //{

                var sendRequest = new FriendRequest
                {
                    Content = $"He withdrew the friendly situation he had sent",
                    SenderId = senderUser.Id,
                    Sender = senderUser,
                    ReceiverId = id,
                    Status = "Notification",
                    RequestTime = DateTime.Now.ToShortDateString() + "\t\t" + DateTime.Now.ToShortTimeString(),
                };

                await _dbContext.FriendRequests.AddAsync(sendRequest);
                await _dbContext.SaveChangesAsync();
                //await _userManager.UpdateAsync(receiverUser);
                //}
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

            var myRequests = _dbContext.FriendRequests.Where(s => s.SenderId == user.Id);
            var myFriends = _dbContext.Friends.Where(f => f.OwnId == user.Id || f.YourFriendId == user.Id);

            foreach (var item in data)
            {
                var request = myRequests.FirstOrDefault(r => r.ReceiverId == item.Id && r.Status == "Request");
                if (request != null)
                {
                    item.HasRequestPending = true;
                }
                var friend = myFriends.FirstOrDefault(f => f.OwnId == item.Id || f.YourFriendId == item.Id);
                if (friend != null)
                {
                    item.IsFriend = true;
                }
            }

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
                    Content = $"Sent You A Friend Request",
                    SenderId = senderUser.Id,
                    Sender = senderUser,
                    ReceiverId = id,
                    Status = "Request",
                    RequestTime = DateTime.Now.ToShortDateString() + "\t\t" + DateTime.Now.ToShortTimeString(),
                };
                _dbContext.FriendRequests.Add(request);
                await _userManager.UpdateAsync(receiverUser);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        public async Task<IActionResult> UnFollowCall(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var friendUser = _userManager.Users.FirstOrDefault(i => i.Id == id);

            var friend = _dbContext.Friends.Where(f => f.OwnId == id && f.YourFriendId == user.Id || f.OwnId == user.Id && f.YourFriendId == id);

            //var request = await _dbContext.FriendRequests.FirstOrDefaultAsync(f => f.SenderId == sender.Id && f.ReceiverId == receicer.Id && f.Status == "Request");

            user.FollowersCount -= 1;
            user.FollowingCount -= 1;

            friendUser.FollowersCount -= 1;
            friendUser.FollowingCount -= 1;

            var request = new FriendRequest
            {
                Content = $"He unfriended you",
                SenderId = user.Id,
                Sender = user,
                ReceiverId = id,
                Status = "Notification",
                RequestTime = DateTime.Now.ToShortDateString() + "\t\t" + DateTime.Now.ToShortTimeString(),
            };

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateAsync(friendUser);

            await _dbContext.FriendRequests.AddAsync(request);


            _dbContext.Friends.RemoveRange(friend);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var requests = await _dbContext.FriendRequests.Include(nameof(FriendRequest.Sender)).Where(r => r.ReceiverId == current.Id).OrderByDescending(r => r.RequestTime).ToListAsync();
            var allUsers = _dbContext.Users.ToList();

            //for (int i = 0; i < requests.Count; i++)
            //{
            //    for (int k = 0; k < allUsers.Count; k++)
            //    {
            //        if (requests[i].SenderId == allUsers[k].Id)
            //        {
            //            requests[i].Sender = allUsers[k];
            //        }
            //    }
            //}
            return Ok(requests);
        }

        //public async Task<IActionResult> GetAllRequests()
        //{
        //    var current = await _userManager.GetUserAsync(HttpContext.User);
        //    var requests = _dbContext.FriendRequests.Where(r => r.ReceiverId == current.Id);
        //    return Ok(requests);
        //}

        //[HttpPut]

        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(user);
        }

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

        public async Task<IActionResult> DeleteRequest(int requestId, string senderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var sender = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == senderId);

            var request = await _dbContext.FriendRequests.FirstOrDefaultAsync(r => r.Id == requestId);

            var newRequest = new FriendRequest
            {
                ReceiverId = sender.Id,
                Sender = user,
                SenderId = user.Id,
                Status = "Notification",
                Content = "Rejected the offer of friendship",
                RequestTime = DateTime.Now.ToShortDateString() + "\t\t" + DateTime.Now.ToShortTimeString(),
            };

            await _dbContext.FriendRequests.AddAsync(newRequest);


            _dbContext.FriendRequests.Remove(request);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> DeleteNotification()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var requests = await _dbContext.FriendRequests.Where(u => u.ReceiverId == user.Id && u.Status == "Notification").ToListAsync();
            if (requests != null)
            {
                _dbContext.FriendRequests.RemoveRange(requests);
                await _dbContext.SaveChangesAsync();
                return Ok(user.Id);
            }
            return BadRequest();
        }

        public async Task<IActionResult> NotificationGeneralFormOfInformation(int requestId)
        {
            var requests = await _dbContext.FriendRequests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (requests != null)
            {
                _dbContext.FriendRequests.Remove(requests);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        public async Task<IActionResult> UserMessage(string id)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            var friend = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return Ok(friend);
        }


        public async Task<IActionResult> ConfirmRequest(string senderId, int requestId)
        {
            var receiver = await _userManager.GetUserAsync(HttpContext.User);
            var sender = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == senderId);

            sender.FriendRequests.Add(new FriendRequest
            {
                Status = "Notification",
                Content = $"{receiver.UserName} confirm request",
                ReceiverId = sender.Id,
                SenderId = receiver.Id,
                Sender = receiver,
                RequestTime = DateTime.Now.ToShortDateString() + "\t\t" + DateTime.Now.ToShortTimeString(),
            });

            var receiverFriend = new Friend
            {
                OwnId = receiver.Id,
                YourFriendId = sender.Id,
            };

            var senderFriend = new Friend
            {
                OwnId = sender.Id,
                YourFriendId = receiver.Id,
            };

            _dbContext.Friends.Add(senderFriend);
            _dbContext.Friends.Add(receiverFriend);

            receiver.FollowersCount += 1;
            receiver.FollowingCount += 1;

            sender.FollowersCount += 1;
            sender.FollowingCount += 1;

            var request = await _dbContext.FriendRequests.FirstOrDefaultAsync(f => f.Id == requestId);

            _dbContext.FriendRequests.Remove(request);

            await _userManager.UpdateAsync(receiver);
            await _userManager.UpdateAsync(sender);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

