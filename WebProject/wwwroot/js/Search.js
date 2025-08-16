document.addEventListener('DOMContentLoaded', function () {
    // Clear search button functionality
    //const searchInput = document.querySelector('.search-bar');
    //const clearButton = document.querySelector('.clear-search');

    //clearButton.addEventListener('click', function () {
    //    searchInput.value = '';
    //    searchInput.focus();
    //});

    // Tab navigation functionality
    const tabs = document.querySelectorAll('.nav-tab');
    const clearButton = document.querySelector('.clear-search');
    const searchIcon = document.querySelector('.search-icon');
    tabs.forEach(tab => {
        tab.addEventListener('click', function () {
            tabs.forEach(t => t.classList.remove('active'));
            this.classList.add('active');
        });
    });

    const all_tab = document.querySelector(".all");
    const match_tab = document.querySelector(".match");
    const tag_tab = document.querySelector(".tag");

    const all_posts = document.querySelector(".all-post");
    const match_posts = document.querySelector(".match-post");
    const tag_posts = document.querySelector(".tag-post");

    all_tab.addEventListener('click', function () {
        all_posts.style.display = 'grid';
        match_posts.style.display = 'none';
        tag_posts.style.display = 'none';
    });

    match_tab.addEventListener('click', function () {
        all_posts.style.display = 'none';
        match_posts.style.display = 'grid';
        tag_posts.style.display = 'none';
    });

    tag_tab.addEventListener('click', function () {
        all_posts.style.display = 'none';
        match_posts.style.display = 'none';
        tag_posts.style.display = 'grid';
    });

    clearButton.addEventListener('click', function () {
        searchInput.value = ''; 
        searchInput.focus();    
    });

    searchIcon.addEventListener('click', function () {
        if (searchInput.style.display === 'none' || searchInput.style.display === '') {
            searchInput.style.display = 'block';  
            searchInput.focus();                  
        } else {
            searchInput.style.display = 'none';   
        }
    });

    
});