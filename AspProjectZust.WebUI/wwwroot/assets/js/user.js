function SendFollow(id) {
    $.ajax({
        url: `/Home/SendFollow/${id}`,
        method: "GET",
        success: function (data) {
            console.log(data);
            //let element = document.querySelector("#alert");
            //element.style.display = "block";
            //element.innerHTML = "You friend request sent successfully";
            GetAllUsers();
            //SendFollowCall(id);
            //setTimeout(() => {
            //    element.innerHTML = "";
            //    element.style.display = "none";
            //}, 5000);
        }
    })
}

function GetMyRequests() {
    $.ajax({
        url: "/Home/GetAllRequests",
        method: "GET",
        success: function (data) {
            let content = "";
            let subContent = "";
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                //alert(data[i].sender.userName);
                //if (data[i].status == "Request") {
                //    subContent = `
                //    <div class='card-body'>
                //        <button class='btn btn-success' onclick="AcceptRequest('${data[i].senderId}','${data[i].receiverId}','${data[i].id}')">Accept </button>
                //        <button class='btn btn-secondary' onclick="DeclineRequest(${data[i].id},'${data[i].senderId}')">Decline</button>
                //    </div>
                //    `;
                //}
                //else {
                //    subContent = ` <div class='card-body'>
                //        <button class='btn btn-secondary' onclick="DeleteRequest('${data[i].receiverId}','${data[i].id}')">Delete</button>
                //    </div>`;
                //}

                //let item = `<div class='card' style='width:15rem;'>
                //        <div class='card-body'> 
                //            <h5 class='card-title'>Friend Request </h5>
                //        </div>
                //               <ul class='list-group list-group-flush'>
                //<li class='list-group-item'>${data[i].content} </li>
                //</ul>
                //    ${subContent}
                //</div>`;

                //content += item;
            }
            //$("#requests").html(content);
        }
    })
}

async function GetAllUsers() {
    $.ajax({
        url: "/Home/GetAllUsers",
        method: "GET",

        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var context = "";
                let subContent = "";
                if (data[i].isFriend) {
                    subContent = `<button  class='btn btn-outline-secondary' onclick="UnFollowCall('${data[i].id}')"> UnFollow</button>`;
                }
                else {

                    subContent = `<button onclick="SendFollow('${data[i].id}')" class='btn btn-outline-primary'> Follow</button>`;
                }
                context += `
                    <div class="col-lg-3 col-sm-6">
                       <div class="single-friends-card">
                           <div class="friends-image">
                               <a href="#">
                                    <img src="~/assets/images/friends/friends-bg-10.jpg" alt="image">
                               </a>
                               <div class="icon">
                                   <a href="#"><i class="flaticon-user"></i></a>
                               </div>
                           </div>
                           <div class="friends-content">
                               <div class="friends-info d-flex justify-content-between align-items-center">
                                   <a href="#">
                                        <img src="~/assets/images/user/user-19.jpg" alt="image">
                                   </a>
                                   <div class="text ms-3">
                                       <h3><a href="#">${data[i].userName}</a></h3>
                                   </div>
                               </div>
                               <ul class="statistics">
                                   <li>
                                       <a href="#">
                                           <span class="item-number">${data[i].likeCount}</span>
                                           <span class="item-text">Likes</span>
                                       </a>
                                   </li>
                                   <li>
                                       <a href="#">
                                           <span class="item-number">${data[i].followingCount}</span>
                                           <span class="item-text">Following</span>
                                       </a>
                                   </li>
                                   <li>
                                       <a href="#">
                                           <span class="item-number">${data[i].followersCount}</span>
                                           <span class="item-text">Followers</span>
                                       </a>
                                   </li>
                               </ul>
                               <div class="button-group d-flex justify-content-between align-items-center">
                                   <div class="add-friend-btn">
                                   ${subContent}
                                   </div>
                                   <div class="send-message-btn">
                                       <button type="submit">Send Message</button>
                                   </div>
                               </div>
                           </div>
                       </div>
                   </div>
                `;
            }

            var id = document.getElementById("allUsers");
            if (id != null) {
                id.innerHTML = context;
            }
        }
    })
}

GetAllUsers();
GetMyRequests();