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
    const searchIcon = document.getElementById('search-icon');
    const searchBox = document.querySelector("#search-box");
    searchBox.id = 'search-box';
    searchIcon.onclick = function () {
        if (searchBox.style.display === 'block') {
            searchBox.style.display = 'none';
        } else {
            searchBox.style.display = 'block';
        }
    };
    document.addEventListener('keydown', function (e) {
        const searchInput = document.getElementById('search-input');
        if (e.key === 'Enter' && searchInput && searchBox.style.display === 'block') {
            const query = searchInput.value.trim();
            if (query) {
                // เปลี่ยน URL ไปยังหน้าผลลัพธ์การค้นหาพร้อม query
                window.location.href = `/Home/Search?input=${encodeURIComponent(query)}`;
            }
        }
    });

    const listItems = document.querySelectorAll("#search-box ul li");
    const search_input = document.querySelector("#search-box input");
    listItems.forEach(li => {
        li.addEventListener('click', function () {
            search_input.value = li.textContent;
        });
    });
});