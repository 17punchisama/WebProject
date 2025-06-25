document.addEventListener('DOMContentLoaded', function () {
    console.log("DOM content loaded");

    // Close the notification dropdown by default when the page is loaded
    let notiCon = document.querySelector(".noti-container");
    notiCon.style.display = 'none'; // Ensure the notification container is hidden initially

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

    // Function to close any open dropdown
    function closeAllDropdowns() {
        if (accountNav.style.display === 'block') {
            accountNav.style.display = 'none';
        }
        if (notiCon.style.display === 'block') {
            notiCon.style.display = 'none';
        }
        if (searchBox.style.display === 'block') {
            searchBox.style.display = 'none';
        }
    }

    userIcon.onclick = function () {
        console.log("User icon clicked");

        // Close the notification dropdown if it's open
        if (notiCon.style.display === 'block') {
            notiCon.style.display = 'none';
        }

        // Toggle the account navigation dropdown
        if (accountNav.style.display === 'block') {
            accountNav.style.display = 'none';
        } else {
            accountNav.style.display = 'block';
        }
    };

    notiIcon.onclick = function () {
        console.log("Notification icon clicked");

        // Close the account navigation dropdown if it's open
        if (accountNav.style.display === 'block') {
            accountNav.style.display = 'none';
        }

        // Toggle the notification container
        if (notiCon.style.display === 'block') {
            notiCon.style.display = 'none';
        } else {
            notiCon.style.display = 'block';
        }
    };

    const searchIcon = document.getElementById('search-icon');
    const searchBox = document.querySelector("#search-box");

    // Toggle the search box when the search icon is clicked
    searchIcon.onclick = function () {
        // Close all other dropdowns before toggling the search box
        //closeAllDropdowns();
        console.log("search icon clicked");

        // Toggle the search box visibility (open/close)
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
                // Redirect to search results page with query
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
