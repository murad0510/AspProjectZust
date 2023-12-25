using AspProjectZust.Core.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectZust.Entities.Entity
{
    public class CustomIdentityUser : IdentityUser, IEntity
    {
        public CustomIdentityUser(IEnumerable<Friend>? friends, IEnumerable<Post>? posts, IEnumerable<FriendRequest>? friendRequests, IEnumerable<Chat>? chats, IEnumerable<Notification>? notifications)
        {
            Friends = friends;
            Posts = posts;
            FriendRequests = friendRequests;
            Chats = chats;
            Notifications = notifications;
        }

        public CustomIdentityUser()
        {
            
        }

        public int LikeCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsOnline { get; set; }
        public bool IsFriend { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BackUpEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? Gender { get; set; }
        public string? RelationStatus { get; set; }
        public string? BloodGroup { get; set; }
        public string? Language { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        public virtual IEnumerable<Friend>? Friends { get; set; }
        public virtual IEnumerable<Post>? Posts { get; set; }
        public virtual IEnumerable<FriendRequest>? FriendRequests { get; set; }
        public virtual IEnumerable<Chat>? Chats { get; set; }
        //[NotMapped]
        public virtual IEnumerable<Notification>? Notifications { get; set; }

    }
}
