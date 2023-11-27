function GetPosts(authorId){
    const postsContainer = document.getElementById('postsContainer');

    $.ajax({
        url: 'http://localhost:5015/api/Post/getPostsByAuthorId?authorId=' + authorId,
        method: 'GET',
        // headers: {
        //     "Content-Type": "application/json",
        //     "Authorization": "bearer " + token
        // },
        success: function (response) {
            response.forEach((post)=>{
                console.log(post);
                const data = JSON.parse(JSON.stringify(post));
                
                const authorId = data.authorId;
                const authorName = data.authorName;
                const authorUserName = data.authorUserName;
                const commentCount = data.commentCount;
                const id = data.id;
                const likeCount = data.likeCount;
                const postDate = data.postDate;
                const postText = data.postText;
                const comments = data.comments;
                console.log(comments)
                console.log(data.comments);
                
                const postContainer = document.createElement('div');
                postContainer.classList.add('post-list');
                postContainer.innerHTML = 
                    `
                        <div class="post-head-items">
                        <div class="left-post-head-items">
                            <p style="height: 5px;">${authorName}</p>
                            <p style="color: gray; height: 5px;">${postDate}</p>
                        </div>
                        <div class="right-post-head-items">
                            <p class="post-more-choises" class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa-solid fa-ellipsis more-choises" ></i></p>
                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <a class="dropdown-item post-dropdown-items" href="#">Delete post<i class="fa-solid fa-trash" style="margin-left: 50px; color: #da6c31;"></i></a>
                              </div>
                        </div>
                    </div>
                    <p class="post-content">${postText}</p>
                    <h6 style="margin-top: 10px; margin-bottom: 0px">Likes: ${likeCount}</h6>
                    <hr style="margin-top: 5px; margin-bottom: 10px;">
                    <div class="post-list-like-comment">
                        <button class="like-button fa-solid fa-thumbs-up" data-post-id="${id}" data-author-id="${authorId}" onclick="Like()"></button>
                        <button type="button" data-toggle="collapse" data-target="#collapseExample${id}" aria-expanded="false" aria-controls="collapseExample" class="fa-solid fa-comment" onclick="ShowIsLiked()"></button>
                    </div>
                    <hr style="margin-top: 10px; margin-bottom: 10px;">
                    <div class="textarea-container">
                        <textarea type="text" id="post${id}" class="input-comment-content"></textarea>
                        <button id="myButton" data-post-author-id="${authorId}" data-post-id="post${id}" class="fa-regular fa-paper-plane send-comment" onclick="AddComment()"></button>
                    </div>
                    <div id="collapseExample${id}" class="collapse">
                            <div class="post-comment">
                                
                            </div>
                    </div>
                    `
                postsContainer.appendChild(postContainer);
                
                
                const commentsContainer = document.getElementById(`collapseExample${id}`)
                
                comments.forEach((data)=>{
                    
                    const comment = JSON.parse(JSON.stringify(data));
                    
                    const commentId = comment.id;
                    const commentAuthorId = comment.authorId;
                    const commentAuthorUserName = comment.authorUserName;
                    const commentText = comment.commentText;
                    const commentDate = comment.date;
                    const commentLikeCount = comment.likeCount;

                    const commentContainer = document.createElement('div');
                    commentContainer.classList.add('post-comment');
                    commentContainer.innerHTML =
                        `
                        <hr style="margin: 0;">
                                <div class="post-comment-head">
                                    <p>${commentAuthorUserName}</p>
                                    <p style="color: gray;">${commentDate}</p>
                                </div>
                                <div class="post-comment-content">
                                    <div class="comment">
                                        <p>${commentText}</p>
                                    </div>
                                    <div class="comment-like-button">
                                        <button data-comment-author-id="${commentAuthorId}" data-comment-id="${commentId}" onclick="LikeComment()" id=${commentId} class="like-comment fa-regular fa-heart fa-xl"></button>
                                    </div>
                                    <div class="post-comment-like-count">
                                        ${commentLikeCount}
                                    </div>
                                    <hr>
                                </div>
                                <hr>
                    `;
                    commentsContainer.appendChild(commentContainer);
                })
                
                
            })
        },
        error: function (error) {
            reject(error);
        }
    });
}

document.getElementById('executeGetPosts').addEventListener('click',function (){
    GetPosts(5);
})