function SendFollow(id) {
    $.ajax({
        url: `/Home/SendFollow/${id}`,
        method: "GET",
        success: function (data) {
            //let element = document.querySelector("#alert");
            //element.style.display = "block";
            //element.innerHTML = "You friend request sent successfully";
            GetAllUsers();
            SendFollowCall(id);
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
                content += `
                                            <div class="item d-flex align-items-center">
                                                <div class="figure">
                                                    <a href="#"><img src="/assets/images/user/${data[i].sender.imageUrl}" class="rounded-circle" alt="image"></a>
                                                </div>

                                                <div class="content d-flex justify-content-between align-items-center">
                                                    <div class="text">
                                                        <h4><a href="#">${data[i].sender.userName}</a></h4>
                                                    </div>
                                                    <div class="btn-box d-flex align-items-center">
                                                        <button class="delete-btn d-inline-block me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" type="button"><i class="ri-close-line"></i></button>

                                                        <button class="confirm-btn d-inline-block" data-bs-toggle="tooltip" data-bs-placement="top" title="Confirm" type="button"><i class="ri-check-line"></i></button>
                                                    </div>
                                                </div>
                                            </div>
                
                
                `;
            }
            console.log(content);
            $("#requests").html(content);
        }
    })
}

async function GetAllUsers() {
    $.ajax({
        url: "/Home/GetAllUsers",
        method: "GET",

        success: function (data) {
            var context = "";
            let subContent = "";
            let d = "";
            for (var i = 0; i < data.length; i++) {
                if (data[i].isFriend) {
                    subContent = `<button class='btn btn-outline-secondary' onclick="UnFollowCall('${data[i].id}')"> UnFollow</button>`;
                    if (data[i].isOnline) {
                        d += `
                    <div class="contact-item">
                        <a href="#"><img src="/assets/images/user/${data[i].imageUrl}" class="rounded-circle" alt="image"></a>
                        <span class="name"><a href="#">${data[i].userName}</a></span>
                        <span class="status-online"></span>
                    </div>
                    `;
                    }
                }
                else {

                    subContent = `<button onclick="SendFollow('${data[i].id}')" class='btn btn-outline-primary'> Follow</button>`;
                }
                context += `
                    <div class="col-lg-3 col-sm-6">
                       <div class="single-friends-card">
                           <div class="friends-image">
                               <a href="#">
                                    <img src="/assets/images/friends/friends-bg-10.jpg" alt="image">
                               </a>
                               <div class="icon">
                                   <a href="#"><i class="flaticon-user"></i></a>
                               </div>
                           </div>
                           <div class="friends-content">
                               <div class="friends-info d-flex justify-content-between align-items-center">
                                   <a href="#">
                                        <img style="width:100px;height:100px" src='/assets/images/user/${data[i].imageUrl}' alt="image">
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

            var id2 = document.getElementById("onlineUsers");
            id2.innerHTML = d;
        }
    })
}

//GetAllUsers();
GetMyRequests();