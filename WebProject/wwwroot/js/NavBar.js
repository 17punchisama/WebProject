document.addEventListener('DOMContentLoaded', function () {
    console.log("DOM content loaded");

    let createBox = document.getElementById('create-box');
    let penIcon = document.querySelector('.fa-solid.fa-pen');

    penIcon.onclick = function () {
        console.log("Pen icon clicked");

        // Redirect directly to the activity creation page
        window.location.href = '/Post/Create';
    };

    let accountNav = document.querySelector(".account-nav");
    let userIcon = document.querySelector('.fa-solid.fa-user');
    let notiIcon = document.querySelector('.fa-solid.fa-bell');
    let notiCon = document.querySelector(".noti-container");

    userIcon.onclick = function () {
        console.log("User icon clicked");

        if (accountNav.style.display === 'block') {
            accountNav.style.display = 'none';
        } else {
            accountNav.style.display = 'block';
        }
    };

    notiIcon.onclick = function () {
        console.log("Notification icon clicked");

        if (notiCon.style.display === 'block') {
            notiCon.style.display = 'none';
        } else {
            notiCon.style.display = 'block';
        }
    };
});
